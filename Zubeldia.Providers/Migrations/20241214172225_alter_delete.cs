using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zubeldia.Providers.Migrations
{
    /// <inheritdoc />
    public partial class alter_delete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractObjectives_Contracts_ContractId",
                table: "ContractObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractSalaries_Contracts_ContractId",
                table: "ContractSalaries");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractTrajectories_Contracts_ContractId",
                table: "ContractTrajectories");

            migrationBuilder.AddForeignKey(
                name: "FK_Objectives_Contracts_Cascade",
                table: "ContractObjectives",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Contracts_Cascade",
                table: "ContractSalaries",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Trajectories_Contracts_Cascade",
                table: "ContractTrajectories",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Objectives_Contracts_Cascade",
                table: "ContractObjectives");

            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Contracts_Cascade",
                table: "ContractSalaries");

            migrationBuilder.DropForeignKey(
                name: "FK_Trajectories_Contracts_Cascade",
                table: "ContractTrajectories");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractObjectives_Contracts_ContractId",
                table: "ContractObjectives",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractSalaries_Contracts_ContractId",
                table: "ContractSalaries",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractTrajectories_Contracts_ContractId",
                table: "ContractTrajectories",
                column: "ContractId",
                principalTable: "Contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
