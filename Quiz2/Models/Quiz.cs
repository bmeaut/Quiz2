﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public class Quiz
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual ApplicationUser Owner { get; set; }
        public virtual ICollection<Game> Games { get; set; }

    }
}