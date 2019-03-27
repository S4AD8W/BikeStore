using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BikeStore.Infrastructure.Migrations
{
    public partial class _0001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ForksNotifications",
                columns: table => new
                {
                    IdxForkNotfication = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Dscr = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ForksModel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForksNotifications", x => x.IdxForkNotfication);
                });

            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    ProductID = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    Descryption = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.ProductID);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Id = table.Column<Guid>(nullable: false),
                    IsEmailConfirm = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Salt = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForksNotifications");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
