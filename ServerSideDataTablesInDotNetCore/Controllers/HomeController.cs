using Microsoft.AspNetCore.Mvc;
using ServerSideDataTablesInDotNetCore.Models;
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace ServerSideDataTablesInDotNetCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly empDbContext _context;




        private readonly ILogger<HomeController> _logger;




        public HomeController(ILogger<HomeController> logger, empDbContext context)
        {
            _logger = logger;



            this._context = context;    
        }






        public IActionResult Index()
        {
            return View();
        }







        [HttpPost]
        public JsonResult GetEmployeeList()
        {
            int totalRecord = 0;
            int filterRecord = 0;

            var draw = Request.Form["draw"].FirstOrDefault();


            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();


            var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();


            var searchValue = Request.Form["search[value]"].FirstOrDefault();


            int pageSize = Convert.ToInt32(Request.Form["length"].FirstOrDefault() ?? "0");


            int skip = Convert.ToInt32(Request.Form["start"].FirstOrDefault() ?? "0");



            var data = _context.Set<Employees>().AsQueryable();

            //get total count of data in table
            totalRecord = data.Count();



            // search data 
            if (!string.IsNullOrEmpty(searchValue))
            {
                data = data.Where(x =>
                  x.EmployeeName.ToLower().Contains(searchValue.ToLower())
                  || x.EmployeeTotalTimeWorked.ToLower().Contains(searchValue.ToLower())
                );
            }


            // get total count of records after search 
            filterRecord = data.Count();



            //sort data
            if (!string.IsNullOrEmpty(sortColumn) && !string.IsNullOrEmpty(sortColumnDirection))
                data = data.OrderBy(sortColumn + " " + sortColumnDirection);




            //pagination
            var empList = data.Skip(skip).Take(pageSize).ToList();

            var returnObj = new { draw = draw, recordsTotal = totalRecord, recordsFiltered = filterRecord, data = empList };
            return Json(returnObj);
        }

   








        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}