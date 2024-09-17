using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Models
{
	public class Department : ModelBase
	{
		[Required(ErrorMessage = "Name is Required!")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Code is Required!")]
		public string Code { get; set; }

		[Display(Name = "Date of Creation")]
		public DateTime DateOfCreation { get; set; }
		public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();
	}
}
