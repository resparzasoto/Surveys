using Surveys.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Surveys.Web.DAL.SqlServer
{
    public class SurveysProvider : SqlServerProvider
    {
        public override string ConnectionString { get; set; } =
            System.Configuration.ConfigurationManager.ConnectionStrings["Surveys"].ConnectionString;

        public async Task<IEnumerable<Survey>> GetAllSurveysAsync()
        {
            var result = new List<Survey>();
            var query = @"SELECT Id, Name, Birthdate, TeamId, Lat, Lon
                          FROM Surveys.dbo.Surveys (NOLOCK)";

            using (var reader = await ExecuteReaderAsync(query))
            {
                while (reader.Read())
                {
                    result.Add(GetSurveyFromReader(reader));
                }
            }

            return result;
        }

        public async Task<int> InsertSurveyAsync(Survey survey)
        {
            if (survey == null)
            {
                return 0;
            }

            var query = @"INSERT INTO Surveys (Id, Name, BirthDate, TeamId, Lat, Lon)
                                       VALUES (@Id, @Name, @BirthDate, @TeamId, @Lat, @Lon)";

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@Id", survey.Id),
                new SqlParameter("@Name", survey.Name),
                new SqlParameter("@BirthDate", survey.Birthdate),
                new SqlParameter("@TeamId", survey.TeamId),
                new SqlParameter("@Lat", survey.Lat),
                new SqlParameter("@Lon", survey.Lon)
            };

            var result = await ExecuteNonQueryAsync(query, parameters.ToArray());

            return result;
        }

        private Survey GetSurveyFromReader(SqlDataReader reader)
        {
            return new Survey()
            {
                Id = reader[nameof(Survey.Id)].ToString(),
                Name = reader[nameof(Survey.Name)].ToString(),
                Birthdate = (DateTime)reader[nameof(Survey.Birthdate)],
                TeamId = (int)reader[nameof(Survey.TeamId)],
                Lon = (double)reader[nameof(Survey.Lon)],
                Lat = (double)reader[nameof(Survey.Lat)]
            };
        }
    }
}
