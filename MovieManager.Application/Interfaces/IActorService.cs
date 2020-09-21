using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Interfaces
{
    public interface IActorService
    {
        Task<IEnumerable<ActorResultDto>> GetAllForIndex();
        Task<ActorDetailsResultDto> GetDetails(int id);
        Task<ActorAddDto> AddGet(); 
        Task AddPost(ActorAddDto actor);
        Task<ActorEditDto> EditGet(int id);
        Task EditPost(ActorEditDto actor);
        Task AddGrade(GradeAddDto grade);
        Task<ActorDto> GetById();
        Task<bool> Remove(int id);
    }
}
