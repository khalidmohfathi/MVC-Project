using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MVC.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC.DAL.Data.Configurations
{
	internal class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
	{
		public void Configure(EntityTypeBuilder<Employee> builder)
		{
			builder.Property(E => E.Salary)
				   .HasColumnType("decimal(18,2)");
			builder.Property(E => E.Gender)
				   .HasConversion(
					(Gender) => Gender.ToString(),
					(GenderAsString) => (Gender)Enum.Parse(typeof(Gender), GenderAsString, true)
					);
		}
	}
}
