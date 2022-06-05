using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace EnterpriseAssistant.Identity.DataAccess.Helpers
{
    public class SqlHelper
    {
        public static string ReadSql(string sqlName, Assembly assembly = null)
        {
            assembly ??= Assembly.GetAssembly(typeof(SqlHelper));
            var names = assembly.GetManifestResourceNames();
            var sqlFileName = names.First(n => n.EndsWith($"{sqlName}.sql"));

            using var sr = new StreamReader(assembly.GetManifestResourceStream(sqlFileName) ??
                                            throw new InvalidOperationException());
            return sr.ReadToEnd();
        }
    }
}