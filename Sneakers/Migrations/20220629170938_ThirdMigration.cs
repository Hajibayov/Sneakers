using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sneakers.Migrations
{
    public partial class ThirdMigration : Migration
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
                    table.ForeignKey(
                        name: "FK_SNEAKERS_SNEAKERS_BRAND_BRAND_ID",
                        column: x => x.BRAND_ID,
                        principalTable: "SNEAKERS_BRAND",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SNEAKERS_SNEAKERS_MODEL_MODEL_ID",
                        column: x => x.MODEL_ID,
                        principalTable: "SNEAKERS_MODEL",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SNEAKERS_SNEAKERS_TYPE_TYPE_ID",
                        column: x => x.TYPE_ID,
                        principalTable: "SNEAKERS_TYPE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SNEAKERS_WAREHOUSE_WAREHOUSE_ID",
                        column: x => x.WAREHOUSE_ID,
                        principalTable: "WAREHOUSE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_SIZE_SNEAKERS_CONNECTION_SIZE_SIZE_ID",
                        column: x => x.SIZE_ID,
                        principalTable: "SIZE",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SIZE_SNEAKERS_CONNECTION_SNEAKERS_SNEAKERS_ID",
                        column: x => x.SNEAKERS_ID,
                        principalTable: "SNEAKERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SIZE_SNEAKERS_CONNECTION_SIZE_ID",
                table: "SIZE_SNEAKERS_CONNECTION",
                column: "SIZE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SIZE_SNEAKERS_CONNECTION_SNEAKERS_ID",
                table: "SIZE_SNEAKERS_CONNECTION",
                column: "SNEAKERS_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SNEAKERS_BRAND_ID",
                table: "SNEAKERS",
                column: "BRAND_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SNEAKERS_MODEL_ID",
                table: "SNEAKERS",
                column: "MODEL_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SNEAKERS_TYPE_ID",
                table: "SNEAKERS",
                column: "TYPE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SNEAKERS_WAREHOUSE_ID",
                table: "SNEAKERS",
                column: "WAREHOUSE_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEE");

            migrationBuilder.DropTable(
                name: "SIZE_SNEAKERS_CONNECTION");

            migrationBuilder.DropTable(
                name: "SIZE");

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
