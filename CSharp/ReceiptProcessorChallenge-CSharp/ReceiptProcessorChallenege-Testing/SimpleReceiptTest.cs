using Newtonsoft.Json;
using ReceiptProcessorChallenge_CSharp.Entities.Rules;
using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenege_Testing
{
    public class SimpleReceiptTest
    {
        Receipt targetReceipt;
        IRule alphaRule;
        IRule everyTwoItemsRule;
        IRule itemDescriptionRule;
        IRule purchaseDateRule;
        IRule purchaseTimeRule;
        IRule totalIsMultipleRule;
        IRule totalIsWholeRule;

        [SetUp]
        public void Setup()
        {
            using(StreamReader r = new("Json/SimpleReceipt.json"))
            {
                string json = r.ReadToEnd();
                targetReceipt = JsonConvert.DeserializeObject<Receipt>(json);
            }

            alphaRule = new AlphanumericRetailerNameRule();
            everyTwoItemsRule = new EveryTwoItemsRule();
            itemDescriptionRule = new ItemDescriptionLengthRule();
            purchaseDateRule = new PurchaseDateRule();
            purchaseTimeRule = new PurchaseTimeRule();
            totalIsMultipleRule = new TotalIsMultipleRule();
            totalIsWholeRule = new TotalWholeDollarRule();
        }

        [Test]
        public void TargetReceiptAlphanumericRetailerName()
        {
            Assert.That(alphaRule.CalculatePoints(targetReceipt), Is.EqualTo(6));
        }

        [Test]
        public void TargetReceiptEveryTwoItems()
        {
            Assert.That(everyTwoItemsRule.CalculatePoints(targetReceipt), Is.EqualTo(0));
        }

        [Test]
        public void TargetReceiptItemDescriptionLength()
        {
            Assert.That(itemDescriptionRule.CalculatePoints(targetReceipt), Is.EqualTo(0));
        }

        [Test]
        public void TargetReceiptPurchaseDate()
        {
            Assert.That(purchaseDateRule.CalculatePoints(targetReceipt), Is.EqualTo(0));
        }

        [Test]
        public void TargetReceiptPurchaseTime()
        {
            Assert.That(purchaseTimeRule.CalculatePoints(targetReceipt), Is.EqualTo(0));
        }

        [Test]
        public void TargetReceiptTotalIsMultiple()
        {
            Assert.That(totalIsMultipleRule.CalculatePoints(targetReceipt), Is.EqualTo(25));
        }

        [Test]
        public void TargetReceiptTotalIsWholeDollar()
        {
            Assert.That(totalIsWholeRule.CalculatePoints(targetReceipt), Is.EqualTo(0));
        }
    }
}
