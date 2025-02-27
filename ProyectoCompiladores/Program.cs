using ProyectoCompiladores.lexer;

public class Compiler
{
    public static void Main(string[] args)
    {
        try
        {
            using StreamReader sr = new(args[0]);
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