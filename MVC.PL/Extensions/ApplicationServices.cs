using Microsoft.Extensions.DependencyInjection;
using MVC.BLL.Interfaces;
using MVC.BLL.Repositories;

namespace MVC.PL.Extensions
{
	public static class ApplicationServices
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IDepartmentRepository, DepartmentRepository>();
			services.AddScoped<IEmployeeRepository, EmployeeRepository>();
			return services;
		}
	}
}
