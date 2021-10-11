﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quiz2.Data;
using Quiz2.DTO;
using Quiz2.Models;

namespace Quiz2.Services
{
    public class UserAnswerService : IUserAnswerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IGameService gameService;
        private readonly IAnswerService answerService;
        private readonly IApplicationUserService applicationUserService;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public UserAnswerService(
            ApplicationDbContext context, 
            IGameService gameService, 
            IAnswerService answerService,
            IApplicationUserService applicationUserService,
            IServiceScopeFactory serviceScopeFactory
            )
        {
            _context = context;
            this.gameService = gameService;
            this.answerService = answerService;
            this.applicationUserService = applicationUserService;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public UserAnswer CreateUserAnswer(string joinId, int answerId, string applicationUserId)
        {
            Console.WriteLine("joinId: " + joinId);
            Console.WriteLine("answerId: " + answerId);
            Console.WriteLine("applicationUserId: " + applicationUserId);
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
                    TimeOfSubmit = (DateTime.Now - game.CurrentQuestionStarted).TotalSeconds
                };
                _context.UserAnswers.Add(userAnswer);
                _context.SaveChanges();
                Console.WriteLine("user válasza: " + userAnswer.ApplicationUser.Id);
                return userAnswer;
            }

            Console.WriteLine("null");
            return null;
        }

        public List<int> getCurrentQuestionStat(string joinId, ApplicationDbContext applicationDbContext = null)
        {
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
            }

            var game = gameService.GetGameByJoinIdWithCurrentQuestion(joinId, applicationDbContext);
            var stats = applicationDbContext.UserAnswers
                .Where(userAnswer => userAnswer.GameId == game.Id)
                .Include(userAnswer => userAnswer.Answer)
                .Where(userAnswer => game.CurrentQuestion.Answers.Contains(userAnswer.Answer))
                .GroupBy(userAnswer => userAnswer.AnswerId)
                .OrderBy(g => g.Key)
                .Select(g => new {AnswerId = g.Key, Count = g.Count()})
                .ToList();

            var counts = new List<int>();
            game.CurrentQuestion.Answers.Sort((x, y) => x.Id.CompareTo(y.Id));


            foreach (var answer in game.CurrentQuestion.Answers)
            {

                var find = false;
                foreach (var stat in stats)
                {
                    if (stat.AnswerId == answer.Id)
                    {
                        find = true;
                        counts.Add(stat.Count);
                    }
                }

                if (!find)
                {
                    counts.Add(0);
                }

            }
            return counts;
        }

        public int getUserPoint(string joinId, string applicationUserId, ApplicationDbContext applicationDbContext = null)
        {
            if (applicationDbContext == null)
            {
                applicationDbContext = _context;
            }
            var game = gameService.GetGameByJoinId(joinId);
            return applicationDbContext.UserAnswers
                .Where(userAnswer => userAnswer.GameId == game.Id)
                .Where(userAnswer => userAnswer.ApplicationUserId == applicationUserId)
                .Include(userAnswer => userAnswer.Answer)
                .Include(userAnswer => userAnswer.Answer.Question)
                .Where(userAnswer => userAnswer.Answer.Correct)
                .Sum(userAnswer => userAnswer.Answer.Question.Points);
            
        }

    }
}