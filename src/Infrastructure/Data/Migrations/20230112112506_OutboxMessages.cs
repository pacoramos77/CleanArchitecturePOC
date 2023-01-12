using System;

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class OutboxMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Done",
                table: "ToDoItems",
                type: "bit",
                nullable: false,
                defaultValue: false
            );

            migrationBuilder.CreateTable(
                name: "OutboxMessages",
                columns: table =>
                    new
                    {
                        Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                        Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                        OcurredOnUtc = table.Column<DateTime>(type: "datetime2", nullable: false),
                        ProcessedOnUtc = table.Column<DateTime>(type: "datetime2", nullable: true),
                        Error = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    },
                constraints: table => table.PrimaryKey("PK_OutboxMessages", x => x.Id)
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "OutboxMessages");

            migrationBuilder.DropColumn(name: "Done", table: "ToDoItems");
        }
    }
}
