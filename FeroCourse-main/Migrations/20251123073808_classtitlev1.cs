using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FeroCourse.Migrations
{
    /// <inheritdoc />
    public partial class classtitlev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Classes",
                newName: "ClassTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ClassTitle",
                table: "Classes",
                newName: "Title");
        }
    }
}
