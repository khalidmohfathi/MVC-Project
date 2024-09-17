using AutoMapper;
using MVC.DAL.Models;
using MVC.PL.ViewModels;

namespace MVC.PL.Helpers
{
	public class MappingProfiles : Profile
	{
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
