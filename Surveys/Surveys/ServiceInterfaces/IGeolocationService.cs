using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Surveys.ServiceInterfaces
{
    public interface IGeolocationService
    {
        Task<Tuple<double, double>> GetCurrentLocationAsync();
    }
}
