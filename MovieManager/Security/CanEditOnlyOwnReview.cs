using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MovieManager.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManager.Security
{
    public class CanEditOnlyOwnReview : AuthorizationHandler<ManageReviewAuthorNameRequirement>
    {
        private readonly IReviewService _reviewService;
        private readonly IHttpContextAccessor _contextAccessor;

        public CanEditOnlyOwnReview(IReviewService reviewService, IHttpContextAccessor contextAccessor)
        {
            _reviewService = reviewService;
            _contextAccessor = contextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ManageReviewAuthorNameRequirement requirement)
        {
            var authFilterContext = context.Resource as Endpoint;

            string loggedInUserName = context.User.Identity.Name;

            string reviewIdBeingEdited = _contextAccessor.HttpContext.Request.Path;

            var review = _reviewService.GetById(Int32.Parse(reviewIdBeingEdited.Split('/').Last()));

            if ((loggedInUserName == review.Result.Author) || (context.User.IsInRole("Admin")))
            {
                context.Succeed(requirement);
            }
            return Task.CompletedTask;
        }
    }
}
