using AutoMapper;
using MovieManager.Application.DTOs.Review;
using MovieManager.Application.Interfaces;
using MovieManager.Domain.Interfaces;
using MovieManager.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MovieManager.Application.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public ReviewService(IReviewRepository reviewRepository, IMapper mapper)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        public async Task AddPost(ReviewAddDto review)
        {
            await _reviewRepository.Add(_mapper.Map<Review>(review));
        }

        public async Task<ReviewDto> EditGet(int id)
        {
            return _mapper.Map<ReviewDto>(await _reviewRepository.GetById(id));
        }

        public async Task EditPost(ReviewDto review)
        {
            await _reviewRepository.Update(_mapper.Map<Review>(review));
        }

        public async Task<ReviewDto> GetById(int id)
        {
            return _mapper.Map<ReviewDto>(await _reviewRepository.GetById(id));
        }

        public async Task<bool> Remove(int id)
        {
            await _reviewRepository.Remove(await _reviewRepository.GetById(id));
            return true;
        }
    }
}
