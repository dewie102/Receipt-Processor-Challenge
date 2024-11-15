using Newtonsoft.Json;
using ReceiptProcessorChallenge_CSharp.Entities;

namespace ReceiptProcessorChallenge_CSharp.Models
{
    public class Receipt
    {
        [JsonProperty(PropertyName = "retailer")]
        public required string Retailer { get; set; }

        [JsonProperty(PropertyName = "purchaseDate")]
        public required string PurchaseDate { get; set; }

        [JsonProperty(PropertyName="purchaseTime")]
        public required string PurchaseTime { get; set; }

        [JsonProperty(PropertyName = "items")]
        [JsonConverter(typeof(ItemCustomValidationConverter))]
        public required List<Item> Items { get; set; }

        [JsonProperty(PropertyName = "total")]
        public required string Total { get; set; }
    }
}
