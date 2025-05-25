using ProyectoCompiladores.lexer;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.IO;

public class Compiler
{
    public static void Main(string[] args)
    {
        try
        {
            string path;
            string destinationPath;
            if (args.Length != 0)
            {
                path = args[0];
            }
            else
            {
                Console.WriteLine("Ingrese la ruta al archivo:");
                path = Console.ReadLine();
            }

            if (args.Length == 2)
            {
                destinationPath = args[1];
            }
            else
            {
                Console.WriteLine("Ingrese la ruta donde desea guardar el archivo:");
                destinationPath = Console.ReadLine();
            }

            // Leer el contenido del archivo fuente
            using StreamReader sr = new(path);
            string input = sr.ReadToEnd();

            // Tokenizar usando tu lexer personalizado
            ProyectoCompiladores.lexer.Lexer lexer = new(input);
            var tokens = lexer.Tokenize();

            Console.WriteLine("\n--- Tokens personalizados ---");
            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }

            // Análisis sintáctico con ANTLR
            Console.WriteLine("\n--- Árbol de análisis con ANTLR ---");

            AntlrInputStream antlrInput = new AntlrInputStream(input);
            GramaticaLexer antlrLexer = new GramaticaLexer(antlrInput);
            CommonTokenStream tokenStream = new CommonTokenStream(antlrLexer);
            GramaticaParser parser = new GramaticaParser(tokenStream);

            // Usamos la regla inicial (por ejemplo, prog)
            var tree = parser.prog();
            
            // Terminar si hay error sintactico
            if (parser.NumberOfSyntaxErrors > 0)
            {
                return;
            }

            // Crear instancia de tu Visitor
            MyVisitor visitor = new MyVisitor("",
                path, destinationPath);
            visitor.Visit(tree);

            Console.WriteLine(visitor.programa);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: " + e.Message);
        }
    }
}
