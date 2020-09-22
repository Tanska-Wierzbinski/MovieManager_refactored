using MovieManager.Application.DTOs;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Grade;
using MovieManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorService _actorService;

        public ActorService(IActorService actorService)
        {
            _actorService = actorService;
        }

        public Task<ActorAddDto> AddGet()
        {
            throw new NotImplementedException();
        }

        public Task AddGrade(GradeAddDto grade)
        {
            throw new NotImplementedException();
        }

        public Task AddPost(ActorAddDto actor)
        {
            throw new NotImplementedException();
        }

        public Task<ActorEditDto> EditGet(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditPost(ActorEditDto actor)
        {
            throw new NotImplementedException();
        }

        public Task<ActorIndexDto> GetAllForIndex(GenderDto? gender, int yearMin, int yearMax, int gradeMin, int gradeMax, string[] countries, string sortOrder, int? pageNumber, int pageSize = 5)
        {
            throw new NotImplementedException();
        }

        public Task<ActorDto> GetById()
        {
            throw new NotImplementedException();
        }

        public Task<ActorDetailsDto> GetDetails(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
