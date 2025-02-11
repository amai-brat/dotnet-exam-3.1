﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicTacToe.AuthService.Migrations
{
    /// <inheritdoc />
    public partial class UserConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_users_login",
                table: "users",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_users_login",
                table: "users");
        }
    }
}
