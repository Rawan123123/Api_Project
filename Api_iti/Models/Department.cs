using System.Text.Json.Serialization;

namespace Api_iti.Models
{
    public class Department
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String? ManagerName { get; set; }

        [JsonIgnore]
        public List<Employee> Emps { get; set; }
    }
}
