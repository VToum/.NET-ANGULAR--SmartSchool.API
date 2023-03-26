using Microsoft.EntityFrameworkCore.Migrations;

namespace SmartSchool.API.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AlunoDisciplinaAlunoId",
                table: "Professores",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AlunoDisciplinaDisciplinaId",
                table: "Professores",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Professores_AlunoDisciplinaAlunoId_AlunoDisciplinaDisciplinaId",
                table: "Professores",
                columns: new[] { "AlunoDisciplinaAlunoId", "AlunoDisciplinaDisciplinaId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Professores_AlunoDisciplinas_AlunoDisciplinaAlunoId_AlunoDisciplinaDisciplinaId",
                table: "Professores",
                columns: new[] { "AlunoDisciplinaAlunoId", "AlunoDisciplinaDisciplinaId" },
                principalTable: "AlunoDisciplinas",
                principalColumns: new[] { "AlunoId", "DisciplinaId" },
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Professores_AlunoDisciplinas_AlunoDisciplinaAlunoId_AlunoDisciplinaDisciplinaId",
                table: "Professores");

            migrationBuilder.DropIndex(
                name: "IX_Professores_AlunoDisciplinaAlunoId_AlunoDisciplinaDisciplinaId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "AlunoDisciplinaAlunoId",
                table: "Professores");

            migrationBuilder.DropColumn(
                name: "AlunoDisciplinaDisciplinaId",
                table: "Professores");
        }
    }
}
