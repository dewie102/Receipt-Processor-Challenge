using System.ComponentModel.DataAnnotations;

namespace ReceiptProcessorChallenge_CSharp.Models
{
    public class Point
    {
        [Key]
        public string Id { get; set; }
        public int Points { get; set; }
    }
}
