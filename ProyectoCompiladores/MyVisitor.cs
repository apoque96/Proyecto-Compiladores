using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;
using ProyectoCompiladores;

public class MyVisitor : GramaticaBaseVisitor<VariableSegment>
{

    private VariableSegment interpreterProgram;
    public string programa;
    public string nombre_programa;

    public MyVisitor(string programa, string nombre_programa)
    {
        interpreterProgram = new VariableSegment();
        this.programa = programa;
        this.nombre_programa = nombre_programa;
    }

    private void visitChildren(ParserRuleContext context)
    {
        if (context?.children == null) return;

        foreach (var child in context.children)
        {
            Visit(child);
        }
    }



    public override VariableSegment VisitProg(GramaticaParser.ProgContext context)
    {
        Console.WriteLine("Visitando programa principal...");
        var progNode = new AST.PgNode();

        visitChildren(context);

        return interpreterProgram; // Visita hijos recursivamente
    }

    public override VariableSegment VisitPg(GramaticaParser.PgContext context)
    {
        Console.WriteLine("Visitando Pg: " + context.GetText());

        visitChildren(context);

        var pgNode = new AST.PgNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitSl(GramaticaParser.SlContext context)
    {
        Console.WriteLine("Visitando Sl: " + context.GetText());

        visitChildren(context);

        var slNode = new AST.SlNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitS(GramaticaParser.SContext context)
    {
        Console.WriteLine("Visitando S: " + context.GetText());

        visitChildren(context);

        var sNode = new AST.SNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitD(GramaticaParser.DContext context)
    {
        Console.WriteLine("Visitando D: " + context.GetText());

        // Si tu regla D es algo como: D : '$int' ID '=' INT ;
        var tipo = context.GetChild(0).GetText(); // o context.INT().GetText() si existe ese método
        var nombre = context.ID().GetText();

        Console.WriteLine($"Declaración de variable: tipo={tipo}, nombre={nombre}");

        switch (tipo)
        {
            case "int":
                programa += $"\npublic static int {nombre} = (int)(";
                Visit(context.e());
                programa += ");";
                break;

            case "float":
                programa += $"\npublic static float {nombre} = ";
                Visit(context.e());
                programa += ";";
                break;

            case "string":
                string valor = context.STRING().GetText();
                programa += $"\npublic static string {nombre} = {valor}";
                break;
            case "bool":
                programa += $"\npublic static bool {nombre} = ";
                Visit(context.ds());
                programa += ";";
                break;
        }

        return interpreterProgram;
    }


    public override VariableSegment VisitE(GramaticaParser.EContext context)
    {
        Console.WriteLine("Visitando E: " + context.GetText());



        
        return interpreterProgram;
    }

    public override VariableSegment VisitEp(GramaticaParser.EpContext context)
    {
        Console.WriteLine("Visitando Ep: " + context.GetText());
        var epNode = new AST.EpNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitT(GramaticaParser.TContext context)
    {
        Console.WriteLine("Visitando T: " + context.GetText());
        var tNode = new AST.TNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitTp(GramaticaParser.TpContext context)
    {
        Console.WriteLine("Visitando Tp: " + context.GetText());
        var tpNode = new AST.TpNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitF(GramaticaParser.FContext context)
    {
        Console.WriteLine("Visitando F: " + context.GetText());
        var fNode = new AST.FNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitDs(GramaticaParser.DsContext context)
    {
        Console.WriteLine("Visitando Ds: " + context.GetText());
        var dsNode = new AST.DsNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitDsp(GramaticaParser.DspContext context)
    {
        Console.WriteLine("Visitando Dsp: " + context.GetText());
        var dspNode = new AST.DspNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitIo(GramaticaParser.IoContext context)
    {
        Console.WriteLine("Visitando Io: " + context.GetText());
        var ioNode = new AST.IoNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitCe(GramaticaParser.CeContext context)
    {
        Console.WriteLine("Visitando Ce: " + context.GetText());
        var ceNode = new AST.CeNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitIfd(GramaticaParser.IfdContext context)
    {
        Console.WriteLine("Visitando Ifd: " + context.GetText());
        var ifdNode = new AST.IfdNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitEd(GramaticaParser.EdContext context)
    {
        Console.WriteLine("Visitando Ed: " + context.GetText());
        var edNode = new AST.EdNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitEdp(GramaticaParser.EdpContext context)
    {
        Console.WriteLine("Visitando Edp: " + context.GetText());
        var edpNode = new AST.EdpNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitFd(GramaticaParser.FdContext context)
    {
        Console.WriteLine("Visitando Fd: " + context.GetText());
        var fdNode = new AST.FdNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitRt(GramaticaParser.RtContext context)
    {
        Console.WriteLine("Visitando Rt: " + context.GetText());
        var rtNode = new AST.RtNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitPl(GramaticaParser.PlContext context)
    {
        Console.WriteLine("Visitando Pl: " + context.GetText());
        var plNode = new AST.PlNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitPlp(GramaticaParser.PlpContext context)
    {
        Console.WriteLine("Visitando Plp: " + context.GetText());
        var plpNode = new AST.PlpNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitTy(GramaticaParser.TyContext context)
    {
        Console.WriteLine("Visitando Ty: " + context.GetText());
        var tyNode = new AST.TyNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitTyp(GramaticaParser.TypContext context)
    {
        Console.WriteLine("Visitando Typ: " + context.GetText());
        var typNode = new AST.TypNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitFc(GramaticaParser.FcContext context)
    {
        Console.WriteLine("Visitando Fc: " + context.GetText());
        var fcNode = new AST.FcNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitP(GramaticaParser.PContext context)
    {
        Console.WriteLine("Visitando P: " + context.GetText());
        var pNode = new AST.PNode();
        return interpreterProgram;
    }

    public override VariableSegment VisitPp(GramaticaParser.PpContext context)
    {
        Console.WriteLine("Visitando Pp: " + context.GetText());
        var ppNode = new AST.PpNode();
        return interpreterProgram;
    }
}

   