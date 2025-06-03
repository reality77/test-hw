using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobOffers.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "job_offers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    source = table.Column<byte>(type: "INTEGER", nullable: false),
                    external_id = table.Column<string>(type: "TEXT", nullable: false),
                    title = table.Column<string>(type: "TEXT", nullable: false),
                    description = table.Column<string>(type: "TEXT", nullable: true),
                    company_name = table.Column<string>(type: "TEXT", nullable: true),
                    location = table.Column<string>(type: "TEXT", nullable: true),
                    posted_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    contract_type = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_job_offers", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_job_offers_source_external_id",
                table: "job_offers",
                columns: new[] { "source", "external_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "job_offers");
        }
    }
}
