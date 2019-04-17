using SQLite;

namespace Surveys.ServiceInterfaces
{
    public interface ISqliteService
    {
        SQLiteConnection GetConnection();
    }
}
