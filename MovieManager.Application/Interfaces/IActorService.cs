﻿using MovieManager.Application.DTOs;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Grade;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface IActorService
    {
        ActorIndexDto GetAllForIndex(GenderDto? gender, int yearMin, int yearMax, int gradeMin, int gradeMax, string[] countries, string sortOrder, int? pageNumber, int pageSize = 5);
        Task<ActorDetailsDto> GetDetails(int id);
        ActorAddDto AddGet(); 
        Task AddPost(ActorAddDto actor);
        Task<ActorEditDto> EditGet(int id);
        Task EditPost(ActorEditDto actor);
        Task AddGrade(GradeAddDto grade);
        Task<ActorDto> GetById(int id);
        Task<bool> Remove(int id);
        Task DeleteImage(string imageName);
    }
}
