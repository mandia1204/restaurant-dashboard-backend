using Models;
using Models.Dto;

namespace Builders
{
    public interface IDashboardBuilder
    {
         DashboardDto Build(Dashboard model, DashboardParameters pars);
    }
}