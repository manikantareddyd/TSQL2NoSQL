using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AntlrCSharp
{
    public class TSqlParserVisitor: TSqlParserBaseVisitor<object>
    {
        public string noSql = "";
        public override object VisitQuery_expression(TSqlParser.Query_expressionContext context)
        {
            var root = context.query_specification();
            var A = root.select_list().select_list_elem();
            var B = root.table_sources();
            var outCols = new JObject();
            var whereClause = new JObject();
            foreach(var c in A)
            {
                var p = c.GetText();
                outCols.Add(p, 1);
                if (p.Equals("*"))
                {
                    noSql = B.GetText() + "find()";
                    return 1;
                }
            }

            noSql = B.GetText() + ".find(" +
                JsonConvert.SerializeObject(whereClause) +
                "," +
                JsonConvert.SerializeObject(outCols) +
                ")";
            return 1;
        }
    }
}
