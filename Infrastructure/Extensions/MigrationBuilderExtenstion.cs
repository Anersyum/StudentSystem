using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.EntityFrameworkCore.Migrations.Operations.Builders;

namespace Infrastructure.Extensions;

public static class MigrationBuilderExtenstion
{
    public static OperationBuilder<SqlOperation> CreateStudentGroupAssignTrigger(this MigrationBuilder migrationBuilder)
    {
        var triggerSql = File.ReadAllText("SqlScripts/ValidateDepartmentGroups.sql");
        
        return migrationBuilder.Sql(triggerSql);
    }

    public static OperationBuilder<SqlOperation> CreateGroupAcademicAveragesView(this MigrationBuilder migrationBuilder)
    {
        var viewSql = File.ReadAllText("SqlScripts/GroupAveragesScript.sql");

        return migrationBuilder.Sql(viewSql);
    }
}
