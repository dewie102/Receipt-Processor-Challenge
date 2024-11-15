using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReceiptProcessorChallenge_CSharp.Models;

namespace ReceiptProcessorChallenge_CSharp.Entities
{
    public class ItemCustomValidationConverter : JsonConverter
    {
        private string shortDescriptionRegexPattern = @"^[\w\s\-]+$";
        private string priceRegexPattern = @"^\d+\.\d{2}$";

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Item);
        }

        public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
        {
            // Deserialize the Object
            List<Item>? items = serializer.Deserialize<List<Item>>(reader);

            ObjectResult error = new("The receipt is invalid")
            {
                StatusCode = 400
            };

            if(items == null)
            {
                return error;
            }

            foreach(Item item in items)
            {
                // Validation
                if(item == null || item.ShortDescription == null || item.Price == null)
                {
                    return error;
                }

                Regex r = new Regex(shortDescriptionRegexPattern);
                if(!r.Match(item.ShortDescription).Success)
                {
                    return error;
                }

                r = new Regex(priceRegexPattern);
                if(!r.Match(item.Price).Success)
                {
                    return error;
                }
            }

            return items;
        }

        public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
