public class Mark
{
    public Guid MarkId { get; set; }

    public int StockId { get; set; }

    public Guid EmployeeId { get; set; }

    public DateTime MarkDate { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Stock? Stock { get; set; }
}