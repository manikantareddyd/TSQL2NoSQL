using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;

namespace AntlrCSharp
{
    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                string input = "";
                StringBuilder text = new StringBuilder();
                Console.WriteLine("Input the chat.");
                
                // to type the EOF character and end the input: use CTRL+D, then press <enter>
                while ((input = Console.ReadLine()) != ";")
                {
                    text.AppendLine(input);
                }
                
                AntlrInputStream inputStream = new AntlrInputStream(text.ToString());
                var tSqlLexer = new TSqlLexer(inputStream);
                var tSqlCTS = new CommonTokenStream(tSqlLexer);
                var tSqlParser = new TSqlParser(tSqlCTS);

                var c = tSqlParser.tsql_file();
                var visitor = new TSqlParserVisitor();                
                visitor.Visit(c);
                Console.WriteLine(visitor.noSql);
                Console.ReadLine();
            }
            catch (Exception ex)
            {                
                Console.WriteLine("Error: " + ex);                
            }
        }
    }
}
