using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    author_phone_number = table.Column<string>(type: "longtext", nullable: false),
                    author_name_first_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    author_name_last_name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    created_by = table.Column<string>(type: "longtext", nullable: true),
                    last_modified_on = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    last_modified_by = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_authors", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "authors");
        }
    }
}
