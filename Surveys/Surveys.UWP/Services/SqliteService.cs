using SQLite;
using Surveys.ServiceInterfaces;
using Surveys.UWP.Services;

[assembly:Xamarin.Forms.Dependency(typeof(SqliteService))]
namespace Surveys.UWP.Services
{
    public class SqliteService : ISqliteService
    {
        public SQLiteConnection GetConnection()
        {
            var localDbFile = System.IO.Path.Combine(Windows.Storage.ApplicationData.Current.LocalFolder.Path, "surveys.db");

            return new SQLiteConnection(localDbFile);
        }
    }
}
