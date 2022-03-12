using FluentMigrator;

namespace Todolist.DbMigration.Migrations;

[Migration(202203121115)]
public class InitTodoTables : Migration
{
    public override void Up()
    {
        Execute.Script("InstallExtension.sql");

        Create.Table("todo")
            .WithColumn("id").AsGuid().PrimaryKey().NotNullable().WithDefaultValue(SystemMethods.NewGuid)
            .WithColumn("title").AsString().Unique().NotNullable()
            .WithColumn("description").AsString().Unique().NotNullable()
            .WithColumn("created_at").AsCustom("timestamp").NotNullable()
            .WithColumn("updated_at").AsCustom("timestamp").NotNullable();

        Execute.Script("GrantAccess.sql");
    }

    public override void Down()
    {
        Delete.Table("todo");
    }
}