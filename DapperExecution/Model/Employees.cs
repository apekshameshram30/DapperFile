using System.ComponentModel.DataAnnotations;

namespace DapperExecution.Model
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int Age { get; set; }

        public string? Position { get; set; }

      // public Companies? Companies { get; set; }
        public int CompanyId { get; set; }
    }
}
