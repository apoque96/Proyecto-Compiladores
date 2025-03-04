using ProyectoCompiladores.lexer;

public class Compiler
{
    public static void Main(string[] args)
    {
        try
        {
            string path;
            if (args.Length == 1)
            {
                path = args[0];   
            }
            else
            {
                Console.WriteLine("Ingrese la ruta al archivo(ruta relativa)");
                path = Console.ReadLine();
            }

            using StreamReader sr = new(path);
            string input = sr.ReadToEnd();
            Lexer lexer = new(input);
            var tokens = lexer.Tokenize();

            foreach (var token in tokens)
            {
                Console.WriteLine(token);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}