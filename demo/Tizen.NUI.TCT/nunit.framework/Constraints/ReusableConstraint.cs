﻿// ***********************************************************************
// Copyright (c) 2008 Charlie Poole
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
    /// ReusableConstraint wraps a constraint expression after
    /// resolving it so that it can be reused consistently.
    /// </summary>
    public class ReusableConstraint : IResolveConstraint
    {
        private readonly IConstraint constraint;

        /// <summary>
        /// Construct a ReusableConstraint from a constraint expression
        /// </summary>
        /// <param name="c">The expression to be resolved and reused</param>
        public ReusableConstraint(IResolveConstraint c)
        {
            this.constraint = c.Resolve();
        }

        /// <summary>
        /// Converts a constraint to a ReusableConstraint
        /// </summary>
        /// <param name="c">The constraint to be converted</param>
        /// <returns>A ReusableConstraint</returns>
        public static implicit operator ReusableConstraint(Constraint c)
        {
            return new ReusableConstraint(c);
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return constraint.ToString();
        }

        #region IResolveConstraint Members

        /// <summary>
        /// Return the top-level constraint for this expression
        /// </summary>
        /// <returns></returns>
        public IConstraint Resolve()
        {
            return constraint;
        }

        #endregion
    }
}
