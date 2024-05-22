namespace DapperExecution.Model
{
    public class Companies
    {
        public int CompanyId { get; set; }
        public string Name { get; set; } = null!;

        public string Address {  get; set; } = null!;

        public string Country {  get; set; } = null!;

        public List<Employees> Employee { get; set; }= new List<Employees>();
    }
}
