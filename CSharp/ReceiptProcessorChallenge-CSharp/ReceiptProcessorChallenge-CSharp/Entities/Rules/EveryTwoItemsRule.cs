using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    // 5 points for every two items on the receipt.
    public class EveryTwoItemsRule : IRule
    {
        public int PointsRewarded { get; set; } = 5;

        public int CalculatePoints(Receipt receipt)
        {
            int result = PointsRewarded * (receipt.Items.Count / 2);

            return result;
        }
    }
}
