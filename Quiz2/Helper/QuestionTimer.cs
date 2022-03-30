using System.Collections.Concurrent;
using Microsoft.AspNetCore.SignalR;
using Quiz2.Hubs;
using Quiz2.Services;

namespace Quiz2.Helper
{
    public class QuestionTimer : System.Timers.Timer
    {
        public static ConcurrentDictionary<string, QuestionTimer> Timers = new ConcurrentDictionary<string, QuestionTimer>();

            public QuestionTimer(double interval)
                : base(interval)
            {
            }
            
            public IHubCallerClients hubCallerClients { get; set; }
        
    }
}