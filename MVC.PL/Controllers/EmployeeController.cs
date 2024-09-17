using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.BLL.Interfaces;
using MVC.DAL.Models;
using MVC.PL.ViewModels;
using System;
using System.Collections.Generic;

namespace MVC.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IEmployeeRepository _repository;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env, IMapper mapper)
		{
			_repository = repository;
			_env = env;
			_mapper = mapper;
		}
		public IActionResult Index(string SearchInput)
		{
			ViewBag.Message = "All Employees";
			if (string.IsNullOrWhiteSpace(SearchInput))
			{
				var employees = _repository.GetAll();
				var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
				return View(mappedEmp);
			}
			else
			{
				var employees = _repository.GetEmployeesByName(SearchInput);
				var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
				return View(mappedEmp);
			}
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(EmployeeViewModel emp)
		{
			if (ModelState.IsValid)
			{
				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);
				var count = _repository.Add(mappedEmp);
				if (count > 0)
				{
					TempData["Success"] = "Employee Created Successfully";
					return RedirectToAction(nameof(Index));
				}
			}
			return View(emp);
		}

		public IActionResult Details(int? id, string ViewName = "Details")
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}
			var emp = _repository.GetById(id.Value);
			if (emp == null)
			{
				return NotFound();
			}
			return View(ViewName, emp);
		}

		[HttpGet]
		public IActionResult Update(int? id)
		{
			return Details(id, "Update");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update([FromRoute] int id, EmployeeViewModel emp)
		{
			if (id != emp.Id)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View(emp);
			}

			try
			{
				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);
				var count = _repository.Update(mappedEmp);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "An Error has Occured");
				}
				return View(emp);
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(EmployeeViewModel emp)
		{
			try
			{
				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);
				_repository.Delete(mappedEmp);
				return RedirectToAction(nameof(Index));
			}
			catch (Exception ex)
			{
				if (_env.IsDevelopment())
				{
					ModelState.AddModelError(string.Empty, ex.Message);
				}
				else
				{
					ModelState.AddModelError(string.Empty, "An Error has Occured");
				}
				return View(emp);
			}
		}
	}
}
