using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace dotnet_rpg.Migrations
{
    public partial class Skill_two : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skills_Character_CharecterId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_CharecterId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "CharecterId",
                table: "Skills");

            migrationBuilder.CreateTable(
                name: "CharecterSkill",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    SkillsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharecterSkill", x => new { x.CharactersId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_CharecterSkill_Character_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharecterSkill_Skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharecterSkill_SkillsId",
                table: "CharecterSkill",
                column: "SkillsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharecterSkill");

            migrationBuilder.AddColumn<int>(
                name: "CharecterId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_CharecterId",
                table: "Skills",
                column: "CharecterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_Character_CharecterId",
                table: "Skills",
                column: "CharecterId",
                principalTable: "Character",
                principalColumn: "Id");
        }
    }
}
