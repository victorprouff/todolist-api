using System.Data;
using FluentMigrator;

namespace Todolist.DbMigration.Migrations;

[Migration(202203121115)]
public class InitTodoTables : Migration
{
    public override void Up()
    {
        Execute.Script("InstallExtension.sql");

        Create.Table("todo_list")
            .WithColumn("id").AsGuid().PrimaryKey().NotNullable().WithDefaultValue(SystemMethods.NewGuid)
            .WithColumn("title").AsString().Unique().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("deadline_at").AsCustom("timestamp").Nullable()
            .WithColumn("started_at").AsCustom("timestamp").Nullable()
            .WithColumn("ended_at").AsCustom("timestamp").Nullable();

        Create.Table("task")
            .WithColumn("id").AsGuid().PrimaryKey().NotNullable().WithDefaultValue(SystemMethods.NewGuid)
            .WithColumn("todo_list_id").AsGuid().NotNullable().ForeignKey("fk_task_todo_list_todo_list_id", "todo_list", "id").OnDelete(Rule.Cascade)
            .WithColumn("title").AsString().NotNullable()
            .WithColumn("description").AsString().Nullable()
            .WithColumn("status").AsInt32().Unique().NotNullable()
            .WithColumn("started_at").AsCustom("timestamp").Nullable()
            .WithColumn("ended_at").AsCustom("timestamp").Nullable();
        
        Execute.Script("GrantAccess.sql");
    }

    public override void Down()
    {
        Delete.Table("todo_list");
        Delete.Table("task");
    }
}