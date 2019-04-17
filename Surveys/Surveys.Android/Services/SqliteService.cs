using SQLite;
using Surveys.Droid.Services;
using Surveys.ServiceInterfaces;

[assembly:Xamarin.Forms.Dependency(typeof(SqliteService))]
namespace Surveys.Droid.Services
{
    public class SqliteService : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDbFile = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "surveys.db");

            return new SQLiteConnection(localDbFile);
        }
    }
}