using System.ComponentModel.DataAnnotations;

namespace ApiRestRoulette.Models
{
    public class User
    {
        [Key]
        public required string Name { get; set; }
        public decimal Cash { get; set; }
    }
}
