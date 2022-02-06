using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EnterpriseAssistant.DataAccess.Sql;

public static class SqlMigrationHelper
{
    public static void RunSqlFromAssembly(this MigrationBuilder builder,
        Assembly assembly, SqlRoutines sqlRoutines)
    {
        var scripts = GetSqlFromAssembly(assembly, sqlRoutines);
        foreach (var sql in scripts)
        {
            builder.Sql(sql);
        }
    }

    public static IEnumerable<string> GetSqlFromAssembly(Assembly assembly, SqlRoutines sqlRoutines)
    {
        var scripts = new List<string>();
        var names = assembly.GetManifestResourceNames();
        var sqlFileNames = names
            .Where(n => 
                NameOfType(n, sqlRoutines));

        foreach (var name in sqlFileNames)
        {
            using var sr =
                new StreamReader(assembly.GetManifestResourceStream(name) ?? throw new InvalidOperationException());
            scripts.Add(sr.ReadToEnd());
        }

        return scripts;
    }

    private static bool NameOfType(string name, SqlRoutines sqlRoutines)
    {
        if (name.EndsWith(".sql") == false)
        {
            return false;
        }

        if ((sqlRoutines & SqlRoutines.Function) != 0 && name.Contains("function_"))
        {
            return true;
        }
        if ((sqlRoutines & SqlRoutines.Procedure) != 0 && name.Contains("procedure_"))
        {
            return true;
        }

        return false;
    }
}