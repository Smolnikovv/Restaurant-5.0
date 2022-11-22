using Microsoft.AspNetCore.Authorization;

namespace Restaurant_5._0.Authorization
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinmumAge { get; }
        public MinimumAgeRequirement(int minmumAge)
        {
            MinmumAge = minmumAge;
        }
    }
}
