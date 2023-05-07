using Newtonsoft.Json;


public partial class Position
{
    public Guid? PositionId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Salary { get; set; }

    public decimal QuarterlyBonus { get; set; }

    public string InterfaceAccesses { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<EmployeeHistory> EmployeeHistories { get; } = new List<EmployeeHistory>();

    [JsonIgnore]
    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();

    public Position() { }

    public Position(Guid? positionId,
                    string name,
                    decimal salary,
                    decimal quarterlyBonus,
                    InterfaceAccesses interfaceAccesses)
    {
        PositionId = positionId;
        Name = name;
        Salary = salary;
        QuarterlyBonus = quarterlyBonus;
        InterfaceAccesses = System.Text.Json.JsonSerializer.Serialize(interfaceAccesses);
    }

    public void Update(string name,
                       decimal salary,
                       decimal quarterlyBonus,
                       string interfaceAccesses)
    {
        this.Name = name;
        this.Salary = salary;
        this.QuarterlyBonus = quarterlyBonus;
        this.InterfaceAccesses = interfaceAccesses;
    }
}
