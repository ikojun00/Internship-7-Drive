using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drive.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Folders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ParentFolderId = table.Column<int>(type: "integer", nullable: true),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Folders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Folders_Folders_ParentFolderId",
                        column: x => x.ParentFolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Folders_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    OwnerId = table.Column<int>(type: "integer", nullable: false),
                    FolderId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Files_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Files_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FolderShares",
                columns: table => new
                {
                    SharedFoldersId = table.Column<int>(type: "integer", nullable: false),
                    SharedWithId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FolderShares", x => new { x.SharedFoldersId, x.SharedWithId });
                    table.ForeignKey(
                        name: "FK_FolderShares_Folders_SharedFoldersId",
                        column: x => x.SharedFoldersId,
                        principalTable: "Folders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FolderShares_Users_SharedWithId",
                        column: x => x.SharedWithId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Content = table.Column<string>(type: "text", nullable: false),
                    FileId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Files_FileId",
                        column: x => x.FileId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Comments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FileShares",
                columns: table => new
                {
                    SharedFilesId = table.Column<int>(type: "integer", nullable: false),
                    SharedWithId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileShares", x => new { x.SharedFilesId, x.SharedWithId });
                    table.ForeignKey(
                        name: "FK_FileShares_Files_SharedFilesId",
                        column: x => x.SharedFilesId,
                        principalTable: "Files",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileShares_Users_SharedWithId",
                        column: x => x.SharedWithId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "PasswordHash", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 14, 46, 35, 899, DateTimeKind.Utc).AddTicks(6244), "john.doe@gmail.com", "oQnjaUetVt4dyhzEnw74rJrZp7GqDfQfs8TLc8H/Aeo=", null },
                    { 2, new DateTime(2024, 12, 31, 14, 46, 35, 899, DateTimeKind.Utc).AddTicks(6950), "jane.smith@gmail.com", "qErJCXpDKt4m+zyvP95FuukV3gEGrD1so8mCtoZtMcE=", null },
                    { 3, new DateTime(2024, 12, 31, 14, 46, 35, 899, DateTimeKind.Utc).AddTicks(6956), "bob.wilson@gmail.com", "qFYB54kbXvV24+kUz/cJy2WVfl9ifGPtY5t3plLC1YY=", null }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "CreatedAt", "Name", "OwnerId", "ParentFolderId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 12, 31, 14, 46, 35, 899, DateTimeKind.Utc).AddTicks(9060), "Documents", 1, null, null },
                    { 2, new DateTime(2024, 12, 31, 14, 46, 35, 899, DateTimeKind.Utc).AddTicks(9710), "Images", 1, null, null },
                    { 3, new DateTime(2024, 12, 31, 14, 46, 35, 899, DateTimeKind.Utc).AddTicks(9714), "Projects", 2, null, null }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "CreatedAt", "FolderId", "Name", "OwnerId", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, "This is a report content", new DateTime(2024, 12, 31, 14, 46, 35, 900, DateTimeKind.Utc).AddTicks(3060), 1, "report.txt", 1, null },
                    { 2, "base64_encoded_image_content", new DateTime(2024, 12, 31, 14, 46, 35, 900, DateTimeKind.Utc).AddTicks(3097), 2, "profile.jpg", 1, null },
                    { 3, "Project timeline and milestones", new DateTime(2024, 12, 31, 14, 46, 35, 900, DateTimeKind.Utc).AddTicks(3101), 3, "project_plan.txt", 2, null }
                });

            migrationBuilder.InsertData(
                table: "FolderShares",
                columns: new[] { "SharedFoldersId", "SharedWithId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Folders",
                columns: new[] { "Id", "CreatedAt", "Name", "OwnerId", "ParentFolderId", "UpdatedAt" },
                values: new object[] { 4, new DateTime(2024, 12, 31, 14, 46, 35, 899, DateTimeKind.Utc).AddTicks(9717), "Work Documents", 2, 3, null });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "CreatedAt", "FileId", "UpdatedAt", "UserId" },
                values: new object[,]
                {
                    { 1, "Great report, but needs more details in section 3", new DateTime(2024, 12, 31, 14, 46, 35, 900, DateTimeKind.Utc).AddTicks(6480), 1, null, 2 },
                    { 2, "Updated the timeline section", new DateTime(2024, 12, 31, 14, 46, 35, 900, DateTimeKind.Utc).AddTicks(6540), 3, null, 1 },
                    { 3, "Please review the changes", new DateTime(2024, 12, 31, 14, 46, 35, 900, DateTimeKind.Utc).AddTicks(6544), 1, null, 1 }
                });

            migrationBuilder.InsertData(
                table: "FileShares",
                columns: new[] { "SharedFilesId", "SharedWithId" },
                values: new object[,]
                {
                    { 1, 2 },
                    { 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Files",
                columns: new[] { "Id", "Content", "CreatedAt", "FolderId", "Name", "OwnerId", "UpdatedAt" },
                values: new object[] { 4, "Notes from team meeting", new DateTime(2024, 12, 31, 14, 46, 35, 900, DateTimeKind.Utc).AddTicks(3104), 4, "meeting_notes.txt", 2, null });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_FileId",
                table: "Comments",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserId",
                table: "Comments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FolderId",
                table: "Files",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_OwnerId",
                table: "Files",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FileShares_SharedWithId",
                table: "FileShares",
                column: "SharedWithId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_OwnerId",
                table: "Folders",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_ParentFolderId",
                table: "Folders",
                column: "ParentFolderId");

            migrationBuilder.CreateIndex(
                name: "IX_FolderShares_SharedWithId",
                table: "FolderShares",
                column: "SharedWithId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "FileShares");

            migrationBuilder.DropTable(
                name: "FolderShares");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropTable(
                name: "Folders");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
