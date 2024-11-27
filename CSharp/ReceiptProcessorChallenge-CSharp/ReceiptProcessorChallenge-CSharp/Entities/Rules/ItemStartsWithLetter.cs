using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    public class ItemStartsWithLetter : IRule
    {
        public int PointsRewarded { get; set; } = 3;

        private char letter = 'a';

        public int CalculatePoints(Receipt receipt)
        {
            int result = 0;

            foreach(Item item in receipt.Items)
            {
                if(item.ShortDescription.ToLower()[0] == letter)
                {
                    result += PointsRewarded;
                }
            }

            return result;
        }
    }
}
