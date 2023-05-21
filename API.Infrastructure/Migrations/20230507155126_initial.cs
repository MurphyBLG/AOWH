using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AOWH.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "position",
                columns: table => new
                {
                    position_id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    salary = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    quarterly_bonus = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    interface_accesses = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("position_pkey", x => x.position_id);
                });

            migrationBuilder.CreateTable(
                name: "stock",
                columns: table => new
                {
                    stock_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    links = table.Column<string>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("stock_pkey", x => x.stock_id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Password = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    surname = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    passport_number = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    passport_issuer = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    passport_issue_date = table.Column<DateOnly>(type: "date", nullable: false),
                    start_of_total_seniority = table.Column<DateOnly>(type: "date", nullable: false),
                    start_of_luch_seniority = table.Column<DateOnly>(type: "date", nullable: false),
                    date_of_termination = table.Column<DateOnly>(type: "date", nullable: true),
                    position_id = table.Column<Guid>(type: "uuid", nullable: true),
                    date_of_start_in_the_current_position = table.Column<DateOnly>(type: "date", nullable: true),
                    salary = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    quarterly_bonus = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    percentage_of_salary_in_advance = table.Column<int>(type: "integer", nullable: false),
                    link = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    DateOfStartInTheCurrentLink = table.Column<DateOnly>(type: "date", nullable: true),
                    stocks = table.Column<string>(type: "jsonb", nullable: true),
                    date_of_start_in_the_current_stock = table.Column<DateOnly>(type: "date", nullable: true),
                    forklift_control = table.Column<bool>(type: "boolean", nullable: false),
                    rolleyes_control = table.Column<bool>(type: "boolean", nullable: false),
                    refresh_token = table.Column<string>(type: "text", nullable: false),
                    refresh_token_expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_pkey", x => x.employee_id);
                    table.ForeignKey(
                        name: "employee_position_id_fkey",
                        column: x => x.position_id,
                        principalTable: "position",
                        principalColumn: "position_id");
                });

            migrationBuilder.CreateTable(
                name: "accounting_history",
                columns: table => new
                {
                    accounting_history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    mentoring = table.Column<decimal>(type: "numeric", nullable: false),
                    teaching = table.Column<decimal>(type: "numeric", nullable: false),
                    bonus = table.Column<decimal>(type: "numeric", nullable: false),
                    vacation = table.Column<decimal>(type: "numeric", nullable: false),
                    advance = table.Column<decimal>(type: "numeric", nullable: false),
                    mentoring_prev = table.Column<decimal>(type: "numeric", nullable: false),
                    teaching_prev = table.Column<decimal>(type: "numeric", nullable: false),
                    bonus_prev = table.Column<decimal>(type: "numeric", nullable: false),
                    vacation_prev = table.Column<decimal>(type: "numeric", nullable: false),
                    advance_prev = table.Column<decimal>(type: "numeric", nullable: false),
                    update_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounting_history", x => x.accounting_history_id);
                    table.ForeignKey(
                        name: "FK_accounting_history_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "employee_history",
                columns: table => new
                {
                    employee_history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Surname = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    PassportNumber = table.Column<string>(type: "text", nullable: false),
                    PassportIssuer = table.Column<string>(type: "text", nullable: true),
                    PassportIssueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    StartOfTotalSeniority = table.Column<DateOnly>(type: "date", nullable: false),
                    StartOfLuchSeniority = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfTermination = table.Column<DateOnly>(type: "date", nullable: true),
                    position_id = table.Column<Guid>(type: "uuid", nullable: true),
                    start_date_of_work_in_current_position = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date_of_work_in_current_position = table.Column<DateOnly>(type: "date", nullable: true),
                    Salary = table.Column<decimal>(type: "numeric", nullable: false),
                    QuarterlyBonus = table.Column<decimal>(type: "numeric", nullable: false),
                    PercentageOfSalaryInAdvance = table.Column<int>(type: "integer", nullable: false),
                    link = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    StartDateOfWorkInCurrentLink = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDateOfWorkInCurrentLink = table.Column<DateOnly>(type: "date", nullable: true),
                    stocks = table.Column<string>(type: "jsonb", nullable: true),
                    start_date_of_work_in_stock = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date_of_work_in_stock = table.Column<DateOnly>(type: "date", nullable: true),
                    ForkliftControl = table.Column<bool>(type: "boolean", nullable: false),
                    RolleyesControl = table.Column<bool>(type: "boolean", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("employee_history_pkey", x => x.employee_history_id);
                    table.ForeignKey(
                        name: "employee_history_employee_id_fkey",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "employee_history_position_id_fkey",
                        column: x => x.position_id,
                        principalTable: "position",
                        principalColumn: "position_id");
                });

            migrationBuilder.CreateTable(
                name: "marks",
                columns: table => new
                {
                    mark_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_id = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    mark_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("mark_pkey", x => x.mark_id);
                    table.ForeignKey(
                        name: "FK_marks_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_marks_stock_stock_id",
                        column: x => x.stock_id,
                        principalTable: "stock",
                        principalColumn: "stock_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shift_history",
                columns: table => new
                {
                    shift_history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_id = table.Column<int>(type: "integer", nullable: false),
                    employee_who_posted_the_shift_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_or_night = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    opening_date_and_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    employees = table.Column<string>(type: "jsonb", nullable: false),
                    closing_date_and_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shift_history_pkey", x => x.shift_history_id);
                    table.ForeignKey(
                        name: "FK_shift_history_employee_employee_who_posted_the_shift_id",
                        column: x => x.employee_who_posted_the_shift_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_shift_history_stock_stock_id",
                        column: x => x.stock_id,
                        principalTable: "stock",
                        principalColumn: "stock_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shifts",
                columns: table => new
                {
                    shift_id = table.Column<Guid>(type: "uuid", nullable: false),
                    stock_id = table.Column<int>(type: "integer", nullable: false),
                    employee_who_posted_the_shift_id = table.Column<Guid>(type: "uuid", nullable: false),
                    day_or_night = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    opening_date_and_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    employees = table.Column<string>(type: "jsonb", nullable: false),
                    closing_date_and_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    last_update = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shift_pkey", x => x.shift_id);
                    table.ForeignKey(
                        name: "shift_employee_who_posted_the_shift_id_fkey",
                        column: x => x.employee_who_posted_the_shift_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "shift_stock_id_fkey",
                        column: x => x.stock_id,
                        principalTable: "stock",
                        principalColumn: "stock_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "work_plans",
                columns: table => new
                {
                    WorkPlanId = table.Column<Guid>(type: "uuid", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    number_of_day_shifts = table.Column<int>(type: "integer", nullable: false),
                    number_of_hours_per_day_shift = table.Column<int>(type: "integer", nullable: false),
                    number_of_night_shifts = table.Column<int>(type: "integer", nullable: false),
                    number_of_hours_per_night_shift = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_plans", x => x.WorkPlanId);
                    table.ForeignKey(
                        name: "FK_work_plans_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "shift_infos",
                columns: table => new
                {
                    shift_info_id = table.Column<Guid>(type: "uuid", nullable: false),
                    shift_history_id = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    date_and_time_of_arrival = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    day_or_night = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    number_of_hours_worked = table.Column<int>(type: "integer", nullable: false),
                    penalty = table.Column<decimal>(type: "numeric", nullable: true),
                    penalty_comment = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    send = table.Column<decimal>(type: "numeric", nullable: true),
                    send_comment = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("shift_info_pkey", x => x.shift_info_id);
                    table.ForeignKey(
                        name: "shift_info_employee_id_fkey",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "shift_info_shift_history_id_fkey",
                        column: x => x.shift_history_id,
                        principalTable: "shift_history",
                        principalColumn: "shift_history_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "accountings",
                columns: table => new
                {
                    employee_id = table.Column<Guid>(type: "uuid", nullable: false),
                    month = table.Column<int>(type: "integer", nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    work_plan_id = table.Column<Guid>(type: "uuid", nullable: false),
                    overtime_day = table.Column<int>(type: "integer", nullable: false),
                    overtime_night = table.Column<int>(type: "integer", nullable: false),
                    salary_for_shift = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    salary_for_hour = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    mentoring = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    seniority = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    teaching = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    bonus = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    vacation = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    earned = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    advance = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    penalties = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    sends = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    payment = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accountings", x => new { x.employee_id, x.month, x.year });
                    table.ForeignKey(
                        name: "FK_accountings_employee_employee_id",
                        column: x => x.employee_id,
                        principalTable: "employee",
                        principalColumn: "employee_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_accountings_work_plans_work_plan_id",
                        column: x => x.work_plan_id,
                        principalTable: "work_plans",
                        principalColumn: "WorkPlanId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_accounting_history_employee_id",
                table: "accounting_history",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_accountings_work_plan_id",
                table: "accountings",
                column: "work_plan_id");

            migrationBuilder.CreateIndex(
                name: "employee_passport_number_key",
                table: "employee",
                column: "passport_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_employee_position_id",
                table: "employee",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_history_employee_id",
                table: "employee_history",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_employee_history_position_id",
                table: "employee_history",
                column: "position_id");

            migrationBuilder.CreateIndex(
                name: "IX_marks_employee_id",
                table: "marks",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_marks_stock_id",
                table: "marks",
                column: "stock_id");

            migrationBuilder.CreateIndex(
                name: "position_name_key",
                table: "position",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_shift_history_employee_who_posted_the_shift_id",
                table: "shift_history",
                column: "employee_who_posted_the_shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_history_stock_id",
                table: "shift_history",
                column: "stock_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_infos_date_and_time_of_arrival",
                table: "shift_infos",
                column: "date_and_time_of_arrival");

            migrationBuilder.CreateIndex(
                name: "IX_shift_infos_employee_id",
                table: "shift_infos",
                column: "employee_id");

            migrationBuilder.CreateIndex(
                name: "IX_shift_infos_shift_history_id",
                table: "shift_infos",
                column: "shift_history_id");

            migrationBuilder.CreateIndex(
                name: "IX_shifts_employee_who_posted_the_shift_id",
                table: "shifts",
                column: "employee_who_posted_the_shift_id");

            migrationBuilder.CreateIndex(
                name: "IX_shifts_stock_id",
                table: "shifts",
                column: "stock_id");

            migrationBuilder.CreateIndex(
                name: "IX_work_plans_employee_id",
                table: "work_plans",
                column: "employee_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounting_history");

            migrationBuilder.DropTable(
                name: "accountings");

            migrationBuilder.DropTable(
                name: "employee_history");

            migrationBuilder.DropTable(
                name: "marks");

            migrationBuilder.DropTable(
                name: "shift_infos");

            migrationBuilder.DropTable(
                name: "shifts");

            migrationBuilder.DropTable(
                name: "work_plans");

            migrationBuilder.DropTable(
                name: "shift_history");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "stock");

            migrationBuilder.DropTable(
                name: "position");
        }
    }
}
