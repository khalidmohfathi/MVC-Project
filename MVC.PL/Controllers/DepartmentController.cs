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
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _env;

		public DepartmentController(IUnitOfWork unitOfWork, IWebHostEnvironment env)
		{
			_unitOfWork = unitOfWork;
			_env = env;
		}
		public IActionResult Index()
		{
			var departments = _unitOfWork.DepartmentRepository.GetAll();
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
				_unitOfWork.DepartmentRepository.Add(dept);
				var count = _unitOfWork.Complete();
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
			var dept = _unitOfWork.DepartmentRepository.GetById(id.Value);
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
				_unitOfWork.DepartmentRepository.Update(dept);
				var count = _unitOfWork.Complete();
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
				_unitOfWork.DepartmentRepository.Delete(dept);
				var count = _unitOfWork.Complete();
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
