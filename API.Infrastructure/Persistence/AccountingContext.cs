using Microsoft.EntityFrameworkCore;

public partial class AccountingContext : DbContext
{
    public AccountingContext()
    {
    }

    public AccountingContext(DbContextOptions<AccountingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; } = null!;

    public virtual DbSet<EmployeeHistory> EmployeeHistories { get; set; } = null!;

    public virtual DbSet<Position> Positions { get; set; } = null!;

    public virtual DbSet<Stock> Stocks { get; set; } = null!;

    public virtual DbSet<Shift> Shifts { get; set; } = null!;

    public virtual DbSet<ShiftInfo> ShiftInfos { get; set; } = null!;

    public virtual DbSet<ShiftHistory> ShiftHistories { get; set; } = null!;

    public virtual DbSet<Mark> Marks { get; set; } = null!;

    public virtual DbSet<WorkPlan> WorkPlans { get; set; } = null!;

    public virtual DbSet<Accounting> Accountings { get; set; } = null!;

    public virtual DbSet<AccountingHistory> AccountingHistories { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("employee_pkey");

            entity.ToTable("employee");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Birthday).HasColumnName("birthday");
            entity.Property(e => e.DateOfTermination).HasColumnName("date_of_termination");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Surname)
                .HasMaxLength(255)
                .HasColumnName("surname");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(255)
                .HasColumnName("patronymic");
            entity.Property(e => e.PassportIssueDate).HasColumnName("passport_issue_date");
            entity.Property(e => e.PassportIssuer)
                .HasMaxLength(255)
                .HasColumnName("passport_issuer");
            entity.Property(e => e.PassportNumber)
                .HasMaxLength(255)
                .HasColumnName("passport_number");

            entity.HasIndex(e => e.PassportNumber, "employee_passport_number_key").IsUnique();

            entity.Property(e => e.StartOfLuchSeniority).HasColumnName("start_of_luch_seniority");
            entity.Property(e => e.StartOfTotalSeniority).HasColumnName("start_of_total_seniority");

            entity.Property(e => e.DateOfStartInTheCurrentPosition).HasColumnName("date_of_start_in_the_current_position");
            entity.Property(e => e.DateOfStartInTheCurrentStock).HasColumnName("date_of_start_in_the_current_stock");
            entity.Property(e => e.ForkliftControl).HasColumnName("forklift_control");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.PercentageOfSalaryInAdvance).HasColumnName("percentage_of_salary_in_advance");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.QuarterlyBonus)
                .HasPrecision(10, 2)
                .HasColumnName("quarterly_bonus");
            entity.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasColumnName("salary");
            entity.Property(e => e.RolleyesControl).HasColumnName("rolleyes_control");
            entity.Property(e => e.Stocks)
                .HasColumnType("jsonb")
                .HasColumnName("stocks");

            entity.Property(e => e.RefreshToken)
                .HasColumnName("refresh_token");

            entity.Property(e => e.RefreshTokenExpires)
                .HasColumnName("refresh_token_expires");

            entity.HasOne(d => d.Position).WithMany(p => p.Employees)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("employee_position_id_fkey");
        });

        modelBuilder.Entity<EmployeeHistory>(entity =>
        {
            entity.HasKey(e => e.EmployeeHistoryId).HasName("employee_history_pkey");

            entity.ToTable("employee_history");

            entity.Property(e => e.EmployeeHistoryId).HasColumnName("employee_history_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.Link)
                .HasMaxLength(255)
                .HasColumnName("link");
            entity.Property(e => e.Stocks)
                .HasColumnType("jsonb")
                .HasColumnName("stocks");
            entity.Property(e => e.StartDateOfWorkInCurrentPosition).HasColumnName("start_date_of_work_in_current_position");
            entity.Property(e => e.EndDateOfWorkInCurrentPosition).HasColumnName("end_date_of_work_in_current_position");
            entity.Property(e => e.StartDateOfWorkIncurrentStock).HasColumnName("start_date_of_work_in_stock");
            entity.Property(e => e.EndDateOfWorkInCurrentStock).HasColumnName("end_date_of_work_in_stock");

            entity.HasOne(d => d.Employee).WithMany(p => p.EmployeeHistories)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("employee_history_employee_id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.EmployeeHistories)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("employee_history_position_id_fkey");
        });

        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.PositionId).HasName("position_pkey");

            entity.ToTable("position");

            entity.HasIndex(e => e.Name, "position_name_key").IsUnique();

            entity.Property(e => e.PositionId).HasColumnName("position_id");
            entity.Property(e => e.InterfaceAccesses)
                .HasColumnType("jsonb")
                .HasColumnName("interface_accesses");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.QuarterlyBonus)
                .HasPrecision(10, 2)
                .HasColumnName("quarterly_bonus");
            entity.Property(e => e.Salary)
                .HasPrecision(10, 2)
                .HasColumnName("salary");
        });

        modelBuilder.Entity<Stock>(entity =>
        {
            entity.HasKey(e => e.StockId).HasName("stock_pkey");

            entity.ToTable("stock");

            entity.Property(e => e.StockId)
                .HasColumnName("stock_id");

            entity.Property(e => e.StockName)
                .HasMaxLength(255)
                .HasColumnName("name");

            entity.Property(e => e.Links)
                .HasColumnType("jsonb")
                .HasColumnName("links");
        });

        modelBuilder.Entity<Shift>(entity =>
        {
            entity.HasKey(e => e.ShiftId).HasName("shift_pkey");

            entity.ToTable("shifts");

            entity.Property(e => e.ShiftId)
                .HasColumnName("shift_id");

            entity.Property(e => e.StockId)
                .HasColumnName("stock_id");

            entity.Property(e => e.EmployeeWhoPostedTheShiftId)
                .HasColumnName("employee_who_posted_the_shift_id");

            entity.Property(e => e.DayOrNight)
                .HasMaxLength(255)
                .HasColumnName("day_or_night");

            entity.Property(e => e.OpeningDateAndTime)
                .IsRequired(false)
                .HasColumnName("opening_date_and_time");

            entity.Property(e => e.Employees)
                .HasColumnType("jsonb")
                .HasColumnName("employees");

            entity.Property(e => e.ClosingDateAndTime)
                .IsRequired(false)
                .HasColumnName("closing_date_and_time");

            entity.Property(e => e.LastUpdate)
                .HasColumnName("last_update")
                .IsRequired(false);

            entity.HasOne(d => d.Stock).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.StockId)
                .HasConstraintName("shift_stock_id_fkey");

            entity.HasOne(d => d.EmployeeWhoPostedTheShift).WithMany(p => p.Shifts)
                .HasForeignKey(d => d.EmployeeWhoPostedTheShiftId)
                .HasConstraintName("shift_employee_who_posted_the_shift_id_fkey");
        });

        modelBuilder.Entity<ShiftInfo>(entity =>
        {
            entity.HasKey(e => e.ShiftInfoId).HasName("shift_info_pkey");

            entity.ToTable("shift_infos");

            entity.Property(e => e.ShiftInfoId)
                .HasColumnName("shift_info_id");

            entity.Property(e => e.ShiftHistoryId)
                .HasColumnName("shift_history_id");

            entity.Property(e => e.EmployeeId)
                .HasColumnName("employee_id");

            entity.Property(e => e.DateAndTimeOfArrival)
                .HasColumnName("date_and_time_of_arrival");

            entity.Property(e => e.DayOrNight)
                .HasMaxLength(255)
                .HasColumnName("day_or_night");

            entity.Property(e => e.NumberOfHoursWorked)
                .HasColumnName("number_of_hours_worked");

            entity.Property(e => e.Penalty)
                .HasColumnName("penalty")
                .IsRequired(false);

            entity.Property(e => e.PenaltyComment)
                .HasMaxLength(255)
                .HasColumnName("penalty_comment")
                .IsRequired(false);

            entity.Property(e => e.Send)
                .HasColumnName("send")
                .IsRequired(false);

            entity.Property(e => e.SendComment)
                .HasMaxLength(255)
                .HasColumnName("send_comment")
                .IsRequired(false);

            entity.HasIndex(e => e.DateAndTimeOfArrival);

            entity.HasIndex(e => e.EmployeeId);

            entity.HasOne(d => d.ShiftHistory).WithMany(p => p.ShiftInfos)
                .HasForeignKey(d => d.ShiftHistoryId)
                .HasConstraintName("shift_info_shift_history_id_fkey");

            entity.HasOne(d => d.Employee).WithMany(p => p.ShiftInfos)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("shift_info_employee_id_fkey");
        });

        modelBuilder.Entity<ShiftHistory>(entity =>
        {
            entity.HasKey(e => e.ShiftHistoryId).HasName("shift_history_pkey");

            entity.ToTable("shift_history");

            entity.Property(e => e.ShiftHistoryId)
                .HasColumnName("shift_history_id");

            entity.Property(e => e.StockId)
                .HasColumnName("stock_id");

            entity.Property(e => e.EmployeeWhoPostedTheShiftId)
                .HasColumnName("employee_who_posted_the_shift_id");

            entity.Property(e => e.DayOrNight)
                .HasMaxLength(255)
                .HasColumnName("day_or_night");

            entity.Property(e => e.OpeningDateAndTime)
                .HasColumnName("opening_date_and_time");

            entity.Property(e => e.Employees)
                .HasColumnType("jsonb")
                .HasColumnName("employees");

            entity.Property(e => e.ClosingDateAndTime)
                .HasColumnName("closing_date_and_time");

            entity.Property(e => e.LastUpdate)
                .HasColumnName("last_update")
                .IsRequired(false);

            entity.HasOne(d => d.Stock).WithMany(p => p.ShiftHistories)
                .HasForeignKey(d => d.StockId);

            entity.HasOne(d => d.EmployeeWhoPostedTheShift).WithMany(p => p.ShiftHistories)
                .HasForeignKey(d => d.EmployeeWhoPostedTheShiftId);
        });

        modelBuilder.Entity<Mark>(entity =>
        {
            entity.HasKey(e => e.MarkId).HasName("mark_pkey");

            entity.ToTable("marks");

            entity.Property(e => e.MarkId)
                .HasColumnName("mark_id");

            entity.Property(e => e.StockId)
                .HasColumnName("stock_id");

            entity.Property(e => e.EmployeeId)
                .HasColumnName("employee_id");

            entity.Property(e => e.MarkDate)
                .HasColumnName("mark_date");

            entity.HasOne(d => d.Stock).WithMany(p => p.Marks)
                .HasForeignKey(d => d.StockId);

            entity.HasOne(d => d.Employee).WithMany(p => p.Marks)
                .HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<WorkPlan>(entity =>
        {
            entity.HasKey(e => e.WorkPlanId);

            entity.ToTable("work_plans");

            entity.Property(e => e.Month)
                .HasColumnName("month");

            entity.Property(e => e.Year)
                .HasColumnName("year");

            entity.Property(e => e.EmployeeId)
                .HasColumnName("employee_id");

            entity.Property(e => e.NumberOfDayShifts)
                .HasColumnName("number_of_day_shifts");

            entity.Property(e => e.NumberOfHoursPerDayShift)
                .HasColumnName("number_of_hours_per_day_shift");

            entity.Property(e => e.NumberOfNightShifts)
                .HasColumnName("number_of_night_shifts");

            entity.Property(e => e.NumberOfHoursPerNightShift)
                .HasColumnName("number_of_hours_per_night_shift");

            entity.HasOne(d => d.Employee).WithMany(p => p.WorkPlans)
                .HasForeignKey(d => d.EmployeeId);
        });

        modelBuilder.Entity<Accounting>(entity =>
        {
            entity.HasKey(a => new { a.EmployeeId, a.Month, a.Year });

            entity.ToTable("accountings");

            entity.Property(e => e.EmployeeId)
                .HasColumnName("employee_id");

            entity.Property(e => e.Month)
                .HasColumnName("month");

            entity.Property(e => e.Year)
                .HasColumnName("year");

            entity.Property(e => e.WorkPlanId)
                .HasColumnName("work_plan_id");

            entity.Property(e => e.OvertimeDay)
                .HasColumnName("overtime_day");

            entity.Property(e => e.OvertimeNight)
                .HasColumnName("overtime_night");

            entity.Property(e => e.SalaryForShift)
                .HasColumnName("salary_for_shift")
                .HasPrecision(10, 2);

            entity.Property(e => e.SalaryForHour)
                .HasColumnName("salary_for_hour")
                .HasPrecision(10, 2);

            entity.Property(e => e.Mentoring)
                .HasColumnName("mentoring")
                .HasPrecision(10, 2);

            entity.Property(e => e.Seniority)
                .HasColumnName("seniority")
                .HasPrecision(10, 2);

            entity.Property(e => e.Teaching)
                .HasColumnName("teaching")
                .HasPrecision(10, 2);

            entity.Property(e => e.Bonus)
                .HasColumnName("bonus")
                .HasPrecision(10, 2);

            entity.Property(e => e.Vacation)
                .HasColumnName("vacation")
                .HasPrecision(10, 2);

            entity.Property(e => e.Earned)
                .HasColumnName("earned")
                .HasPrecision(10, 2);

            entity.Property(e => e.Advance)
                .HasColumnName("advance")
                .HasPrecision(10, 2);

            entity.Property(e => e.Penalties)
                .HasColumnName("penalties")
                .HasPrecision(10, 2);

            entity.Property(e => e.Sends)
                .HasColumnName("sends")
                .HasPrecision(10, 2);

            entity.Property(e => e.Payment)
                .HasColumnName("payment")
                .HasPrecision(10, 2);
        });

        modelBuilder.Entity<AccountingHistory>(entity =>
        {
            entity.HasKey(e => e.AccountingHistoryId);

            entity.ToTable("accounting_history");

            entity.Property(e => e.AccountingHistoryId)
                .HasColumnName("accounting_history_id");

            entity.Property(e => e.EmployeeId)
                .HasColumnName("employee_id");

            entity.Property(e => e.Year)
                .HasColumnName("year");
            
            entity.Property(e => e.Month)
                .HasColumnName("month");

            entity.Property(e => e.MentoringPrev)
                .HasColumnName("mentoring_prev");

            entity.Property(e => e.Mentoring)
                .HasColumnName("mentoring");

            entity.Property(e => e.TeachingPrev)
                .HasColumnName("teaching_prev");

            entity.Property(e => e.Teaching)
                .HasColumnName("teaching");

            entity.Property(e => e.BonusPrev)
                .HasColumnName("bonus_prev");

            entity.Property(e => e.Bonus)
                .HasColumnName("bonus");

            entity.Property(e => e.VacationPrev)
                .HasColumnName("vacation_prev");
            
            entity.Property(e => e.Vacation)
                .HasColumnName("vacation");
            
            entity.Property(e => e.AdvancePrev)
                .HasColumnName("advance_prev");

            entity.Property(e => e.Advance)
                .HasColumnName("advance");

            entity.Property(e => e.UpdateDate)
                .HasColumnName("update_time");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}