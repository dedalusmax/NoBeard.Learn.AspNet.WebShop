using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Migrations;
using NoBeard.Learn.AspNet.WebShop.App.Models;

#nullable disable

namespace NoBeard.Learn.AspNet.WebShop.App.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdminUsersRoles : Migration
    {
        private const string ADMIN_ROLE_ID = "A3EFCE80-5BBC-478F-8554-F5FA5C315635";

        private const string ADMIN_ROLE_NAME = "Admin";

        private const string ADMIN_USER_ID = "D4C12CE8-A3E7-4707-AB0C-601D361FF0F5";

        private const string ADMIN_USER_NAME = "admin@webshop.hr";

        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            var pwd = hasher.HashPassword(null, "Password123!");

            migrationBuilder.Sql($@"
                BEGIN TRANSACTION;

                INSERT INTO [AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp])
                VALUES ('{ADMIN_ROLE_ID}', '{ADMIN_ROLE_NAME}', '{ADMIN_ROLE_NAME.ToUpper()}', NEWID());

                INSERT INTO [AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount])
                VALUES ('{ADMIN_USER_ID}', '{ADMIN_USER_NAME}', '{ADMIN_USER_NAME.ToUpper()}', '{ADMIN_USER_NAME}', '{ADMIN_USER_NAME.ToUpper()}', 1, '{pwd}', NEWID(), NEWID(), NULL, 0, 0, NULL, 1, 0);

                INSERT INTO [AspNetUserRoles] ([UserId], [RoleId])
                VALUES ('{ADMIN_USER_ID}', '{ADMIN_ROLE_ID}');

                COMMIT TRANSACTION; 
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"
                BEGIN TRANSACTION;
                DELETE FROM [AspNetUserRoles] WHERE [UserId] = '{ADMIN_USER_ID}' AND [RoleId] = '{ADMIN_ROLE_ID}';
                DELETE FROM [AspNetUsers] WHERE [Id] = '{ADMIN_USER_ID}';
                DELETE FROM [AspNetRoles] WHERE [Id] = '{ADMIN_ROLE_ID}';
                COMMIT TRANSACTION;
            ");
        }
    }
}
