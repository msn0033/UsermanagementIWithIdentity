using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsermanagementIWithIdentity.Data.Migrations
{
    public partial class PictureField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        
            migrationBuilder.AddColumn<byte[]>(
                name: "profilePicture",
                table: "AspNetUsers",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropColumn(
                name: "profilePicture",
                table: "AspNetUsers");
        }
    }
}
