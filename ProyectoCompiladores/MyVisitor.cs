using System;
using Antlr4.Runtime.Misc;

public class MiVisitor : GramaticaBaseVisitor<object>
{
    public override object VisitProg(GramaticaParser.ProgContext context)
    {
        Console.WriteLine("Visitando programa principal...");
        return base.VisitProg(context); // Visita hijos recursivamente
    }

    public override object VisitD(GramaticaParser.DContext context)
    {
        Console.WriteLine("Visitando declaración: " + context.GetText());
        return null;
    }

    public override object VisitIo(GramaticaParser.IoContext context)
    {
        Console.WriteLine("Visitando entrada/salida: " + context.GetText());
        return null;
    }
}