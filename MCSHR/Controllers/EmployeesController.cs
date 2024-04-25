using MCSHR.BussinessLayer;
using MCSHR.Context;
using MCSHR.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Data;
using System.Globalization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MCSHR.Controllers
{
    
    public class EmployeesController : Controller
    {
        private readonly RepositoryContext _repository;

        public EmployeesController(RepositoryContext repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult UploadFile()
        {
            UploadFileModal modal = new UploadFileModal();
            return View("Index",modal);
        }

        // Upload Employee Tab Delimited File
        [HttpPost]
        public IActionResult UploadEmployeesFile(UploadFileModal uploadedFileModal)
        {

            uploadedFileModal.IsResponse = true;
            try
            {
                if (ModelState.IsValid)
                {
                    FilesHandler.UploadFile(ref uploadedFileModal);
                    FilesHandler.UploadEmployeeFromFile(uploadedFileModal.File.FileName, false, _repository);
                }
                else
                {
                    uploadedFileModal.IsSuccess = false;
                    uploadedFileModal.Message = "Data Is Not In The Right Format";
                }
            }
            catch (Exception)
            {
                uploadedFileModal.IsSuccess = false;
                uploadedFileModal.Message = "Some thing went wrong";
            }
            return View("Index", uploadedFileModal);
        }

        [HttpGet()]
        public IActionResult EmployeeList()
        {
            IEnumerable<Employee> Employees;
            try
            {
                Employees = _repository.employees.ToList(); // Get Employees From Database
            }
            catch (Exception)
            {
                Employees = new List<Employee>();
            }

            return View(Employees);
        }

        [HttpGet()]
        public IActionResult AddNew()
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();
            return View(employeeDTO);
        }

        [HttpPost]
        public IActionResult AddNew(EmployeeDTO employeeDTO)
        {
            employeeDTO.IsResponse = true;
            try
            {
                if (ModelState.IsValid) // Validate Employee
                {
                    if (employeeDTO.employee.Emp_Type == EmployeeTypes.FreeLancer && employeeDTO.employee.Assurance)
                    {
                        employeeDTO.IsSuccess = false;
                        employeeDTO.Message = "Employee Can't Have Assurance While The Employment Type Is FreeLancer";
                    }
                    else
                    {
                        _repository.employees.Add(employeeDTO.employee); // Add Employee To DB
                        _repository.SaveChanges();
                        employeeDTO.IsSuccess = true;
                        employeeDTO.Message = "Employee Added Successfully";
                        employeeDTO.employee = new Employee();
                    }
                }
                else
                {
                    employeeDTO.IsSuccess = false;
                    employeeDTO.Message = "Data Is Not In The Right Format";
                }
            }
            catch (Exception)
            {

                employeeDTO.IsSuccess = false;
                employeeDTO.Message = "Data Is Not In The Right Format";
            }
            return View("AddNew", employeeDTO);
        }

        [HttpGet ]
        public IActionResult Edit(int id)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();
            try
            {
                employeeDTO.employee = _repository.employees.FirstOrDefault(emp => emp.ID == id); // get employee from DB using ID
                if (employeeDTO.employee == null) // Check if the employ exists
                {
                    employeeDTO.IsResponse = true;
                    employeeDTO.IsSuccess = false;
                    employeeDTO.Message = "No Employee With Match Your Data";
                }
            }
            catch (Exception)
            {
                employeeDTO.IsResponse = true;
                employeeDTO.IsSuccess = false;
                employeeDTO.Message = "Some Thing Went Wrong";
            }
            return View(employeeDTO);
        }

        [HttpPost]
        public IActionResult Edit(EmployeeDTO employeeDTO)
        {
            employeeDTO.IsResponse = true;
            try
            {
                if (ModelState.IsValid) // Validate Employee Date
                {
                    if (employeeDTO.employee.Emp_Type == EmployeeTypes.FreeLancer && employeeDTO.employee.Assurance)
                    {
                        employeeDTO.IsSuccess = false;
                        employeeDTO.Message = "Employee Can't Have Assurance While The Employment Type Is FreeLacer";
                    }
                    else
                    {
                        if (employeeDTO.employee != null)
                        {
                            _repository.Entry(employeeDTO.employee).State = EntityState.Modified; // Update Employee In DB
                            _repository.SaveChanges();
                            employeeDTO.IsSuccess = true;
                            employeeDTO.Message = "Employee Updated Successfully";
                        }
                        else
                        {
                            employeeDTO.IsSuccess = false;
                            employeeDTO.Message = "No Employee With Match Your Data";
                        }
                    }
                }
                else
                {
                    employeeDTO.IsSuccess = false;
                    employeeDTO.Message = "Data Is Not In The Right Format";
                }
            }
            catch (Exception)
            {
                employeeDTO.IsSuccess = false;
                employeeDTO.Message = "Some Thing Went Wrong";
            }
            return View(employeeDTO);
        }

        [HttpGet()]
        public IActionResult Delete(int id)
        {
            EmployeeDTO employeeDTO = new EmployeeDTO();

            try
            {
                employeeDTO.employee = _repository.employees.FirstOrDefault(emp => emp.ID == id);
                if (employeeDTO.employee == null)
                {
                    employeeDTO.IsResponse = true;
                    employeeDTO.IsSuccess = false;
                    employeeDTO.Message = "No Employee With Match Your Data";
                }
            }
            catch (Exception)
            {
                employeeDTO.IsResponse = true;
                employeeDTO.IsSuccess = false;
                employeeDTO.Message = "Some Thing Went Wrong";
            }
            return View(employeeDTO);
        }

        [HttpPost()]
        public IActionResult Delete(EmployeeDTO employeeDTO)
        {
            try
            {
                if (employeeDTO.employee == null)
                {
                    employeeDTO.IsResponse = true;
                    employeeDTO.IsSuccess = false;
                    employeeDTO.Message = "No Employee With Match Your Data";
                    return View("Delete", employeeDTO);
                }

                _repository.Entry(employeeDTO.employee).State = EntityState.Deleted;
                _repository.SaveChanges();
            }
            catch (Exception)
            {
                employeeDTO.IsResponse = true;
                employeeDTO.IsSuccess = false;
                employeeDTO.Message = "Some Thing Went Wrong";
            }
            return RedirectToAction("EmployeeList");
        }
    }
}
