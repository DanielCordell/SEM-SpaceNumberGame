using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using NCalc;

namespace MathsTests
{
    public class LevelConstructorTests
    {
        [Test]
        public void Test_ThrowsExceptionsOnBadInput()
        {
            // This should throw, as there needs to be one more item in the visible boolean list (last param) than there are number of number generators (first param). This has one of each.
            Assert.Throws(typeof(ArgumentException), () => new Level(1, new List<int>[] { new List<int> { 1,3 } }, null, new List<bool> { false }));

            // This should throw, as the number of operators (second param) should be equal to the number of number generators (first param). This has 2 operators but one generator.
            Assert.Throws(typeof(ArgumentException), () => new Level(1, new List<int>[] { new List<int> { 1, 3 } }, new Operator[] { Operator.Add, Operator.Equals }, new List<bool> { false, true }));

            // This should throw, as there needs to be at least one false value in the visible boolean list.
            Assert.Throws(typeof(ArgumentException), () => new Level(1, new List<int>[] { new List<int> { 1, 3 } }, new Operator[] { Operator.Add, Operator.Equals }, new List<bool> { true, true }));

            // This should throw, as the last item in the operator list must be equals!.
            Assert.Throws(typeof(ArgumentException), () => new Level(1, new List<int>[] { new List<int> { 1, 3 } }, new Operator[] { Operator.Add, Operator.Divide }, new List<bool> { true, true }));
        }

        [Test]
        public void Test_StatementStringCorrect()
        {
            Level level = new Level(1, new List<int>[] { new List<int> { 2 }, new List<int> { 4 }, new List<int> { 2 }, new List<int> { 5 }, new List<int> { 1 } }, new Operator[] { Operator.Add, Operator.Divide, Operator.Multiply, Operator.Subtract, Operator.Equals }, new List<bool> { false, false, false, false, false, false });
            Assert.AreEqual("2+4/2*5-1=11", level.statementString);
        }

        [Test]
        public void Test_FinalQuestionNumberIsAnswer()
        {
            Level level = new Level(1, new List<int>[] { new List<int> { 2 }, new List<int> { 4 }, new List<int> { 2 }, new List<int> { 5 }, new List<int> { 1 } }, new Operator[] { Operator.Add, Operator.Divide, Operator.Multiply, Operator.Subtract, Operator.Equals }, new List<bool> { false, false, false, false, false, false });
            int result = Convert.ToInt32(new Expression("2+4/2*5-1").Evaluate());
            Assert.AreEqual(result, level.questionNumbers[level.questionNumbers.Length - 1]);
        }


        [Test]
        public void Test_FixDivision()
        {
            Level level = new Level(1, new List<int>[] { new List<int> { 2 }, new List<int> { 1 }, new List<int> { 5 } }, new Operator[] { Operator.Divide, Operator.Divide, Operator.Equals }, new List<bool> { false, false, false, false });
            Assert.IsTrue(level.operatorsUsed.Count(it => it == Operator.Divide) < 2);
        }
    }
}
