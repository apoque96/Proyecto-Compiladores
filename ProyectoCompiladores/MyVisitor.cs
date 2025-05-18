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
                programa += $"\npublic static string {nombre} = {valor};";
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

        Visit(context.t());
        Visit(context.ep());

        return interpreterProgram;
    }

    public override VariableSegment VisitEp(GramaticaParser.EpContext context)
    {
        Console.WriteLine("Visitando Ep: " + context.GetText());

        if (context.ChildCount == 0)
        {
            // Caso base: ep está vacío
            return interpreterProgram;
        }

        string operador = context.GetChild(0).GetText(); // '+' o '-'

        programa += " " + operador + " ";

        Visit(context.t());
        Visit(context.ep());

        return interpreterProgram;
    }

    public override VariableSegment VisitT(GramaticaParser.TContext context)
    {
        Console.WriteLine("Visitando T: " + context.GetText());

        Visit(context.f());
        Visit(context.tp());

        return interpreterProgram;
    }

    public override VariableSegment VisitTp(GramaticaParser.TpContext context)
    {
        Console.WriteLine("Visitando Tp: " + context.GetText());

        if (context.ChildCount == 0)
        {
            // Caso base: ep está vacío
            return interpreterProgram;
        }

        string operador = context.GetChild(0).GetText(); // '+' o '-'

        programa += " " + operador + " ";

        Visit(context.f());
        Visit(context.tp());

        return interpreterProgram;
    }

    public override VariableSegment VisitF(GramaticaParser.FContext context)
    {
        Console.WriteLine("Visitando F: " + context.GetText());

        if (context.e() != null)
        {
            programa += "(";
            Visit(context.e());
            programa += ")";
        }
        else if (context.INT() != null)
        {
            programa += context.INT().GetText();
        }
        else if (context.FLOAT() != null)
        {
            programa += context.FLOAT().GetText();
        }
        else if (context.ID() != null)
        {
            programa += context.ID().GetText();
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitDs(GramaticaParser.DsContext context)
    {
        Console.WriteLine("Visitando Ds: " + context.GetText());

        if (context.BOOL() != null)
        {
            programa += context.BOOL().GetText();
        }
        else if (context.STRING() != null && context.GetText().Contains("=="))
        {
            programa += context.STRING(0).GetText() + " == " + context.STRING(1).GetText();
        }
        else if (context.STRING() != null && context.GetText().Contains("!="))
        {
            programa += context.STRING(0).GetText() + " != " + context.STRING(1).GetText();
        }
        else
        {
            Visit(context.e(0));
            programa += " " + context.dsp().GetText() + " ";
            Visit(context.e(1));
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitDsp(GramaticaParser.DspContext context)
    {
        Console.WriteLine("Visitando Dsp: " + context.GetText());
        programa += context.GetText(); // ya es un operador como >, <, etc.
        return interpreterProgram;
    }

    public override VariableSegment VisitIo(GramaticaParser.IoContext context)
    {
        Console.WriteLine("Visitando Io: " + context.GetText());

        if (context.GetText().StartsWith("Read"))
        {
            var id = context.ID().GetText();
            programa += $"\n{id} = Convert.ToInt32(Console.ReadLine());";
        }
        else if (context.GetText().StartsWith("Write"))
        {
            var texto = context.STRING().GetText();
            programa += $"\nConsole.WriteLine({texto});";
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitCe(GramaticaParser.CeContext context)
    {
        Console.WriteLine("Visitando Ce: " + context.GetText());
        if (context.ifd() != null)
        {
            Visit(context.ifd());
        }
        else // while
        {
            programa += $"\nwhile(";
            Visit(context.ds());
            programa += ")\n{";
            Visit(context.sl());
            programa += "\n}";
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitIfd(GramaticaParser.IfdContext context)
    {
        Console.WriteLine("Visitando Ifd: " + context.GetText());
        programa += $"\nif (";
        Visit(context.ds());
        programa += ")\n{";
        Visit(context.sl());
        programa += "\n}";

        if (context.ed() != null)
            Visit(context.ed());

        return interpreterProgram;
    }

    public override VariableSegment VisitEd(GramaticaParser.EdContext context)
    {
        Console.WriteLine("Visitando Ed: " + context.GetText());

        if (context.edp() == null)
            return interpreterProgram;

        programa += $"\nelse";
        Visit(context.edp());
        
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

   