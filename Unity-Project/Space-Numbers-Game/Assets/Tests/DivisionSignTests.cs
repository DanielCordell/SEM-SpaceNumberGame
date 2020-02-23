using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MathsTests
{
    public class DivisionSignTests
    {
        [Test]
        public void Test_FindIndexOfDivisionSignWithSingleDivision()
        {
            Operator[] operators = new Operator[] { Operator.Add, Operator.Multiply, Operator.Divide, Operator.Multiply, Operator.Equals };
            int[] results = Level.FindIndexOfDivisionSign(operators);
            Assert.NotNull(results);
            Assert.AreEqual(results.Length, 1);
            Assert.AreEqual(results[0], 2);
        }
        
        [Test]
        public void Test_FindIndexOfDivisionSignWithMultipleDivisions()
        {
            Operator[] operators = new Operator[] { Operator.Divide, Operator.Multiply, Operator.Divide, Operator.Multiply, Operator.Divide, Operator.Equals };
            int[] results = Level.FindIndexOfDivisionSign(operators);
            Assert.NotNull(results);
            Assert.AreEqual(results.Length, 3);
            Assert.AreEqual(results[0], 0);
            Assert.AreEqual(results[1], 2);
            Assert.AreEqual(results[2], 4);
        }

        [Test]
        public void Test_FindIndexOfDivisionSignWithNoDivsions()
        {
            Operator[] operators = new Operator[] { Operator.Multiply, Operator.Multiply, Operator.Add, Operator.Equals };
            int[] results = Level.FindIndexOfDivisionSign(operators);
            Assert.NotNull(results);
            Assert.AreEqual(results.Length, 0);
        }

        [Test]
        public void Test_FindIndexOfDivisionSignWithEmptyList()
        {
            Operator[] operators = new Operator[] { };
            int[] results = Level.FindIndexOfDivisionSign(operators);
            Assert.NotNull(results);
            Assert.AreEqual(results.Length, 0);
        }

        [Test]
        public void Test_FindIndexOfDivisionSignWithNull()
        {
            Assert.Throws(typeof(ArgumentNullException), () => Level.FindIndexOfDivisionSign(null));
        }
    }
}
