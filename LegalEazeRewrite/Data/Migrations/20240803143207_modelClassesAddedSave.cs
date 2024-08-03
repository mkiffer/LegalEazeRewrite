using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LegalEazeRewrite.Data.Migrations
{
    /// <inheritdoc />
    public partial class modelClassesAddedSave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactInfo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientID);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    CourtID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.CourtID);
                });

            migrationBuilder.CreateTable(
                name: "LawFirms",
                columns: table => new
                {
                    LawFirmID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawFirms", x => x.LawFirmID);
                });

            migrationBuilder.CreateTable(
                name: "Matters",
                columns: table => new
                {
                    MatterID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CourtID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matters", x => x.MatterID);
                    table.ForeignKey(
                        name: "FK_Matters_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_Matters_Courts_CourtID",
                        column: x => x.CourtID,
                        principalTable: "Courts",
                        principalColumn: "CourtID");
                });

            migrationBuilder.CreateTable(
                name: "Lawyers",
                columns: table => new
                {
                    LawyerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LawFirmID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lawyers", x => x.LawyerID);
                    table.ForeignKey(
                        name: "FK_Lawyers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Lawyers_LawFirms_LawFirmID",
                        column: x => x.LawFirmID,
                        principalTable: "LawFirms",
                        principalColumn: "LawFirmID");
                });

            migrationBuilder.CreateTable(
                name: "Managers",
                columns: table => new
                {
                    ManagerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LawFirmID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Managers", x => x.ManagerID);
                    table.ForeignKey(
                        name: "FK_Managers_AspNetUsers_UserID",
                        column: x => x.UserID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Managers_LawFirms_LawFirmID",
                        column: x => x.LawFirmID,
                        principalTable: "LawFirms",
                        principalColumn: "LawFirmID");
                });

            migrationBuilder.CreateTable(
                name: "LawyerClients",
                columns: table => new
                {
                    LawyerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClientID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerClients", x => new { x.LawyerID, x.ClientID });
                    table.ForeignKey(
                        name: "FK_LawyerClients_Clients_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Clients",
                        principalColumn: "ClientID");
                    table.ForeignKey(
                        name: "FK_LawyerClients_Lawyers_LawyerID",
                        column: x => x.LawyerID,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerID");
                });

            migrationBuilder.CreateTable(
                name: "LawyerManagers",
                columns: table => new
                {
                    LawyerID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ManagerID = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LawyerManagers", x => new { x.LawyerID, x.ManagerID });
                    table.ForeignKey(
                        name: "FK_LawyerManagers_Lawyers_LawyerID",
                        column: x => x.LawyerID,
                        principalTable: "Lawyers",
                        principalColumn: "LawyerID");
                    table.ForeignKey(
                        name: "FK_LawyerManagers_Managers_ManagerID",
                        column: x => x.ManagerID,
                        principalTable: "Managers",
                        principalColumn: "ManagerID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_LawyerClients_ClientID",
                table: "LawyerClients",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_LawyerManagers_ManagerID",
                table: "LawyerManagers",
                column: "ManagerID");

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_LawFirmID",
                table: "Lawyers",
                column: "LawFirmID");

            migrationBuilder.CreateIndex(
                name: "IX_Lawyers_UserID",
                table: "Lawyers",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Managers_LawFirmID",
                table: "Managers",
                column: "LawFirmID");

            migrationBuilder.CreateIndex(
                name: "IX_Managers_UserID",
                table: "Managers",
                column: "UserID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Matters_ClientID",
                table: "Matters",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Matters_CourtID",
                table: "Matters",
                column: "CourtID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LawyerClients");

            migrationBuilder.DropTable(
                name: "LawyerManagers");

            migrationBuilder.DropTable(
                name: "Matters");

            migrationBuilder.DropTable(
                name: "Lawyers");

            migrationBuilder.DropTable(
                name: "Managers");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "LawFirms");
        }
    }
}
