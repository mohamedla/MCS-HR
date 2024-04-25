using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MCSHR.Models
{
    public class EmployeeDTO : ResponseModel
    {
        public Employee employee { get; set; }
    }
}
