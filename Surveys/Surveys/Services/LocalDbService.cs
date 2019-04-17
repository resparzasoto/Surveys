using SQLite;
using Surveys.Models;
using Surveys.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Surveys.Services
{
    public class LocalDbService : ILocalDbService
    {
        private readonly SQLiteConnection connection = null;

        public LocalDbService()
        {
            connection = Xamarin.Forms.DependencyService.Get<ISqliteService>().GetConnection();

            CreateDatabase();
        }

        private void CreateDatabase()
        {
            connection.CreateTable<Survey>();
        }

        public Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            return Task.Run(() => (IEnumerable<Survey>)connection.Table<Survey>().ToArray());
        }

        public Task InsertSurveyAsync(Survey survey)
        {
            return Task.Run(() =>
            {
                connection.Insert(survey);
            });
        }

        public Task DeleteSurveyAsync(Survey survey)
        {
            return Task.Run(() =>
            {
                var query = $"DELETE FROM Survey WHERE Id = '{survey.Id}'";

                var command = connection.CreateCommand(query);

                var result = command.ExecuteNonQuery();

                return result > 0;
            });
        }
    }
}
