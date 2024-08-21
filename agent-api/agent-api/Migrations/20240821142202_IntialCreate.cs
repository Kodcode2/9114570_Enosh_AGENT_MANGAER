using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace agent_api.Migrations
{
    /// <inheritdoc />
    public partial class IntialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LocationModel",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    x = table.Column<int>(type: "int", nullable: false),
                    y = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationModel", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Agents",
                columns: table => new
                {
                    AgentId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentNickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AgentStatus = table.Column<int>(type: "int", nullable: false),
                    AgentLocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agents", x => x.AgentId);
                    table.ForeignKey(
                        name: "FK_Agents_LocationModel_AgentLocationId",
                        column: x => x.AgentLocationId,
                        principalTable: "LocationModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Targets",
                columns: table => new
                {
                    TargetId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TargetName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetRole = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetPicture = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TargetStatus = table.Column<int>(type: "int", nullable: false),
                    TargetLocationId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Targets", x => x.TargetId);
                    table.ForeignKey(
                        name: "FK_Targets_LocationModel_TargetLocationId",
                        column: x => x.TargetLocationId,
                        principalTable: "LocationModel",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Missions",
                columns: table => new
                {
                    MissionId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AgentId = table.Column<long>(type: "bigint", nullable: false),
                    TargetId = table.Column<long>(type: "bigint", nullable: false),
                    MissionFinalLocationId = table.Column<long>(type: "bigint", nullable: false),
                    MissionTime = table.Column<double>(type: "float", nullable: false),
                    MissionStatus = table.Column<int>(type: "int", nullable: false),
                    MissionCompletedTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Missions", x => x.MissionId);
                    table.ForeignKey(
                        name: "FK_Missions_Agents_AgentId",
                        column: x => x.AgentId,
                        principalTable: "Agents",
                        principalColumn: "AgentId");
                    table.ForeignKey(
                        name: "FK_Missions_LocationModel_MissionFinalLocationId",
                        column: x => x.MissionFinalLocationId,
                        principalTable: "LocationModel",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Missions_Targets_TargetId",
                        column: x => x.TargetId,
                        principalTable: "Targets",
                        principalColumn: "TargetId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_AgentLocationId",
                table: "Agents",
                column: "AgentLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_AgentId",
                table: "Missions",
                column: "AgentId");

            migrationBuilder.CreateIndex(
                name: "IX_Missions_MissionFinalLocationId",
                table: "Missions",
                column: "MissionFinalLocationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Missions_TargetId",
                table: "Missions",
                column: "TargetId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Targets_TargetLocationId",
                table: "Targets",
                column: "TargetLocationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Missions");

            migrationBuilder.DropTable(
                name: "Agents");

            migrationBuilder.DropTable(
                name: "Targets");

            migrationBuilder.DropTable(
                name: "LocationModel");
        }
    }
}
