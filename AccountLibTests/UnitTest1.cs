using AccountLib;
using NUnit.Framework;

namespace AccountLibTests
{
    public class Tests
    {
        private SavingsAccount account;
        [SetUp]
        public void Setup()
        {
            account = new SavingsAccount();
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}