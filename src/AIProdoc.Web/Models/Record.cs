using System.ComponentModel.DataAnnotations;

namespace AIProdoc.Web.Models
{
    public class Record
    {
        public int Id { get; set; }

        [Required, StringLength(120)]
        public string Name { get; set; } = string.Empty;

        [StringLength(512)]
        public string? Description { get; set; }

        [Range(0, 1_000_000)]
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
    }
}
