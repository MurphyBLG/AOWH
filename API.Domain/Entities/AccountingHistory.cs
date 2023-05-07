using Newtonsoft.Json;

public class AccountingHistory
{
    public Guid AccountingHistoryId { get; set; }
    public Guid EmployeeId { get; set; }
    public int Year { get; set; }
    public int Month { get; set; }
    public decimal Mentoring { get; set; }
    public decimal Teaching { get; set; }
    public decimal Bonus { get; set; }
    public decimal Vacation { get; set; }
    public decimal Advance { get; set; }
    public decimal MentoringPrev { get; set; }
    public decimal TeachingPrev { get; set; }
    public decimal BonusPrev { get; set; }
    public decimal VacationPrev { get; set; }
    public decimal AdvancePrev { get; set; }
    public DateTime UpdateDate { get; set; }
    [JsonIgnore]
    public virtual Employee Employee { get; set; } = null!;

    public AccountingHistory() { }

    public AccountingHistory(Guid employeeId,
                             int year, 
                             int month, 
                             decimal mentoring, 
                             decimal teaching, 
                             decimal bonus, 
                             decimal vacation, 
                             decimal advance, 
                             decimal mentoringPrev, 
                             decimal teachingPrev, 
                             decimal bonusPrev, 
                             decimal vacationPrev, 
                             decimal advancePrev)
    {
        AccountingHistoryId = Guid.NewGuid();
        EmployeeId = employeeId;
        Year = year;
        Month = month;
        Mentoring = mentoring;
        Teaching = teaching;
        Bonus = bonus;
        Vacation = vacation;
        Advance = advance;
        MentoringPrev = mentoringPrev;
        TeachingPrev = teachingPrev;
        BonusPrev = bonusPrev;
        VacationPrev = vacationPrev;
        AdvancePrev = advancePrev;
        UpdateDate = DateTime.UtcNow; 
    }
}