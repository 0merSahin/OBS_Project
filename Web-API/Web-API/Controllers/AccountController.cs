﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Web_API.Concrete;
using Web_API.Entities;
using Web_API.Enums;

namespace Web_API.Controllers
{
    [ApiController]
    public class AccountController : Controller
	{
        protected readonly SqlDbContext _context;

        public AccountController(SqlDbContext context)
        {
            _context = context;
        }


        [HttpGet("api/Account/Get/CheckNumber/{Number}")]
        public ActionResult<IEnumerable<AccountTypeEnum>> CheckNumber(string Number)
        {    
            Employee? employee = _context.Employees.FirstOrDefault(x => x.UserNumber == Number);
            if (employee != null)
            {
                return Ok(AccountTypeEnum.Employee);
            }

            Student? student = _context.Students.FirstOrDefault(x => x.Number == Number);
            if (student != null)
            {
                return Ok(AccountTypeEnum.Student);
            }
            else
                return Ok(AccountTypeEnum.Null);
        }



        [HttpPost("api/Account/LoginEmployee")]
        public ActionResult<IEnumerable<LoginReturnEnum>> EmployeeLogin(Employee employee)
        {
            Employee? employeeData = _context.Employees.FirstOrDefault(x => x.UserNumber == employee.UserNumber);
            if (employeeData == null)
            {
                return Ok(LoginReturnEnum.WrongNumber);
            }
            else
            {
                employeeData = null;
                employeeData = _context.Employees.FirstOrDefault(x => x.UserNumber == employee.UserNumber && x.Password == employee.Password);
                if (employeeData != null)
                {
                    return Ok(LoginReturnEnum.Accept);
                }
                else
                {
                    return Ok(LoginReturnEnum.WrongPassword);
                }
            }
        }



        [HttpPost("api/Account/LoginStudent")]
        public ActionResult<IEnumerable<LoginReturnEnum>> StudentLogin(Student student)
        {
            Student? studentData = _context.Students.FirstOrDefault(x => x.Number == student.Number);
            if (studentData == null)
            {
                return Ok(LoginReturnEnum.WrongNumber);
            }
            else
            {
                studentData = null;
                studentData = _context.Students.FirstOrDefault(x => x.Number == student.Number && x.Password == student.Password);
                if (studentData != null)
                {
                    return Ok(LoginReturnEnum.Accept);
                }
                else
                {
                    return Ok(LoginReturnEnum.WrongPassword);
                }
            }
        }


        
    }
}

