using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Buffers;
using MovieManager.Application.DTOs;
using MovieManager.Application.DTOs.Actor;
using MovieManager.Application.DTOs.Grade;
using MovieManager.Application.Interfaces;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class ActorService : IActorService
    {
        private readonly IActorRepository _actorRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IMovieActorRepository _movieActorRepository;
        private readonly IMapper _mapper;

        public ActorService(IActorRepository actorRepository, IMovieRepository movieRepository, IGradeRepository gradeRepository, IMovieActorRepository movieActorRepository, IMapper mapper)
        {
            _actorRepository = actorRepository;
            _movieRepository = movieRepository;
            _gradeRepository = gradeRepository;
            _movieActorRepository = movieActorRepository;
            _mapper = mapper;
        }

        public ActorAddDto AddGet()
        {
            return new ActorAddDto()
            {
                Movies = _movieRepository.GetAll().Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }).ToList()
            };
        }

        public async Task AddPost(ActorAddDto actor)
        {
            var newActor = _mapper.Map<Actor>(actor);

            await _actorRepository.UploadImage(actor.ImageFile, newActor);
            await _actorRepository.Add(newActor);

            foreach (var movie in actor.MovieIds)
            {
                await _movieActorRepository.Add(new MovieActor() { MovieId = movie, ActorId = newActor.Id });
            }

            //foreach (var movie in actor.MovieIds)
            //{
            //    var newMovieActor = new MovieActor()
            //    {
            //        MovieId = movie,
            //        ActorId = newActor.Id
            //    };
            //    await _movieActorRepository.Add(newMovieActor);
            //}                                                                 DO PRZETESTOWANIA Z MAPPEREM
        }

        public async Task AddGrade(GradeAddDto grade)
        {
            await _gradeRepository.Add(_mapper.Map<Grade>(grade));
        }

        public async Task<ActorEditDto> EditGet(int id)
        {
            //var result = _mapper.Map<ActorEditDto>(await _actorRepository.GetById(id));
            //result.Movies = _movieRepository.GetAll().Select(m => new SelectListItem { Text = m.Name, Value = m.Id.ToString() }).ToList();
            //result.MovieIds = _movieActorRepository.GetAll().Where(ma=>ma.ActorId == id).Select(ma=>ma.MovieId).ToArray();

            return _mapper.Map<ActorEditDto>(await _actorRepository.GetById(id));
        }

        public async Task EditPost(ActorEditDto actor)
        {
            var editedActor = _mapper.Map<Actor>(actor);
            await _actorRepository.Update(editedActor);
            await _actorRepository.UploadImage(actor.ImageFile, editedActor);

            var movieActors = await _movieActorRepository.Search(ma => ma.ActorId == editedActor.Id);
            foreach (var ma in movieActors)
            {
                await _movieActorRepository.Remove(ma);
            }

            foreach (var movie in actor.MovieIds)
            {
                await _movieActorRepository.Add(new MovieActor() { MovieId = movie, ActorId = editedActor.Id });
            }

            //foreach (var movie in actor.MovieIds)
            //{
            //    var newMovieActor = new MovieActor()
            //    {
            //        MovieId = movie,
            //        ActorId = editedActor.Id
            //    };
            //    await _movieActorRepository.Add(newMovieActor);
            //}                                                                   DO PRZETESTOWANIA Z MAPPEERM
        }

        public ActorIndexDto GetAllForIndex(GenderDto? gender, int yearMin, int yearMax, int gradeMin, int gradeMax, string[] countries, string sortOrder, int? pageNumber, int pageSize = 5)
        {
            ActorIndexDto actorsForIndex = new ActorIndexDto()
            {
                Actors = _actorRepository.GetAll().ProjectTo<ActorDto>(_mapper.ConfigurationProvider).AsEnumerable(),
                GradeMin = gradeMin,
                GradeMax = gradeMax == 0 ? 10 : gradeMax,
                YearMax = yearMax == 0 ? 2100 : yearMax,
                YearMin = yearMin == 0 ? yearMax : yearMin,
                Gender = gender,
                Countries = countries,
                SortOrder = sortOrder,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            Filter(actorsForIndex);
            Sort(actorsForIndex);

            actorsForIndex.PaginatedActors = PaginatedList<ActorDto>.Create(actorsForIndex.Actors.AsQueryable(), pageNumber ?? 1, pageSize);

            return actorsForIndex;
        }

        //---------------------------------------------------------------------------------------
        private class ActorDtoComparer : IEqualityComparer<ActorDto>
        {
            public bool Equals([AllowNull] ActorDto first, [AllowNull] ActorDto second)
            {
                if (first.Country == second.Country)
                    return true;

                return false;
            }

            public int GetHashCode([DisallowNull] ActorDto obj)
            {
                return obj.Id.GetHashCode();
            }
        }
        //---------------------------------------------------------------------------------------

        private void Filter(ActorIndexDto actorsForIndex)
        {
            if (actorsForIndex.Countries.Length != 0)
            {
                foreach (var country in actorsForIndex.Countries)
                {
                    actorsForIndex.Actors = actorsForIndex.Actors.Intersect(actorsForIndex.Actors.Where(a => a.Country == country), new ActorDtoComparer());
                }
            }

            IEnumerable<ActorDto> filter;

            if (actorsForIndex.Gender != null)
            {
                filter = actorsForIndex.Actors.Where(m => m.Gender == actorsForIndex.Gender)
                                                 .Where(m => m.BornDate.Year >= actorsForIndex.YearMin && m.BornDate.Year <= actorsForIndex.YearMax);
                //actorsForIndex.Actors = filter.Where(m => m.Grades.Any())
                //                                .Where(m => m.Grades
                //                                    .Average(m => m.GradeValue) >= actorsForIndex.GradeMin && m.Grades
                //                                        .Average(m => m.GradeValue) < actorsForIndex.GradeMax + 1);
                // .Union(filter.Where(a => a.Grades.Any() == false));
            }
            else
            {
                filter = actorsForIndex.Actors.Where(m => m.BornDate.Year >= actorsForIndex.YearMin && m.BornDate.Year <= actorsForIndex.YearMax);

                //actorsForIndex.Actors = filter.Where(m => m.Grades.Any())
                //                                 .Where(m => m.Grades
                //                                    .Average(m => m.GradeValue) >= actorsForIndex.GradeMin && m.Grades
                //                                        .Average(m => m.GradeValue) < actorsForIndex.GradeMax + 1);
                //  .Union(filter.Where(a => a.Grades.Any() == false));
            }

            actorsForIndex.Actors = filter.Where(m => m.Grades.Any())
                                                .Where(m => m.Grades
                                                    .Average(m => m.GradeValue) >= actorsForIndex.GradeMin && m.Grades
                                                        .Average(m => m.GradeValue) < actorsForIndex.GradeMax + 1);

            if (actorsForIndex.GradeMin == 0)
            {
                actorsForIndex.Actors = actorsForIndex.Actors.Union(filter.Where(a => a.Grades.Any() == false));
            }

            //actorsForIndex.Actors = actorsForIndex.Gender != null ? actorsForIndex.Actors.Where(m => m.Gender == actorsForIndex.Gender)
            //                                                                             .Where(m => m.BornDate.Year >= actorsForIndex.YearMin && m.BornDate.Year <= actorsForIndex.YearMax)//;
            //                                                                             .Where(m => m.Grades.Any())
            //                                                                                .Where(m => m.Grades
            //                                                                                    .Average(m => m.GradeValue) >= actorsForIndex.GradeMin && m.Grades
            //                                                                                        .Average(m => m.GradeValue) < actorsForIndex.GradeMax + 1)
            //                                                      : actorsForIndex.Actors.Where(m => m.BornDate.Year >= actorsForIndex.YearMin && m.BornDate.Year <= actorsForIndex.YearMax)//;
            //                                                                             .Where(m => m.Grades.Any())
            //                                                                                .Where(m => m.Grades
            //                                                                                    .Average(m => m.GradeValue) >= actorsForIndex.GradeMin && m.Grades
            //                                                                                        .Average(m => m.GradeValue) < actorsForIndex.GradeMax + 1);
            //return actorsForIndex;

        }

        private void Sort(ActorIndexDto actorsForIndex)
        {
            switch (actorsForIndex.SortOrder)
            {
                case "nameDesc":
                    actorsForIndex.Actors = actorsForIndex.Actors.OrderByDescending(m => m.LastName);
                    break;
                case "bornDesc":
                    actorsForIndex.Actors = actorsForIndex.Actors.OrderByDescending(m => m.BornDate);
                    break;
                case "born":
                    actorsForIndex.Actors = actorsForIndex.Actors.OrderBy(m => m.BornDate);
                    break;
                case "gradeDesc":
                    actorsForIndex.Actors = actorsForIndex.Actors.Where(m => m.Grades.Any()).OrderByDescending(m => m.Grades.Average(k => k.GradeValue)).Union(actorsForIndex.Actors);
                    break;
                case "grade":
                    actorsForIndex.Actors = actorsForIndex.Actors.Where(m => m.Grades.Any()).OrderBy(m => m.Grades.Average(k => k.GradeValue)).Union(actorsForIndex.Actors);
                    break;
                case "quantityGradeDesc":
                    actorsForIndex.Actors = actorsForIndex.Actors.Where(m => m.Grades.Any()).OrderByDescending(m => m.Grades.Count()).Union(actorsForIndex.Actors);
                    break;
                case "quantityGrade":
                    actorsForIndex.Actors = actorsForIndex.Actors.Where(m => m.Grades.Any()).OrderBy(m => m.Grades.Count()).Union(actorsForIndex.Actors);
                    break;
                default:
                    actorsForIndex.Actors = actorsForIndex.Actors.OrderBy(m => m.LastName);
                    break;
            }
        }
        public async Task<ActorDto> GetById(int id)
        {
            return _mapper.Map<ActorDto>(await _actorRepository.GetById(id));
        }

        public async Task<ActorDetailsDto> GetDetails(int id)
        {
            return _mapper.Map<ActorDetailsDto>(await _actorRepository.GetById(id));
        }

        public async Task<bool> Remove(int id)
        {
            await _actorRepository.Remove(await _actorRepository.GetById(id));
            return true;
        }

        public async Task DeleteImage(string imageName)
        {
            await _actorRepository.DeleteImage(imageName);
        }
    }
}
