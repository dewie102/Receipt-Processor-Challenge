using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    // If the trimmed length of the item description is a multiple of 3,
    // multiply the price by 0.2 and round up to the nearest integer.
    // The result is the number of points earned.
    public class ItemDescriptionLengthRule : IRule
    {
        public int PointsRewarded { get; set; } = 0;

        private int multipleOf = 3;
        private float multiplyBy = 0.2f;

        public int CalculatePoints(Receipt receipt)
        {
            int result = 0;
            foreach(Item item in receipt.Items)
            {
                if(item.ShortDescription.Trim().Length % multipleOf == 0)
                {
                    result += (int)Math.Ceiling(double.Parse(item.Price) * multiplyBy);
                }
            }

            return result;
        }
    }
}
