using AutoMapper;
using MovieManager.Application.DTOs;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Category;
using MovieManager.Application.DTOs.Grade;
using MovieManager.Application.DTOs.Movie;
using MovieManager.Application.DTOs.Review;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MovieManager.Application.Configuration
{
    public class AutomapperConfig : Profile
    {
        public AutomapperConfig()
        {
            CreateMap<Actor, ActorAddDto>().ForMember(a=>a.ImageFile, src=>src.Ignore()).ReverseMap();
            CreateMap<Actor, ActorDto>().ReverseMap();
            CreateMap<Actor, ActorIndexDto>().ReverseMap();
            CreateMap<Actor, ActorDetailsDto>()
                .ForMember(a => a.Movies, src => src.MapFrom(src => src.MovieActors.Select(src => src.Movie)))
                .ReverseMap();
            CreateMap<Actor, ActorEditDto>()
                .ForMember(a => a.ImageFile, src => src.Ignore())
                .ForMember(a=>a.MovieIds, src=>src.MapFrom(src=>src.MovieActors.Select(src=>src.MovieId)))
                .ReverseMap();

            CreateMap<Review, ReviewAddDto>().ReverseMap();
            CreateMap<Review, ReviewDto>().ReverseMap();

            CreateMap<Movie, MovieDto>().ForMember(m=>m.Id, src=>src.MapFrom(src => src.Id)).ReverseMap();
            CreateMap<Movie, MovieAddDto>()
                .ForMember(m => m.ImageFile, src => src.Ignore())
                .ReverseMap();
            CreateMap<Movie, MovieIndexDto>().ReverseMap();
            CreateMap<Movie, MovieDetailsDto>()
                .ForMember(m=>m.Actors, src=>src.MapFrom(src=>src.MovieActors.Select(src=>src.Actor)))
                .ForMember(m=>m.Categories,src=>src.MapFrom(src=>src.MovieCategories.Select(src=>src.Category)))
                .ReverseMap();
            CreateMap<Movie, MovieEditDto>()
                .ForMember(m => m.ImageFile, src => src.Ignore())
                .ForMember(m=>m.ActorIds, src=>src.MapFrom(src=>src.MovieActors.Select(src=>src.ActorId)))
                .ForMember(m=>m.CategoryIds,src=>src.MapFrom(src=>src.MovieCategories.Select(src=>src.CategoryId)))
                .ReverseMap();

            CreateMap<Category, CategoryAddDto>().ReverseMap();
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<Category, CategoryDetailsDto>().ReverseMap();

            CreateMap<Grade, GradeAddDto>().ReverseMap();
            CreateMap<Grade, GradeDto>().ReverseMap();

        }
    }
}
