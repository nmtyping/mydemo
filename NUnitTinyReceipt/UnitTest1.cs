using NUnit.Framework;
using TinyReceipt;

namespace NUnitTinyReceipt
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TaxRoundingTest()
        {
            Assert.AreEqual(ReceiptService.RoundingTax(0.035M), 0.05M);
            Assert.AreEqual(ReceiptService.RoundingTax(0.045M), 0.05M);
            Assert.AreEqual(ReceiptService.RoundingTax(0.056M), 0.10M);
        }
    }
}