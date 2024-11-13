using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

namespace ReceiptProcessorChallenge_CSharp.Models
{
    public class Receipt
    {
        private string retailerRegexPattern = @"^[\w\s\-&]+$";
        private string totalRegexPattern = @"^\d+\.\d{2}$";

        private string retailer = "";
        private string total = "";

        [Key]
        public string Id { get; set; } = "";

        [JsonProperty(PropertyName = "retailer")]
        public required string Retailer {
            get
            {
                return retailer;
            }
            set
            {
                Regex r = new Regex(retailerRegexPattern);
                if(r.Match(value).Success)
                {
                    retailer = value;
                } else
                {
                    Console.Error.WriteLine($"Retailer didn't match proper regex: {value}");
                    retailer = "";
                }
            }
        }

        [JsonProperty(PropertyName = "purchaseDate")]
        public required string PurchaseDate { get; set; }

        [JsonProperty(PropertyName="purchaseTime")]
        public required string PurchaseTime { get; set; }

        [JsonProperty(PropertyName = "items")]
        public required List<Item> Items { get; set; }

        [JsonProperty(PropertyName = "total")]
        public required string Total {
            get
            {
                return total;
            }
            set
            {
                Regex r = new Regex(totalRegexPattern);
                if(r.Match(value).Success)
                {
                    total = value;
                } else
                {
                    Console.Error.WriteLine($"Total didn't match proper regex: {value}");
                }
            }
        }
    }
}
