using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace MathsTests
{
    public class GapsMemberFunctionTests
    {
        Level level;

        List<int>[] numberRanges;
        List<int> firstRange;
        List<int> secondRange;

        [OneTimeSetUp]
        public void Setup()
        {
            firstRange = new List<int> { 1, 2, 3 };
            secondRange = new List<int> { 4, 5 };
            numberRanges = new List<int>[] { firstRange.Select(it => it).ToList(), secondRange.Select(it => it).ToList() };
            level = new Level(1, numberRanges.Select(it => it).ToArray(), new Operator[] { Operator.Add, Operator.Equals }, new List<bool> { false, true, false });
        }


        [Test]
        public void Test_NumberOfGaps()
        {
            Assert.AreEqual(level.GetNumberOfGaps(), 2);
        }

        [Test]
        public void Test_ValuesOfGaps()
        {
            int[] values = level.GetValuesOfGaps();
            Assert.AreEqual(values.Length, 2);
            Assert.IsTrue(firstRange.Contains(values[0]));
            Assert.IsTrue(firstRange.Select(x => secondRange.Select(y => x + y)).SelectMany(x => x).Contains(values[1]));
        }
    }
}
