using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MathsTests
{
    public class ExpressionStringTests
    {
        [Test]
        public void Test_ExpressionGeneration()
        {
            int?[] x = new int?[] { 1, 5, 3, 7, null };
            Operator[] y = new Operator[] { Operator.Add, Operator.Multiply, Operator.Subtract, Operator.Equals };
            string str = Level.GenerateExpressionString(x, ref y);
            Assert.AreEqual(str, "1+5*3-7");
        }

        [Test]
        public void Test_NoEqualsInExpression()
        {
            int?[] x = new int?[] { 1, 5, 3, 7, null };
            Operator[] y = new Operator[] { Operator.Add, Operator.Multiply, Operator.Divide, Operator.Equals };
            string str = Level.GenerateExpressionString(x, ref y);
            Assert.IsFalse(str.Contains('='));
        }

        [Test] //Todo, this test won't exist after a refactor
        public void Test_NoSequentialDivision()
        {
            int?[] x = new int?[] { 1, 5, 3, 7, null };
            Operator[] y = new Operator[] { Operator.Add, Operator.Divide, Operator.Divide, Operator.Equals };
            string str = Level.GenerateExpressionString(x, ref y);
            Assert.AreNotEqual(y[2], Operator.Divide);
        }

        [Test]
        public void Test_ThrowsIfNoEquals()
        {
            int?[] x = new int?[] { 1, 5, 3, 7, null };
            Operator[] y = new Operator[] { Operator.Add, Operator.Divide, Operator.Divide, Operator.Add };
            Assert.Throws(typeof(ArgumentException), () => Level.GenerateExpressionString(x, ref y));
        }
    }
}
