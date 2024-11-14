using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    // 25 points if the total is a multiple of 0.25.
    public class TotalIsMultipleRule : IRule
    {
        public int PointsRewarded { get; set; } = 25;

        private float multipleOf = 0.25f;
        private float tolerence = 0.0001f;

        public int CalculatePoints(Receipt receipt)
        {
            int result = 0;
            float.TryParse(receipt.Total, out float total);

            float number = total % multipleOf;
            if(Math.Abs(number - 0) < tolerence)
            {
                result = PointsRewarded;
            }

            return result;
        }
    }
}
