using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Restaurant_5._0.Authorization
{
    public class MinimumAgeRequirementHandler : AuthorizationHandler<MinimumAgeRequirement>
    {
        private readonly ILogger<MinimumAgeRequirementHandler> logger;

        public MinimumAgeRequirementHandler(ILogger<MinimumAgeRequirementHandler> logger)
        {
            this.logger = logger;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumAgeRequirement requirement)
        {
            var date = DateTime.Parse(context.User.FindFirst(x => x.Type == "BirthDate").Value);
            var userEmail = context.User.FindFirst(x => x.Type == ClaimTypes.Name).Value;

            logger.LogInformation($"User: {userEmail} with date of birth: {date} ");

            if (date.AddYears(requirement.MinmumAge) <= DateTime.Today)
            {
                logger.LogInformation("login succeded");
                context.Succeed(requirement);
            }
            else
            {

                logger.LogInformation("login failed");
            }
            return Task.CompletedTask;
        }
    }
}
