namespace EFDemo.Models.Domain
{
    public class Employee
    {
        public int    Id     { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Age { get; set; }
        public string Number { get; set; } = null!;
    }
}

       