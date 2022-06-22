using Microsoft.EntityFrameworkCore.Migrations;

namespace Sneakers.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateIndex(
                name: "IX_SIZE_SNEAKERS_CONNECTION_SIZE_ID",
                table: "SIZE_SNEAKERS_CONNECTION",
                column: "SIZE_ID");

            migrationBuilder.CreateIndex(
                name: "IX_SIZE_SNEAKERS_CONNECTION_SNEAKERS_ID",
                table: "SIZE_SNEAKERS_CONNECTION",
                column: "SNEAKERS_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_SIZE_SNEAKERS_CONNECTION_SIZE_SIZE_ID",
                table: "SIZE_SNEAKERS_CONNECTION",
                column: "SIZE_ID",
                principalTable: "SIZE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SIZE_SNEAKERS_CONNECTION_SNEAKERS_SNEAKERS_ID",
                table: "SIZE_SNEAKERS_CONNECTION",
                column: "SNEAKERS_ID",
                principalTable: "SNEAKERS",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SNEAKERS_SNEAKERS_BRAND_BRAND_ID",
                table: "SNEAKERS",
                column: "BRAND_ID",
                principalTable: "SNEAKERS_BRAND",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SNEAKERS_SNEAKERS_MODEL_MODEL_ID",
                table: "SNEAKERS",
                column: "MODEL_ID",
                principalTable: "SNEAKERS_MODEL",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SNEAKERS_SNEAKERS_TYPE_TYPE_ID",
                table: "SNEAKERS",
                column: "TYPE_ID",
                principalTable: "SNEAKERS_TYPE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SNEAKERS_WAREHOUSE_WAREHOUSE_ID",
                table: "SNEAKERS",
                column: "WAREHOUSE_ID",
                principalTable: "WAREHOUSE",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SIZE_SNEAKERS_CONNECTION_SIZE_SIZE_ID",
                table: "SIZE_SNEAKERS_CONNECTION");

            migrationBuilder.DropForeignKey(
                name: "FK_SIZE_SNEAKERS_CONNECTION_SNEAKERS_SNEAKERS_ID",
                table: "SIZE_SNEAKERS_CONNECTION");

            migrationBuilder.DropForeignKey(
                name: "FK_SNEAKERS_SNEAKERS_BRAND_BRAND_ID",
                table: "SNEAKERS");

            migrationBuilder.DropForeignKey(
                name: "FK_SNEAKERS_SNEAKERS_MODEL_MODEL_ID",
                table: "SNEAKERS");

            migrationBuilder.DropForeignKey(
                name: "FK_SNEAKERS_SNEAKERS_TYPE_TYPE_ID",
                table: "SNEAKERS");

            migrationBuilder.DropForeignKey(
                name: "FK_SNEAKERS_WAREHOUSE_WAREHOUSE_ID",
                table: "SNEAKERS");

            migrationBuilder.DropIndex(
                name: "IX_SNEAKERS_BRAND_ID",
                table: "SNEAKERS");

            migrationBuilder.DropIndex(
                name: "IX_SNEAKERS_MODEL_ID",
                table: "SNEAKERS");

            migrationBuilder.DropIndex(
                name: "IX_SNEAKERS_TYPE_ID",
                table: "SNEAKERS");

            migrationBuilder.DropIndex(
                name: "IX_SNEAKERS_WAREHOUSE_ID",
                table: "SNEAKERS");

            migrationBuilder.DropIndex(
                name: "IX_SIZE_SNEAKERS_CONNECTION_SIZE_ID",
                table: "SIZE_SNEAKERS_CONNECTION");

            migrationBuilder.DropIndex(
                name: "IX_SIZE_SNEAKERS_CONNECTION_SNEAKERS_ID",
                table: "SIZE_SNEAKERS_CONNECTION");
        }
    }
}
