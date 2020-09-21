using MovieManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MovieManager.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorService _actorService;

        public ActorService(IActorService actorService)
        {
            _actorService = actorService;
        }
    }
}
