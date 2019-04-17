using Surveys.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.ServiceInterfaces
{
    public interface ILocalDbService
    {
        Task<IEnumerable<Survey>> GetAllSurveysAsync();

        Task InsertSurveyAsync(Survey survey);

        Task DeleteSurveyAsync(Survey survey);
    }
}
