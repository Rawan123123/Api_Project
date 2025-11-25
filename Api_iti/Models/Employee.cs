using System.ComponentModel.DataAnnotations.Schema;

namespace Api_iti.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Address { get; set; }

        [ForeignKey("Dept")]
        public int DepartmentId { get; set; }
        public Department Dept { get; set; }

    }
}
