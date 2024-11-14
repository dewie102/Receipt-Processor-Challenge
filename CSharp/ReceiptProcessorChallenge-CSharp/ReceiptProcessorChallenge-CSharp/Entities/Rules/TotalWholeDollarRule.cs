using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    // 50 points if the total is a round dollar amount with no cents.
    public class TotalWholeDollarRule : IRule
    {
        public int PointsRewarded { get; set; } = 50;

        private float tolerance = 0.0001f;

        public int CalculatePoints(Receipt receipt)
        {
            int result = 0;
            float.TryParse(receipt.Total, out float total);

            float number = total % 1;
            if(Math.Abs(number - 0) < tolerance)
            {
                result = PointsRewarded;
            }

            return result;
        }
    }
}
