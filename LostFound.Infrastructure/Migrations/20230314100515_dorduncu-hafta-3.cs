using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LostFound.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dorduncuhafta3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "phoneNumber",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "phoneNumber",
                table: "Users");
        }
    }
}
