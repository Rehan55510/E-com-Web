using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_com_Web.Migrations
{
    /// <inheritdoc />
    public partial class AddShowOnHomeAndSignalR : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowOnHome",
                table: "Shoes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowOnHome",
                table: "Shoes");
        }
    }
}
