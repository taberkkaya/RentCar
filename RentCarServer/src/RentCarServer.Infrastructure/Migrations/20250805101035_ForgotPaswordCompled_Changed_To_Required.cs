using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentCarServer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForgotPaswordCompled_Changed_To_Required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsForgotPasswordCompleted_Value",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsForgotPasswordCompleted_Value",
                table: "Users",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
