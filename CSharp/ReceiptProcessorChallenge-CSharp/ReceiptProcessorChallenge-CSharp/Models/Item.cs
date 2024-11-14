using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

using Newtonsoft.Json;

namespace ReceiptProcessorChallenge_CSharp.Models
{
    public class Item
    {
        private string shortDescriptionRegexPattern = @"^[\w\s\-]+$";
        private string priceRegexPattern = @"^\d+\.\d{2}$";

        private string shortDescription = "";
        private string price = "";

        /*public Item()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }*/

        [JsonProperty(PropertyName= "shortDescription")]
        public required string ShortDescription {
            get
            {
                return shortDescription;
            }
            set
            {
                Regex r = new Regex(shortDescriptionRegexPattern);
                if(r.Match(value).Success)
                {
                    shortDescription = value;
                }
                else
                {
                    Console.Error.WriteLine($"Short Description didn't match proper regex: {value}");
                    shortDescription = "";
                }
            }
        }
        [JsonProperty(PropertyName = "price")]
        public required string Price {
            get
            {
                return price;
            }
            set
            {
                Regex r = new Regex(priceRegexPattern);
                if(r.Match(value).Success)
                {
                    price = value;
                }
                else
                {
                    Console.Error.WriteLine($"Retailer didn't match proper regex: {value}");
                    price = "";
                }
            }
        }
    }
}
