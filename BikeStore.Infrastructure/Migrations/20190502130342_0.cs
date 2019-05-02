using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BikeStore.Infrastructure.Migrations
{
    public partial class _0 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                    IdxUser = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
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
                    table.PrimaryKey("PK_Users", x => x.IdxUser);
                });

            migrationBuilder.CreateTable(
                name: "ForksNotifications",
                columns: table => new
                {
                    Guid = table.Column<Guid>(nullable: false),
                    CreateAt = table.Column<DateTime>(nullable: false),
                    UpdateAt = table.Column<DateTime>(nullable: false),
                    IdxForkNotfication = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Dscr = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    ForksModel = table.Column<string>(nullable: true),
                    UserIdxUser = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForksNotifications", x => x.IdxForkNotfication);
                    table.ForeignKey(
                        name: "FK_ForksNotifications_Users_UserIdxUser",
                        column: x => x.UserIdxUser,
                        principalTable: "Users",
                        principalColumn: "IdxUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ForkNotficationImages",
                columns: table => new
                {
                    IdxForkNotoificationImage = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    IdxForkNotfication = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Size = table.Column<long>(nullable: false),
                    Content = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForkNotficationImages", x => x.IdxForkNotoificationImage);
                    table.ForeignKey(
                        name: "FK_ForkNotficationImages_ForksNotifications_IdxForkNotfication",
                        column: x => x.IdxForkNotfication,
                        principalTable: "ForksNotifications",
                        principalColumn: "IdxForkNotfication",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ForkNotficationImages_IdxForkNotfication",
                table: "ForkNotficationImages",
                column: "IdxForkNotfication");

            migrationBuilder.CreateIndex(
                name: "IX_ForksNotifications_UserIdxUser",
                table: "ForksNotifications",
                column: "UserIdxUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForkNotficationImages");

            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "ForksNotifications");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
