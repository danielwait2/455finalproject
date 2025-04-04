using System.ComponentModel.DataAnnotations;

namespace WaterProject.API.Data
{
    public class Recommendation
    {
        [Key]
        public string? ArticleId { get; set; }
        [Required]
        public string? Recommendation1 { get; set; }
        public string? Recommendation2 { get; set; }
        public string? Recommendation3 { get; set; }
        public string? Recommendation4 { get; set; }
        public string? Recommendation5 { get; set; }

    }
}
