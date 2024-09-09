using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.BLL.Interfaces;
using MVC.DAL.Models;
using System;

namespace MVC.PL.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IDepartmentRepository _repository;
		private readonly IWebHostEnvironment _env;

		public DepartmentController(IDepartmentRepository repository, IWebHostEnvironment env)
		{
			_repository = repository;
			_env = env;
		}
		public IActionResult Index()
		{
			var departments = _repository.GetAll();
			return View(departments);
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(Department dept)
		{
			if (ModelState.IsValid)
			{
				var count = _repository.Add(dept);
				if (count > 0)
				{
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
			//if (!id.HasValue)
			//{
			//	return BadRequest();
			//}
			//var dept = _repository.GetById(id.Value);
			//if (dept == null)
			//{
			//	return NotFound();
			//}
			//return View(dept);
			return Details(id, "Update");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Update([FromRoute] int id, Department dept)
		{
			if (id != dept.Id)
			{
				return BadRequest();
			}

			if (!ModelState.IsValid)
			{
				return View(dept);
			}

			try
			{
				var count = _repository.Update(dept);
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
				return View(dept);
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			return Details(id, "Delete");
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Department dept)
		{
			try
			{
				_repository.Delete(dept);
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
				return View(dept);
			}
		}

	}
}
