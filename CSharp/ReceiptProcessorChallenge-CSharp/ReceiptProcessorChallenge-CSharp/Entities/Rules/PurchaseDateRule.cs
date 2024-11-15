using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    // 6 points if the day in the purchase date is odd.
    public class PurchaseDateRule : IRule
    {
        public int PointsRewarded { get; set; } = 6;

        public int CalculatePoints(Receipt receipt)
        {
            int result = 0;

            string[] dateParts = receipt.PurchaseDate.Split('-');

            int day = int.Parse(dateParts[2]);

            if(day % 2 == 1)
            {
                result = PointsRewarded;
            }

            return result;
        }
    }
}
