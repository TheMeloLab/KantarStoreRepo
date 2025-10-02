using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KantarStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBasketStatusField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Baskets");
        }
    }
}
