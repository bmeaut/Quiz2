using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<ApplicationUser> Owners { get; set; }
        public ICollection<Game> Games { get; set; }

    }
}