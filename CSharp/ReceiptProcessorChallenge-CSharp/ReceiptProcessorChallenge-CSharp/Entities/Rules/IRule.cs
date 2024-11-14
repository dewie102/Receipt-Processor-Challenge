using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    public interface IRule
    {
        public int PointsRewarded {get; set;}
        public int CalculatePoints(Receipt receipt);
    }
}
