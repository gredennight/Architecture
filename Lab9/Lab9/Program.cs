var list1 = new List<Employee>
        {
            new Employee("Manager", 8000),
            new Employee("Developer", 6000),
            new Employee("Designer", 5000)
        };
var list2 = new List<Employee>
        {
            new Employee("Manager", 8500),
            new Employee("Developer", 6200),
            new Employee("Analyst", 5500)
        };
var itDept = new Department("IT Department", list1);
var businessDept = new Department("Business Department", list2);
var company = new Company(new List<Department> { itDept, businessDept });
var visitor = new SalaryReportVisitor();
Console.WriteLine("\n\nSalary Report for Company:-----------------------------------------");
company.Accept(visitor);
visitor.PrintCompanyReport();
Console.WriteLine("\n\nSalary Report for Department:--------------------------------------");
itDept.Accept(visitor);
visitor.PrintDepartmentReport();
public interface IVisitor
{
    void VisitCompany(Company company);
    void VisitDepartment(Department department);
    void VisitEmployee(Employee employee);
}
public interface IElement { void Accept(IVisitor visitor); }
public class Company : IElement
{
    public List<Department> Departments { get; }
    public Company(List<Department> departments) { Departments = departments; }
    public void Accept(IVisitor visitor) { visitor.VisitCompany(this); }
}
public class Department : IElement
{
    public string Name { get; }
    public List<Employee> Employees { get; }
    public Department(string name, List<Employee> employees)
    {
        Name = name;
        Employees = employees;
    }
    public void Accept(IVisitor visitor) { visitor.VisitDepartment(this); }
}
public class Employee : IElement
{
    public string Position { get; }
    public decimal Salary { get; }
    public Employee(string position, decimal salary)
    {
        Position = position;
        Salary = salary;
    }
    public void Accept(IVisitor visitor) { visitor.VisitEmployee(this); }
}
public class SalaryReportVisitor : IVisitor
{
    private decimal totalCompanySalary = 0;
    private decimal totalDepartmentSalary = 0;
    private string currentDepartment = "";
    public void VisitCompany(Company company)
    {
        totalCompanySalary = 0;
        foreach (var department in company.Departments) { department.Accept(this); }
    }
    public void VisitDepartment(Department department)
    {
        currentDepartment = department.Name;
        totalDepartmentSalary = 0;
        Console.WriteLine($"\nDepartment: {currentDepartment}");
        foreach (var employee in department.Employees) { employee.Accept(this); }
    }
    public void VisitEmployee(Employee employee)
    {
        Console.WriteLine($"- {employee.Position}: ${employee.Salary}");
        totalCompanySalary += employee.Salary;
        totalDepartmentSalary += employee.Salary;
    }
    public void PrintCompanyReport() { Console.WriteLine($"\nTotal Company Salary: ${totalCompanySalary}"); }
    public void PrintDepartmentReport() { Console.WriteLine($"Total Department Salary ({currentDepartment}): ${totalDepartmentSalary}"); }
}
