using System;
using System.Threading.Tasks;

namespace Surveys.ServiceInterfaces
{
    public interface IGeolocationService
    {
        Task<Tuple<double, double>> GetCurrentLocationAsync();
    }
}
