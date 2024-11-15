using Newtonsoft.Json;

namespace ReceiptProcessorChallenge_CSharp.Models
{
    public class Item
    {
        [JsonProperty(PropertyName= "shortDescription")]
        public required string ShortDescription { get; set; }

        [JsonProperty(PropertyName = "price")]
        public required string Price { get; set; }
    }
}
