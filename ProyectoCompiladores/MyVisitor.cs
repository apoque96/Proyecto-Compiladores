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

    public override object VisitE(GramaticaParser.EContext context)
    {
        Console.WriteLine("Visitando E: " + context.GetText());
        var eNode = new AST.ENode();
        return base.VisitE(context);
    }

    public override object VisitEp(GramaticaParser.EpContext context)
    {
        Console.WriteLine("Visitando Ep: " + context.GetText());
        var epNode = new AST.EpNode();
        return base.VisitEp(context);
    }

    public override object VisitT(GramaticaParser.TContext context)
    {
        Console.WriteLine("Visitando T: " + context.GetText());
        var tNode = new AST.TNode();
        return base.VisitT(context);
    }

    public override object VisitTp(GramaticaParser.TpContext context)
    {
        Console.WriteLine("Visitando Tp: " + context.GetText());
        var tpNode = new AST.TpNode();
        return base.VisitTp(context);
    }

    public override object VisitF(GramaticaParser.FContext context)
    {
        Console.WriteLine("Visitando F: " + context.GetText());
        var fNode = new AST.FNode();
        return base.VisitF(context);
    }

    public override object VisitDs(GramaticaParser.DsContext context)
    {
        Console.WriteLine("Visitando Ds: " + context.GetText());
        var dsNode = new AST.DsNode();
        return base.VisitDs(context);
    }

    public override object VisitDsp(GramaticaParser.DspContext context)
    {
        Console.WriteLine("Visitando Dsp: " + context.GetText());
        var dspNode = new AST.DspNode();
        return base.VisitDsp(context);
    }

    public override object VisitIo(GramaticaParser.IoContext context)
    {
        Console.WriteLine("Visitando Io: " + context.GetText());
        var ioNode = new AST.IoNode();
        return base.VisitIo(context);
    }

    public override object VisitCe(GramaticaParser.CeContext context)
    {
        Console.WriteLine("Visitando Ce: " + context.GetText());
        var ceNode = new AST.CeNode();
        return base.VisitCe(context);
    }

    public override object VisitIfd(GramaticaParser.IfdContext context)
    {
        Console.WriteLine("Visitando Ifd: " + context.GetText());
        var ifdNode = new AST.IfdNode();
        return base.VisitIfd(context);
    }
}

   