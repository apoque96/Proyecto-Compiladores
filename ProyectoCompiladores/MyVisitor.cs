using System;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime;
using ProyectoCompiladores;

public class MyVisitor : GramaticaBaseVisitor<VariableSegment>
{

    private VariableSegment interpreterProgram;
    public string programa;
    public string nombre_programa;
    public string directorio;

    private string tempString;

    public MyVisitor(string programa, string nombre_programa, string directorio)
    {
        interpreterProgram = new VariableSegment();
        this.programa = programa;
        this.nombre_programa = nombre_programa;
        this.directorio = directorio;
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

        programa += $"using System;\n\npublic class {
            Path.GetFileNameWithoutExtension(nombre_programa).Replace(" ", "_")}\n{{";

        try
        {
            visitChildren(context);

            programa += "\n}";

            string nombre = Path.GetFileNameWithoutExtension(nombre_programa);

            // Borrar contenido original del archivo
            if (File.Exists(directorio + "\\" + nombre))
            {
                File.Delete(directorio + "\\" + nombre);
            }

            // Quita la extensión al archivo y guarda el archivo
            File.WriteAllText(
            Path.Combine(Path.GetDirectoryName(directorio + "\\" + nombre) ?? 
            "", 
            Path.GetFileNameWithoutExtension(directorio + "\\" + nombre)) + ".cs", programa);

            return interpreterProgram;
        }
        catch (Exception e)
        {
            Console.WriteLine($"{e.Message}");
            programa = "";
            return null;
        }
    }

    public override VariableSegment VisitPg(GramaticaParser.PgContext context)
    {
        Console.WriteLine("Visitando Pg: " + context.GetText());

        Visit(context.fd());

        programa += "\npublic static void Main(string[] args)\n{";

        Visit(context.sl());

        programa += "\n}";

        return interpreterProgram;
    }

    public override VariableSegment VisitSl(GramaticaParser.SlContext context)
    {
        Console.WriteLine("Visitando Sl: " + context.GetText());

        visitChildren(context);


        return interpreterProgram;
    }

    public override VariableSegment VisitS(GramaticaParser.SContext context)
    {
        Console.WriteLine("Visitando S: " + context.GetText());

        visitChildren(context);

        return interpreterProgram;
    }

    public override VariableSegment VisitD(GramaticaParser.DContext context)
    {
        Console.WriteLine("Visitando D: " + context.GetText());

        // Si tu regla D es algo como: D : '$int' ID '=' INT ;
        var tipo = context.GetChild(0).GetText(); // o context.INT().GetText() si existe ese método
        var nombre = context.ID().GetText();

        // Agregar la variable al diccionario
        var variables = interpreterProgram.variables;

        Console.WriteLine($"Declaración de variable: tipo={tipo}, nombre={nombre}");

        switch (tipo)
        {
            case "int":
                // Revisa si la variable ya existe
                if (variables["int"].ContainsKey(nombre))
                    programa += $"\n{nombre} = (int)(";
                else if (
                    variables["float"].ContainsKey(nombre) ||
                    variables["string"].ContainsKey(nombre) ||
                    variables["bool"].ContainsKey(nombre))
                    throw new Exception($"Error: La variable {nombre} ya existe con un tipo diferente.");
                else
                {
                    variables["int"][nombre] = new Variable(nombre, Type.INT);
                    programa += $"\nint {nombre} = (int)(";
                }
                Visit(context.e());
                programa += ");";
                break;

            case "float":
                // Revisa si la variable ya existe
                if (variables["float"].ContainsKey(nombre))
                    programa += $"\n{nombre} = (float)(";
                else if (
                    variables["int"].ContainsKey(nombre) ||
                    variables["string"].ContainsKey(nombre) ||
                    variables["bool"].ContainsKey(nombre))
                    throw new Exception($"Error: La variable {nombre} ya existe con un tipo diferente.");
                else
                {
                    variables["float"][nombre] = new Variable(nombre, Type.FLOAT);
                    programa += $"\nfloat {nombre} = (float)(";
                }
                Visit(context.e());
                programa += ");";
                break;

            case "string":
                // Revisa si la variable ya existe
                string valor = context.STRING().GetText();
                if (variables["string"].ContainsKey(nombre))
                    programa += $"\n{nombre} = {valor};";
                else if (
                    variables["int"].ContainsKey(nombre) ||
                    variables["float"].ContainsKey(nombre) ||
                    variables["bool"].ContainsKey(nombre))
                    throw new Exception($"Error: La variable {nombre} ya existe con un tipo diferente.");
                else
                {
                    variables["string"][nombre] = new Variable(nombre, Type.STRING);
                    programa += $"\nstring {nombre} = {valor};";
                }
                break;
            case "bool":
                // Revisa si la variable ya existe
                if (variables["bool"].ContainsKey(nombre))
                    programa += $"\n{nombre} = ";
                else if (
                    variables["int"].ContainsKey(nombre) ||
                    variables["float"].ContainsKey(nombre) ||
                    variables["string"].ContainsKey(nombre))
                    throw new Exception($"Error: La variable {nombre} ya existe con un tipo diferente.");
                else
                {
                    variables["bool"][nombre] = new Variable(nombre, Type.BOOL);
                    programa += $"\nbool {nombre} = ";
                }
                Visit(context.ds());
                programa += ";";
                break;
        }

        interpreterProgram.variables = variables;

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
            programa += context.FLOAT().GetText().Substring(1) + "f";
        }
        else if (context.ID() != null)
        {
            //Verifica que la variable existe
            var id = context.ID().GetText();
            var variables = interpreterProgram.variables;

            if (!variables["int"].ContainsKey(id) && !variables["float"].ContainsKey(id))
                throw new Exception($"Error: no se encontro la variable {id} o es del tipo incorrecto");

            programa += context.ID().GetText();
        }
        else
            throw new Exception($"Error: carácter inesperado {context.GetText()}");

        return interpreterProgram;
    }

    public override VariableSegment VisitDs(GramaticaParser.DsContext context)
    {
        Console.WriteLine("Visitando Ds: " + context.GetText());

        if (context.BOOL() != null)
        {
            programa += context.BOOL().GetText();
        }
        else if (context.ID() != null)
        {
            if (context.GetChild(1) != null)
                programa += "!";
            // Revisa si la variable existe
            var variables = interpreterProgram.variables;

            var id = context.ID().GetText();

            if (!variables["bool"].ContainsKey(id))
                throw new Exception($"Error: no se encontro la variable {id} o es del tipo incorrecto");

            programa += id;
        }
        else if (context.e(0) != null)
        {
            Visit(context.e(0));
            programa += " " + context.dsp().GetText() + " ";
            Visit(context.e(1));
        }
        else if (context.STRING() != null && context.GetText().Contains("=="))
        {
            programa += context.STRING(0).GetText() + " == " + context.STRING(1).GetText();
        }
        else if (context.STRING() != null && context.GetText().Contains("!="))
        {
            programa += context.STRING(0).GetText() + " != " + context.STRING(1).GetText();
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
            var variables = interpreterProgram.variables;

            if (variables["int"].ContainsKey(id))
                programa += $"\n{id} = Convert.ToInt32(Console.ReadLine());";
            else if (variables["float"].ContainsKey(id))
                programa += $"\n{id} = float.Parse(Console.ReadLine());";
            else if (variables["bool"].ContainsKey(id))
                programa += $"\n{id} = Convert.ToBoolean(Console.ReadLine());";
            else if (variables["string"].ContainsKey(id))
                programa += $"\n{id} = Console.ReadLine();";
            else
                throw new Exception($"Error: no se pudo encontrar la variable {id}");
        }
        else if (context.GetText().StartsWith("Write"))
        {
            var id = context.ID().GetText();
            var variables = interpreterProgram.variables;

            if (!variables["int"].ContainsKey(id) && !variables["float"].ContainsKey(id)
                && !variables["bool"].ContainsKey(id) && !variables["string"].ContainsKey(id))
                    throw new Exception($"Error: no se pudo encontrar la variable {id}");

            programa += $"\nConsole.WriteLine({id});";
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

        if (context.ifd() != null)
        {
            Visit(context.ifd()); // else if
        }
        else
        {
            programa += "{\n";
            Visit(context.sl());
            programa += "\n}";
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitFd(GramaticaParser.FdContext context)
    {
        Console.WriteLine("Visitando Fd: " + context.GetText());

        if (context.ID() != null)
        {
            string nombre = context.ID().GetText();
            programa += $"\npublic static ";

            // vaciar la variable (utilizada para la declaración de variables)
            tempString = "";

            Visit(context.pl());

            Visit(context.rt()); // tipo de retorno y cuerpo

            programa = programa.Replace("$patata", nombre); // reemplazar si es necesario

            // Limpiar las variables
            var variables = interpreterProgram.variables;

            variables["int"] = new Dictionary<string, Variable>();
            variables["float"] = new Dictionary<string, Variable>();
            variables["string"] = new Dictionary<string, Variable>();
            variables["bool"] = new Dictionary<string, Variable>();

            Visit(context.fd()); // siguiente función (por recursión)
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitRt(GramaticaParser.RtContext context)
    {
        Console.WriteLine("Visitando Rt: " + context.GetText());

        if (context.ty() != null) // tiene retorno
        {
            string tipo = context.ty().GetText();
            programa += $"{tipo} $patata(";

            // agregar al programa las variables declaradas
            programa += tempString;

            programa += ")\n{\n";
            Visit(context.sl());
            programa += "\nreturn ";

            // Revisar que el tipo de retorno sea el mismo
            var typ = context.typ();
            if (typ.ID() != null)
            {
                // Revisar que la variable de retorno exista
                var variables = interpreterProgram.variables;
                if (!variables["int"].ContainsKey(typ.ID().GetText()) && tipo == "int")
                    throw new Exception($"Error: la variable {typ.ID().GetText()} " +
                        $"no existe o es de un tipo distinto al de la función");
                else if (!variables["float"].ContainsKey(typ.ID().GetText()) && tipo == "float")
                    throw new Exception($"Error: la variable {typ.ID().GetText()} " +
                        $"no existe o es de un tipo distinto al de la función");
                else if (!variables["string"].ContainsKey(typ.ID().GetText()) && tipo == "string")
                    throw new Exception($"Error: la variable {typ.ID().GetText()} " +
                        $"no existe o es de un tipo distinto al de la función");
                else if (!variables["bool"].ContainsKey(typ.ID().GetText()) && tipo == "bool")
                    throw new Exception($"Error: la variable {typ.ID().GetText()} " +
                        $"no existe o es de un tipo distinto al de la función");
            }
            else if (tipo == "int" && typ.INT() == null)
                throw new Exception("Error: el tipo de dato de " +
                    "retorno no coincide con el de la funcion");
            else if (tipo == "float" && typ.FLOAT() == null)
                throw new Exception("Error: el tipo de dato de " +
                    "retorno no coincide con el de la funcion");
            else if (tipo == "string" && typ.STRING() == null)
                throw new Exception("Error: el tipo de dato de " +
                    "retorno no coincide con el de la funcion");
            else if (tipo == "bool" && typ.BOOL() == null)
                throw new Exception("Error: el tipo de dato de " +
                    "retorno no coincide con el de la funcion");


            Visit(context.typ());
            programa += ";\n}";
        }
        else // sin retorno explícito (void)
        {
            programa += $"void $patata(";

            // agregar al programa las variables declaradas
            programa += tempString;

            programa += ")\n{\n";
            Visit(context.sl());
            programa += "\n}";
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitPl(GramaticaParser.PlContext context)
    {
        Console.WriteLine("Visitando Pl: " + context.GetText());

        if (context.ty() != null && context.ID() != null)
        {
            string tipo = context.ty().GetText();
            string nombre = context.ID().GetText();

            // Agregar la variable al diccionario
            var variables = interpreterProgram.variables;

            Console.WriteLine($"Declaración de variable: tipo={tipo}, nombre={nombre}");

            switch (tipo)
            {
                case "int":
                    variables["int"][nombre] = new Variable(nombre, Type.INT);
                    tempString += $"int {nombre}";
                    break;

                case "float":
                    variables["float"][nombre] = new Variable(nombre, Type.FLOAT);
                    tempString += $"float {nombre}";
                    break;

                case "string":
                    variables["string"][nombre] = new Variable(nombre, Type.STRING);
                    tempString += $"string {nombre}";
                    break;
                case "bool":
                    variables["bool"][nombre] = new Variable(nombre, Type.BOOL);
                    tempString += $"bool {nombre}";
                    break;
            }
            interpreterProgram.variables = variables;

            Visit(context.plp());
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitPlp(GramaticaParser.PlpContext context)
    {
        Console.WriteLine("Visitando Plp: " + context.GetText());

        if (context.ty() != null && context.ID() != null)
        {
            string tipo = context.ty().GetText();
            string nombre = context.ID().GetText();

            // Agregar la variable al diccionario
            var variables = interpreterProgram.variables;

            Console.WriteLine($"Declaración de variable: tipo={tipo}, nombre={nombre}");

            switch (tipo)
            {
                case "int":
                    variables["int"][nombre] = new Variable(nombre, Type.INT);
                    tempString += $", int {nombre}";
                    break;

                case "float":
                    variables["float"][nombre] = new Variable(nombre, Type.FLOAT);
                    tempString += $", float {nombre}";
                    break;

                case "string":
                    variables["string"][nombre] = new Variable(nombre, Type.STRING);
                    tempString += $", string {nombre}";
                    break;
                case "bool":
                    variables["bool"][nombre] = new Variable(nombre, Type.BOOL);
                    tempString += $", bool {nombre}";
                    break;
            }
            interpreterProgram.variables = variables;
            Visit(context.plp());
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitTy(GramaticaParser.TyContext context)
    {
        Console.WriteLine("Visitando Ty: " + context.GetText());

        programa += context.GetText();

        return interpreterProgram;
    }

    public override VariableSegment VisitTyp(GramaticaParser.TypContext context)
    {
        Console.WriteLine("Visitando Typ: " + context.GetText());


        if (context.INT() != null)
        {
            programa += context.INT().GetText();
        }
        else if (context.FLOAT() != null)
        {
            programa += context.FLOAT().GetText();
        }
        else if (context.STRING() != null)
        {
            programa += context.STRING().GetText();
        }
        else if (context.BOOL() != null)
        {
            programa += context.BOOL().GetText();
        }
        else if (context.ID() != null)
        {
            programa += context.ID().GetText();
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitFc(GramaticaParser.FcContext context)
    {
        Console.WriteLine("Visitando Fc: " + context.GetText());


        programa += $"\n{context.ID().GetText()} (";
        Visit(context.p());
        programa += $");";

        return interpreterProgram;
    }

    public override VariableSegment VisitP(GramaticaParser.PContext context)
    {
        Console.WriteLine("Visitando P: " + context.GetText());

        if (context.typ() != null)
        {
            Visit(context.typ());
            Visit(context.pp());
        }

        return interpreterProgram;
    }

    public override VariableSegment VisitPp(GramaticaParser.PpContext context)
    {
        Console.WriteLine("Visitando Pp: " + context.GetText());

        if (context.typ() != null)
        {
            programa += ", ";
            Visit(context.typ());
            Visit(context.pp());
        }

        return interpreterProgram;
    }
}