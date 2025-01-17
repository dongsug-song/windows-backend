﻿// ***********************************************************************
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
using System;
using NUnit.Framework.Internal;

namespace NUnit.Framework
{
    /// <summary>
    /// Attribute used to provide descriptive text about a 
    /// test case or fixture.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class PostconditionAttribute : PropertyAttribute
    {
        public int Index
        {
            set;
            get;
        }
        public string Description
        {
            set;
            get;
        }
        /// <summary>
        /// Construct a description Attribute
        /// </summary>
        /// <param name="description">The text of the description</param>
        public PostconditionAttribute(int index, string description) : base("" + index, description) {
            Index = index;
            Description = description;
        }
    }

}
