using Application_ECommerce.App.Dashboard.Dtos;
using System.Threading.Tasks;

namespace Application_ECommerce.App.Dashboard.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardStatsDto> GetDashboardStatsAsync();
    }
}
