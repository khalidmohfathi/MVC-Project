using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.BLL.Interfaces;
using MVC.DAL.Models;
using System;

namespace MVC.PL.Controllers
{
    public class EmployeeController : Controller
	{
		private readonly IEmployeeRepository _repository;
		private readonly IWebHostEnvironment _env;

		public EmployeeController(IEmployeeRepository repository, IWebHostEnvironment env)
		{
			_repository = repository;
			_env = env;
		}
		public IActionResult Index()
		{
			ViewBag.Message = "All Employees";
			var employees = _repository.GetAll();
			return View(employees);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Employee dept)
		{
			if (ModelState.IsValid)
			{
				var count = _repository.Add(dept);
				if (count > 0)
				{
					TempData["Success"] = "Employee Created Successfully";
					return RedirectToAction(nameof(Index));
				}
			}
			return View(dept);
		}

		public IActionResult Details(int? id, string ViewName = "Details")
		{
			if (!id.HasValue)
			{
				return BadRequest();
			}
			var dept = _repository.GetById(id.Value);
			if (dept == null)
			{
				return NotFound();
			}
			return View(ViewName, dept);
		}

		[HttpGet]
		public IActionResult Update(int? id)
		{
			return Details(id, "Update");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update([FromRoute] int id, Employee emp)
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
				var count = _repository.Update(emp);
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
		public IActionResult Delete(Employee emp)
		{
			try
			{
				_repository.Delete(emp);
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
