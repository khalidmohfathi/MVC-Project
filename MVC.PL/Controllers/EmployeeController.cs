using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MVC.BLL.Interfaces;
using MVC.DAL.Models;
using MVC.PL.Helpers;
using MVC.PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

namespace MVC.PL.Controllers
{
	public class EmployeeController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _env;
		private readonly IMapper _mapper;

		public EmployeeController(IUnitOfWork unitOfWork, IWebHostEnvironment env, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_env = env;
			_mapper = mapper;
		}
		public IActionResult Index(string SearchInput)
		{
			ViewBag.Message = "All Employees";
			if (string.IsNullOrWhiteSpace(SearchInput))
			{
				var employees = _unitOfWork.EmployeeRepository.GetAll();
				var mappedEmp = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
				return View(mappedEmp);
			}
			else
			{
				var employees = _unitOfWork.EmployeeRepository.GetEmployeesByName(SearchInput);
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
				emp.ImageName = DocumentSettings.UploadFile(emp.Image, "Images");
				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);
				_unitOfWork.EmployeeRepository.Add(mappedEmp);
				var count = _unitOfWork.Complete();
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
			var emp = _unitOfWork.EmployeeRepository.GetById(id.Value);
			var mappedEmp = _mapper.Map<Employee, EmployeeViewModel>(emp);
			if (emp == null)
			{
				return NotFound();
			}
			return View(ViewName, mappedEmp);
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
				emp.ImageName = DocumentSettings.UploadFile(emp.Image, "Images");
				var mappedEmp = _mapper.Map<EmployeeViewModel, Employee>(emp);
				_unitOfWork.EmployeeRepository.Update(mappedEmp);
				_unitOfWork.Complete();
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
				_unitOfWork.EmployeeRepository.Delete(mappedEmp);
				_unitOfWork.Complete();
				DocumentSettings.DeleteFile(emp.ImageName, "Images");
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
