using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    // One point for every alphanumeric character in the retailer name.
    public class AlphanumericRetailerNameRule : IRule
    {
        public int PointsRewarded { get; set; } = 1;

        public int CalculatePoints(Receipt receipt)
        {
            int result = 0;

            foreach(char character in receipt.Retailer)
            {
                if(char.IsLetterOrDigit(character))
                {
                    result += PointsRewarded;
                }
            }

            return result;
        }
    }
}
