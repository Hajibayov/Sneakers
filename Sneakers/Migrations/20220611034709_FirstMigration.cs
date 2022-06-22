using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sneakers.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMPLOYEE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EMAIL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WORK_ENTER = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SIZE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SIZE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SIZE_SNEAKERS_CONNECTION",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SNEAKERS_ID = table.Column<int>(type: "int", nullable: false),
                    SIZE_ID = table.Column<int>(type: "int", nullable: false),
                    QUANTITY = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SIZE_SNEAKERS_CONNECTION", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SNEAKERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WAREHOUSE_ID = table.Column<int>(type: "int", nullable: false),
                    TYPE_ID = table.Column<int>(type: "int", nullable: false),
                    BRAND_ID = table.Column<int>(type: "int", nullable: false),
                    MODEL_ID = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<int>(type: "int", nullable: false),
                    CREATED_AT = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UPDATED_AT = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CREATED_BY = table.Column<int>(type: "int", nullable: false),
                    UPDATED_BY = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SNEAKERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SNEAKERS_BRAND",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BRAND = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SNEAKERS_BRAND", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SNEAKERS_MODEL",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MODEL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SNEAKERS_MODEL", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "SNEAKERS_TYPE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TYPE = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SNEAKERS_TYPE", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WAREHOUSE",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LOCATION = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CAPACITY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ZIP = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WAREHOUSE", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "SIZE");

            migrationBuilder.DropTable(
                name: "SIZE_SNEAKERS_CONNECTION");

            migrationBuilder.DropTable(
                name: "SNEAKERS");

            migrationBuilder.DropTable(
                name: "SNEAKERS_BRAND");

            migrationBuilder.DropTable(
                name: "SNEAKERS_MODEL");

            migrationBuilder.DropTable(
                name: "SNEAKERS_TYPE");

            migrationBuilder.DropTable(
                name: "WAREHOUSE");
        }
    }
}
