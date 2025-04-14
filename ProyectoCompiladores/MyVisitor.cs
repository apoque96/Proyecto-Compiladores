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

    public override object VisitEd(GramaticaParser.EdContext context)
    {
        Console.WriteLine("Visitando Ed: " + context.GetText());
        var edNode = new AST.EdNode();
        return base.VisitEd(context);
    }

    public override object VisitEdp(GramaticaParser.EdpContext context)
    {
        Console.WriteLine("Visitando Edp: " + context.GetText());
        var edpNode = new AST.EdpNode();
        return base.VisitEdp(context);
    }

    public override object VisitFd(GramaticaParser.FdContext context)
    {
        Console.WriteLine("Visitando Fd: " + context.GetText());
        var fdNode = new AST.FdNode();
        return base.VisitFd(context);
    }

    public override object VisitRt(GramaticaParser.RtContext context)
    {
        Console.WriteLine("Visitando Rt: " + context.GetText());
        var rtNode = new AST.RtNode();
        return base.VisitRt(context);
    }

    public override object VisitPl(GramaticaParser.PlContext context)
    {
        Console.WriteLine("Visitando Pl: " + context.GetText());
        var plNode = new AST.PlNode();
        return base.VisitPl(context);
    }

    public override object VisitPlp(GramaticaParser.PlpContext context)
    {
        Console.WriteLine("Visitando Plp: " + context.GetText());
        var plpNode = new AST.PlpNode();
        return base.VisitPlp(context);
    }

    public override object VisitTy(GramaticaParser.TyContext context)
    {
        Console.WriteLine("Visitando Ty: " + context.GetText());
        var tyNode = new AST.TyNode();
        return base.VisitTy(context);
    }

    public override object VisitTyp(GramaticaParser.TypContext context)
    {
        Console.WriteLine("Visitando Typ: " + context.GetText());
        var typNode = new AST.TypNode();
        return base.VisitTyp(context);
    }

    public override object VisitFc(GramaticaParser.FcContext context)
    {
        Console.WriteLine("Visitando Fc: " + context.GetText());
        var fcNode = new AST.FcNode();
        return base.VisitFc(context);
    }

    public override object VisitP(GramaticaParser.PContext context)
    {
        Console.WriteLine("Visitando P: " + context.GetText());
        var pNode = new AST.PNode();
        return base.VisitP(context);
    }

    public override object VisitPp(GramaticaParser.PpContext context)
    {
        Console.WriteLine("Visitando Pp: " + context.GetText());
        var ppNode = new AST.PpNode();
        return base.VisitPp(context);
    }
}

   