public class Stock
{
    public int StockId { get; set; }

    public string StockName { get; set; } = null!;

    public string Links { get; set; } = null!;


    public virtual ICollection<Shift> Shifts { get; } = new List<Shift>();
    
    public virtual ICollection<ShiftHistory> ShiftHistories { get; } = new List<ShiftHistory>();

    public virtual ICollection<Mark> Marks { get; } = new List<Mark>();
}