// ***********************************************************************
// Copyright (c) 2007 Charlie Poole
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
namespace NUnit.Framework.Constraints
{
    /// <summary>
    /// EndsWithConstraint can test whether a string ends
    /// with an expected substring.
    /// </summary>
    public class EndsWithConstraint : StringConstraint
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EndsWithConstraint"/> class.
        /// </summary>
        /// <param name="expected">The expected string</param>
        public EndsWithConstraint(string expected) : base(expected) 
        {
            this.descriptionText = "String ending with";
        }

        /// <summary>
        /// Test whether the constraint is matched by the actual value.
        /// This is a template method, which calls the IsMatch method
        /// of the derived class.
        /// </summary>
        /// <param name="actual"></param>
        /// <returns></returns>
        protected override bool Matches(string actual)
        {
            if (this.caseInsensitive)
                return actual != null && actual.ToLower().EndsWith(expected.ToLower());
            else
                return actual != null && actual.EndsWith(expected);
        }
    }
}