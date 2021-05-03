using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quiz2.Models
{
    public enum GameStatuses
    {
        Created,
        Started,
        Finished
    }  
    public class Game
    {
        [Key]
        public int Id { get; set; }
        
        public int QuizId { get; set; }
        [Required]
        public Quiz Quiz { get; set; }
        
        public int? CurrentQuestionId { get; set; }
        public Question CurrentQuestion { get; set; }
        
        public string OwnerId { get; set; }
        public ApplicationUser Owner { get; set; }
        
        public ICollection<ApplicationUser> JoinedUsers { get; set; }
        public string JoinId { get; set; }

        public GameStatuses Status { get; set; }
        
        public DateTime CurrentQuestionStarted { get; set; }
    }
}