using Surveys.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.ServiceInterfaces
{
    public interface IWebApiService
    {
        Task<IEnumerable<Team>> GetTeamsAsync();

        Task<bool> SaveSurveysAsync(IEnumerable<Survey> surveys);

        Task<bool> LoginAsync(string username, string password);
    }
}
