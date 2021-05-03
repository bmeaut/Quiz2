﻿using System;
using Quiz2.Data;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class UserAnswerService: IUserAnswerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGameService gameService;
        private readonly IAnswerService answerService;
        private readonly IApplicationUserService applicationUserService;

        public UserAnswerService(ApplicationDbContext context, IGameService gameService, IAnswerService answerService, IApplicationUserService applicationUserService)
        {
            _context = context;
            this.gameService = gameService;
            this.answerService = answerService;
            this.applicationUserService = applicationUserService;
        }
        public UserAnswer CreateUserAnswer(string joinId, int answerId, string applicationUserId)
        {
            Console.WriteLine("joinId: "+joinId);
            Console.WriteLine("answerId: "+answerId);
            Console.WriteLine("applicationUserId: "+applicationUserId);
            var game = gameService.GetGameByJoinId(joinId);
            var answer = answerService.GetAnswer(answerId);
            var user = applicationUserService.GetUser(applicationUserId);
            if (game != null && answer != null && user != null)
            {
                var userAnswer = new UserAnswer() 
                {
                    Game = game,
                    ApplicationUserId = applicationUserId,
                    Answer = answer,
                    TimeOfSubmit = (DateTime.Now-game.CurrentQuestionStarted).TotalSeconds
                };
                _context.UserAnswers.Add(userAnswer);
                _context.SaveChanges();
                Console.WriteLine("user válasza: "+userAnswer.ApplicationUser.Id);
                return userAnswer;
            }
            Console.WriteLine("null");
            return null;
        }
    }
}