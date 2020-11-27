namespace AccountLibTests
{
    using AccountLib;
    using Moq;
    using NUnit.Framework;
    using System.Security.Cryptography.X509Certificates;

    public class SavingsAccountTests
    {
        private Mock<SavingsAccount> partiallyMockedaccount;

        [SetUp]
        public void Setup()
        {
            partiallyMockedaccount = new Mock<SavingsAccount>();
        }

        [TearDown]
        public void Clean()
        {
            partiallyMockedaccount = null;
        }

        [Test]
        public void TestDepositWhenCurrentBalanceIs5000INR()
        {
            partiallyMockedaccount.SetupSequence(x => x.GetBalance())
                .Returns(5000.00)
                .Returns(15000.00);

            double actualBalance = partiallyMockedaccount.Object.Deposit(10000.00);
            double expectedBalance = 15000.00;

            Assert.That (actualBalance, Is.EqualTo(expectedBalance));
            partiallyMockedaccount.Verify(x => x.GetBalance(), Times.Once());
            partiallyMockedaccount.Verify(x => x.UpdateBalanceInDB(It.IsAny<double>()), Times.Once);

            actualBalance = partiallyMockedaccount.Object.Deposit(2000.00);
            expectedBalance = 17000.00;

            Assert.That(actualBalance, Is.EqualTo(expectedBalance));
            partiallyMockedaccount.Verify(x => x.GetBalance(), Times.AtLeastOnce());
            partiallyMockedaccount.Verify(x => x.UpdateBalanceInDB(It.IsAny<double>()), Times.AtLeastOnce);

        }

        [Test]
        public void TestDepositWhenCurrentBalanceIs15000INR()
        {
            partiallyMockedaccount.Setup(x => x.GetBalance()).Returns(15000.00);

            double actualBalance = partiallyMockedaccount.Object.Deposit(2000.00);
            double expectedBalance = 17000.00;

            Assert.That(actualBalance, Is.EqualTo(expectedBalance));
            partiallyMockedaccount.Verify(x => x.GetBalance(), Times.Once());
            partiallyMockedaccount.Verify(x => x.UpdateBalanceInDB(It.IsAny<double>()), Times.Once);
        }
    }
}
