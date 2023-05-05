using System.ComponentModel.DataAnnotations;

namespace ServerSideDataTablesInDotNetCore.Models
{
    public class Employees
    {

        [Key]
        public int EmployeeId { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeTotalTimeWorked { get; set; }

    }
}
