using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsermanagementIWithIdentity.Data.Migrations
{
    public partial class Seed_UserRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into AspNetUserRoles(UserId,RoleId) SELECT 'd957b143-fa24-4c32-a4d2-f06d316ec936',Id from AspNetRoles");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from AspNetUserRoles");
        }
    }
}
