using MVC.DAL.Models;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System;

namespace MVC.PL.ViewModels
{
	public enum Gender
	{
		[EnumMember(Value = "Male")]
		Male = 1,
		[EnumMember(Value = "Female")]
		Female = 2
	}
	public enum EmployeeType
	{
		FullTime = 1,
		PartTime = 2
	}
	public class EmployeeViewModel
	{
		public int Id { get; set; }

		[Required(ErrorMessage = "Name is required!")]
		[MaxLength(50, ErrorMessage = "Max Length for name is 50")]
		[MinLength(4, ErrorMessage = "Min Length For name is 4")]
		public string Name { get; set; }

		[Range(21, 60)]
		public int? Age { get; set; }

		[RegularExpression(@"^[0-9]{1,3}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}-[a-zA-Z]{1,10}$", ErrorMessage = "Address Must be like 123-street-city-country")]
		public string Address { get; set; }

		[DataType(DataType.Currency)]
		public decimal Salary { get; set; }
		public bool IsActive { get; set; }

		[EmailAddress]
		public string Email { get; set; }

		[Phone]
		[Display(Name = "Phone Number")]
		public string PhoneNumber { get; set; }

		[Display(Name = "Hire Date")]
		public DateTime HireDate { get; set; }

		public bool IsDeleted { get; set; }

		public Gender Gender { get; set; }
		public Department Department { get; set; }
		public int? DepartmentId { get; set; }
	}
}