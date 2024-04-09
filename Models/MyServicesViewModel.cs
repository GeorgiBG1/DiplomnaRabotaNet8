using Data.Models;
using DTOs.OUTPUT;

namespace Models
{
    public class MyServicesViewModel
    {
        public ICollection<ServiceStatus> StatusesList { get; set; }
        public IEnumerable<ServiceMiniDTO> Services { get; set; }
    }
}
