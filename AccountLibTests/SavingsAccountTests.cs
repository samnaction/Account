namespace AccountLibTests
{
    using AccountLib;
    using Moq;
    using NUnit.Framework;
    using System;

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

            Assert.That(actualBalance, Is.EqualTo(expectedBalance));
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

        [Test]
        public void TestWithdraw1000INRWhenCurrentBalanceIs900INR()
        {
            partiallyMockedaccount.Setup(x => x.GetBalance()).Returns(900.00);

            Assert.Throws(Is.TypeOf<Exception>().And.Message.EqualTo("Insufficient Balance"),
                delegate
                {
                    partiallyMockedaccount.Object.Withdraw(1000.00);
                });

            partiallyMockedaccount.Verify(x => x.GetBalance(), Times.Once);
            partiallyMockedaccount.Verify(x => x.UpdateBalanceInDB(It.IsAny<double>()), Times.Never);
        }
    }
}
