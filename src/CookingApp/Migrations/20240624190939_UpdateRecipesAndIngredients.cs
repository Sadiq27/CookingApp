using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateRecipesAndIngredients : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$raqZ/3s.7/5XCS7tsIp7vu6OzXbEoFR9v6dY3TF5KN8sIRuiqPwze");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$iixCIf6vxeHd3iM5T1/F7eFKmQTEg5hFH5pOcXZdRKSgFuJFcnh9m");
        }
    }
}
