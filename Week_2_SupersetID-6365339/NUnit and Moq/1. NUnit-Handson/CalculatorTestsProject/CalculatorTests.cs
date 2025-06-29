using NUnit.Framework;
using CalcLibrary; 

namespace CalculatorTestsProject
{
    [TestFixture]
    public class CalculatorTests
    {
        private SimpleCalculator calc;

        [SetUp]
        public void Setup()
        {
            
            calc = new SimpleCalculator();
        }

        [TearDown]
        public void TearDown()
        {
            
            calc = null;
        }

        [Test]
        [TestCase(10, 20, 30)]
        [TestCase(-5, 5, 0)]
        [TestCase(0, 0, 0)]
        public void Addition_WhenCalled_ReturnsCorrectSum(double a, double b, double expected)
        {
            double result = calc.Addition(a, b);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test, Ignore("Demo purpose: Test is ignored.")]
        public void ThisTestWillBeIgnored()
        {
            Assert.Fail("Should not run.");
        }
    }
}
