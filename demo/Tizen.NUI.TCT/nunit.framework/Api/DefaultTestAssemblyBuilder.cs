// ***********************************************************************
// Copyright (c) 2012-2014 Charlie Poole
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************
#define PORTABLE
#define TIZEN
#define NUNIT_FRAMEWORK
#define NUNITLITE
#define NET_4_5
#define PARALLEL
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Common;
using NUnit.Compatibility;
using NUnit.Framework.Interfaces;
using NUnit.Framework.Internal;
using NUnit.Framework.Internal.Builders;

namespace NUnit.Framework.Api
{
    /// <summary>
    /// DefaultTestAssemblyBuilder loads a single assembly and builds a TestSuite
    /// containing test fixtures present in the assembly.
    /// </summary>
    public class DefaultTestAssemblyBuilder : ITestAssemblyBuilder
    {
        static Logger log = InternalTrace.GetLogger(typeof(DefaultTestAssemblyBuilder));

        #region Instance Fields

        /// <summary>
        /// The default suite builder used by the test assembly builder.
        /// </summary>
        ISuiteBuilder _defaultSuiteBuilder;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultTestAssemblyBuilder"/> class.
        /// </summary>
        public DefaultTestAssemblyBuilder()
        {
            _defaultSuiteBuilder = new DefaultSuiteBuilder();
        }

        #endregion

        #region Build Methods

        /// <summary>
        /// Build a suite of tests from a provided assembly
        /// </summary>
        /// <param name="assembly">The assembly from which tests are to be built</param>
        /// <param name="options">A dictionary of options to use in building the suite</param>
        /// <returns>
        /// A TestSuite containing the tests found in the assembly
        /// </returns>
        public ITest Build(Assembly assembly, IDictionary<string, object> options)
        {
#if PORTABLE
            log.Debug("Loading {0}", assembly.FullName);
#else
            log.Debug("Loading {0} in AppDomain {1}", assembly.FullName, AppDomain.CurrentDomain.FriendlyName);
#endif

#if SILVERLIGHT
            string assemblyPath = AssemblyHelper.GetAssemblyName(assembly).Name;
#elif PORTABLE
            string assemblyPath = AssemblyHelper.GetAssemblyName(assembly).FullName;
#else
            string assemblyPath = AssemblyHelper.GetAssemblyPath(assembly);
#endif

            return Build(assembly, assemblyPath, options);
        }

        /// <summary>
        /// Build a suite of tests given the filename of an assembly
        /// </summary>
        /// <param name="assemblyName">The filename of the assembly from which tests are to be built</param>
        /// <param name="options">A dictionary of options to use in building the suite</param>
        /// <returns>
        /// A TestSuite containing the tests found in the assembly
        /// </returns>
        public ITest Build(string assemblyName, IDictionary<string, object> options)
        {
#if PORTABLE
            log.Debug("Loading {0}", assemblyName);
#else
            log.Debug("Loading {0} in AppDomain {1}", assemblyName, AppDomain.CurrentDomain.FriendlyName);
#endif

            TestSuite testAssembly = null;

            try
            {
                var assembly = AssemblyHelper.Load(assemblyName);

                if (assembly == null)
                    throw new Exception("assembly is null");

                testAssembly = Build(assembly, assemblyName, options);
            }
            catch (Exception ex)
            {
                testAssembly = new TestAssembly(assemblyName);
                testAssembly.RunState = RunState.NotRunnable;
                testAssembly.Properties.Set(PropertyNames.SkipReason, ex.Message);
            }

            return testAssembly;
        }

        private TestSuite Build(Assembly assembly, string assemblyPath, IDictionary<string, object> options)
        {
            TestSuite testAssembly = null;

            try
            {
                if (options.ContainsKey(PackageSettings.DefaultTestNamePattern))
                    TestNameGenerator.DefaultTestNamePattern = options[PackageSettings.DefaultTestNamePattern] as string;

                if (options.ContainsKey(PackageSettings.TestParameters))
                {
                    string parameters = options[PackageSettings.TestParameters] as string;
                    if (!string.IsNullOrEmpty(parameters))
                        foreach (string param in parameters.Split(new[] { ';' }))
                        {
                            int eq = param.IndexOf("=");

                            if (eq > 0 && eq < param.Length - 1)
                            {
                                var name = param.Substring(0, eq);
                                var val = param.Substring(eq + 1);

                                TestContext.Parameters.Add(name, val);
                            }
                        }
                }

                IList fixtureNames = null;
                if (options.ContainsKey(PackageSettings.LOAD))
                    fixtureNames = options[PackageSettings.LOAD] as IList;
                var fixtures = GetFixtures(assembly, fixtureNames);

                testAssembly = BuildTestAssembly(assembly, assemblyPath, fixtures);
            }
            catch (Exception ex)
            {
                testAssembly = new TestAssembly(assemblyPath);
                testAssembly.RunState = RunState.NotRunnable;
                testAssembly.Properties.Set(PropertyNames.SkipReason, ex.Message);
            }

            return testAssembly;
        }

        #endregion

        #region Helper Methods

        private IList<Test> GetFixtures(Assembly assembly, IList names)
        {
            var fixtures = new List<Test>();
            log.Debug("Examining assembly for test fixtures");

            var testTypes = GetCandidateFixtureTypes(assembly, names);

            log.Debug("Found {0} classes to examine", testTypes.Count);
#if LOAD_TIMING
            System.Diagnostics.Stopwatch timer = new System.Diagnostics.Stopwatch();
            timer.Start();
#endif
            int testcases = 0;
            foreach (Type testType in testTypes)
            {
                var typeInfo = new TypeWrapper(testType);

                try
                {
                    if (_defaultSuiteBuilder.CanBuildFrom(typeInfo))
                    {
                        Test fixture = _defaultSuiteBuilder.BuildFrom(typeInfo);
                        fixtures.Add(fixture);
                        testcases += fixture.TestCaseCount;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex.ToString());
                }
            }

#if LOAD_TIMING
            log.Debug("Found {0} fixtures with {1} test cases in {2} seconds", fixtures.Count, testcases, timer.Elapsed);
#else
            log.Debug("Found {0} fixtures with {1} test cases", fixtures.Count, testcases);
#endif

            return fixtures;
        }

        private IList<Type> GetCandidateFixtureTypes(Assembly assembly, IList names)
        {
            var types = assembly.GetTypes();

            if (names == null || names.Count == 0)
                return types;

            var result = new List<Type>();

            foreach (string name in names)
            {
                Type fixtureType = assembly.GetType(name);
                if (fixtureType != null)
                    result.Add(fixtureType);
                else
                {
                    string prefix = name + ".";

                    foreach (Type type in types)
                        if (type.FullName.StartsWith(prefix))
                            result.Add(type);
                }
            }

            return result;
        }

        private TestSuite BuildTestAssembly(Assembly assembly, string assemblyName, IList<Test> fixtures)
        {
            TestSuite testAssembly = new TestAssembly(assembly, assemblyName);

            if (fixtures.Count == 0)
            {
                testAssembly.RunState = RunState.NotRunnable;
                testAssembly.Properties.Set(PropertyNames.SkipReason, "Has no TestFixtures");
            }
            else
            {
                NamespaceTreeBuilder treeBuilder =
                    new NamespaceTreeBuilder(testAssembly);
                treeBuilder.Add(fixtures);
                testAssembly = treeBuilder.RootSuite;
            }

            testAssembly.ApplyAttributesToTest(assembly);

#if !PORTABLE
#if !SILVERLIGHT
            testAssembly.Properties.Set(PropertyNames.ProcessID, System.Diagnostics.Process.GetCurrentProcess().Id);
#endif
            testAssembly.Properties.Set(PropertyNames.AppDomain, AppDomain.CurrentDomain.FriendlyName);
#endif

            // TODO: Make this an option? Add Option to sort assemblies as well?
            // [tronghieu.d] - no need to sort tcs, to sync with tc order of tct-mgr
            //testAssembly.Sort();

            return testAssembly;
        }
        #endregion
    }
}
