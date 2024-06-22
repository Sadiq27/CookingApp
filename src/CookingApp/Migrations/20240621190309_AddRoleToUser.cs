using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CookingApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRoleToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$IUbeF4/uaysRkd5nSgm4reeW0jZV479nx2PNnnO.Ths6hZn/8.MwO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "Password",
                value: "$2a$11$e.5mQ833mYjParaBtjUTYe.pXZ./nkfouFwHmbRfcxSUQl8akrbta");
        }
    }
}
