using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HRMS.Migrations
{
    /// <inheritdoc />
    public partial class AddRelAndChangeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
                //// حذف Foreign Key من Employees
                //migrationBuilder.DropForeignKey(
                //    name: "FK_Employees_Departments_DepartmentId",
                //    table: "Employees");

                //// حذف Primary Key من Departments
                //migrationBuilder.DropPrimaryKey(
                //    name: "PK_Departments",
                //    table: "Departments");

                //migrationBuilder.DropForeignKey(
                //name: "FK_Employees_Departments_DepartmentId",
                //table: "Employees");


                migrationBuilder.AlterColumn<long>(
                    name: "ManagerId",
                    table: "Employees",
                    type: "bigint",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<long>(
                    name: "DepartmentId",
                    table: "Employees",
                    type: "bigint",
                    nullable: true,
                    oldClrType: typeof(int),
                    oldType: "int");

                migrationBuilder.AlterColumn<long>(
                    name: "Id",
                    table: "Departments",
                    type: "bigint",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int")
                    .Annotation("SqlServer:Identity", "1, 1")
                    .OldAnnotation("SqlServer:Identity", "1, 1");

                migrationBuilder.CreateIndex(
                    name: "IX_Employees_ManagerId",
                    table: "Employees",
                    column: "ManagerId");
            // إعادة إنشاء Primary Key
            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                    name: "FK_Employees_Departments_DepartmentId",
                    table: "Employees",
                    column: "DepartmentId",
                    principalTable: "Departments",
                    principalColumn: "Id");

                migrationBuilder.AddForeignKey(
                    name: "FK_Employees_Employees_ManagerId",
                    table: "Employees",
                    column: "ManagerId",
                    principalTable: "Employees",
                    principalColumn: "Id");
           
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ManagerId",
                table: "Employees");

            migrationBuilder.AlterColumn<int>(
                name: "ManagerId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Departments",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
