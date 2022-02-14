using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsermanagementIWithIdentity.Data.Migrations
{
    public partial class seedUsers : Migration
    {

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FirstName], [LastName], [profilePicture]) " +
                "VALUES (N'd957b143-fa24-4c32-a4d2-f06d316ec936', N'admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEI0RerROjdysocqzas37Fxjmjd5TduJmqbcybyCvS2Qct+HEHiWp7Qjqt60fSRuGJQ==', N'BWHR2ENMXKJGXWQMRMXNQC7UZBCJULKY', N'4e8d9732-0b25-4331-b2b9-77ba3b95967d', NULL, 0, 0, NULL, 1, 0, N'admin', N'admin', NULL)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [dbo].[AspNetUsers] WHERE Id='d957b143-fa24-4c32-a4d2-f06d316ec936'");
        }
    }



}
