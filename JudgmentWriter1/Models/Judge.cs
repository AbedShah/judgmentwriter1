using System.ComponentModel.DataAnnotations;

namespace JudgmentWriter1.Models
{
    public class Judge
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string Username { get; set; }
        [MaxLength(10)]
        public string Gender { get; set; }
        public int Password { get; set; }
    }
}
