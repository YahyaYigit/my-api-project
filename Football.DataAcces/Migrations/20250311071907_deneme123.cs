using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Basketball.DataAcces.Migrations
{
    /// <inheritdoc />
    public partial class deneme123 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FatherWhatsappGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MotherWhatsappGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "WhatsappGroup",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptedFatherWhatsappGroup",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptedMotherWhatsappGroup",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAcceptedWhatsappGroup",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAcceptedFatherWhatsappGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAcceptedMotherWhatsappGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAcceptedWhatsappGroup",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "FatherWhatsappGroup",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MotherWhatsappGroup",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "WhatsappGroup",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
