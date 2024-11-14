using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities.Rules
{
    // 10 points if the time of purchase is after 2:00pm and before 4:00pm.
    public class PurchaseTimeRule : IRule
    {
        public int PointsRewarded { get; set; } = 10;

        int startTimeHour = 14;
        int endTimeHour = 16;

        public int CalculatePoints(Receipt receipt)
        {
            int result = 0;

            string[] timeParts = receipt.PurchaseTime.Split(':');

            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);

            if((hour >= startTimeHour && minute > 0) && (hour < endTimeHour))
            {
                result = PointsRewarded;
            }

            return result;
        }
    }
}
