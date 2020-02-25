using System;
using NUnit.Framework;
using NCalc;

namespace MathsTests
{
    public class ExpressionParsingTests
    {
        [Test]
        public void Test_ExpressionParsedSimple()
        {
            int result = Convert.ToInt32(new Expression("5+4-2").Evaluate());
            Assert.AreEqual(7, result);
        }

        [Test]
        public void Test_ExpressionParsedComplex()
        {
            int result = Convert.ToInt32(new Expression("5+4-6/2+4*8/4").Evaluate());
            Assert.AreEqual(14, result);
        }

        [Test]
        public void Test_ExpressionThrowDiv0Exception()
        {
            Exception ex = Assert.Catch(() => Convert.ToInt32(new Expression("5/0").Evaluate()));
            Assert.IsTrue(ex.GetType() == typeof(OverflowException) || ex.GetType() == typeof(DivideByZeroException));
        }
    }
}
