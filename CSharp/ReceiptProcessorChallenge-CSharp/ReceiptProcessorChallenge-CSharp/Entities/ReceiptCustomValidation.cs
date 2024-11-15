using System.Text.RegularExpressions;
using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities
{
    public class ReceiptCustomValidation
    {
        private static string retailerRegexPattern = @"^[\w\s\-&]+$";
        private static string totalRegexPattern = @"^\d+\.\d{2}$";

        public static bool IsValid(Receipt receipt)
        {
            // Validation Logic
            if(receipt == null || receipt.Retailer == null || receipt.PurchaseDate == null || receipt.PurchaseTime == null || receipt.Items == null || receipt.Total == null)
            {
                return false;
            }

            if(receipt.Retailer.Trim().Length == 0 || receipt.PurchaseDate.Trim().Length == 0 || receipt.PurchaseTime.Trim().Length == 0 || receipt.Total.Trim().Length == 0)
            {
                return false;
            }

            Regex r = new Regex(retailerRegexPattern);
            if(!r.Match(receipt.Retailer).Success)
            {
                return false;
            }

            r = new Regex(totalRegexPattern);
            if(!r.Match(receipt.Total).Success)
            {
                return false;
            }

            return true;
        }
    }
}
