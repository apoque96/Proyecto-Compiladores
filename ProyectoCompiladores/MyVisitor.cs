using System;
using Antlr4.Runtime.Misc;
using ProyectoCompiladores;

public class MyVisitor : GramaticaBaseVisitor<object>
{
    public override object VisitProg(GramaticaParser.ProgContext context)
    {
        Console.WriteLine("Visitando programa principal...");
        var progNode = new AST.PgNode();
        return base.VisitProg(context); // Visita hijos recursivamente
    }

    public override object VisitPg(GramaticaParser.PgContext context)
    {
        Console.WriteLine("Visitando Pg: " + context.GetText()); 
        var pgNode = new AST.PgNode();  
        return base.VisitPg(context);
    }

    public override object VisitSl(GramaticaParser.SlContext context)
    {
        Console.WriteLine("Visitando Sl: " + context.GetText());
        var slNode = new AST.SlNode();
        return base.VisitSl(context);
    }

    public override object VisitS(GramaticaParser.SContext context)
    {
        Console.WriteLine("Visitando S: " + context.GetText());
        var sNode = new AST.SNode();
        return base.VisitS(context);
    }

    public override object VisitD(GramaticaParser.DContext context)
    {
        Console.WriteLine("Visitando D: " + context.GetText());
        var dNode = new AST.DNode();
        return base.VisitD(context);
    }


}