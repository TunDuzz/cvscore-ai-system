using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CVScore.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddInterviewLanguage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "InterviewProfiles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Language",
                table: "InterviewProfiles");
        }
    }
}
