using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CorePlatform.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseCpfAsPatientKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_CPF",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "Appointments");

            migrationBuilder.AddColumn<string>(
                name: "PatientCpf",
                table: "Appointments",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "CPF");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientCpf",
                table: "Appointments",
                column: "PatientCpf");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientCpf",
                table: "Appointments",
                column: "PatientCpf",
                principalTable: "Patients",
                principalColumn: "CPF",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_Patients_PatientCpf",
                table: "Appointments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patients",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_PatientCpf",
                table: "Appointments");

            migrationBuilder.DropColumn(
                name: "PatientCpf",
                table: "Appointments");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "Patients",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "PatientId",
                table: "Appointments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patients",
                table: "Patients",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CPF",
                table: "Patients",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PatientId",
                table: "Appointments",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_Patients_PatientId",
                table: "Appointments",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
