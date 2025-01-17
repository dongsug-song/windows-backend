﻿// ***********************************************************************
// Copyright (c) 2010 Charlie Poole
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
using System.IO;
using System.Reflection;

#if PORTABLE
using Path = NUnit.Compatibility.Path;
#endif

namespace NUnit.Framework.Internal
{
    /// <summary>
    /// TestAssembly is a TestSuite that represents the execution
    /// of tests in a managed assembly.
    /// </summary>
    public class TestAssembly : TestSuite
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestAssembly"/> class
        /// specifying the Assembly and the path from which it was loaded.
        /// </summary>
        /// <param name="assembly">The assembly this test represents.</param>
        /// <param name="path">The path used to load the assembly.</param>
        public TestAssembly(Assembly assembly, string path)
            : base(path)
        {
            this.Assembly = assembly;
            this.Name = Path.GetFileName(path);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestAssembly"/> class
        /// for a path which could not be loaded.
        /// </summary>
        /// <param name="path">The path used to load the assembly.</param>
        public TestAssembly(string path) : base(path)
        {
            this.Name = Path.GetFileName(path);
        }

        /// <summary>
        /// Gets the Assembly represented by this instance.
        /// </summary>
        public Assembly Assembly { get; private set; }

        /// <summary>
        /// Gets the name used for the top-level element in the
        /// XML representation of this test
        /// </summary>
        public override string TestType
        {
            get
            {
                return "Assembly";
            }
        }
    }
}
