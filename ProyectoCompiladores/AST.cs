using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoCompiladores
{
    internal class AST
    {
        public class PgNode { public void Accept() => Console.WriteLine("Visitando PgNode"); }
        public class SlNode { public void Accept() => Console.WriteLine("Visitando SlNode"); }
        public class SNode { public void Accept() => Console.WriteLine("Visitando SNode"); }
        public class DNode { public void Accept() => Console.WriteLine("Visitando DNode"); }
        public class ENode { public void Accept() => Console.WriteLine("Visitando ENode"); }
        public class EpNode { public void Accept() => Console.WriteLine("Visitando EpNode"); }
        public class TNode { public void Accept() => Console.WriteLine("Visitando TNode"); }
        public class TpNode { public void Accept() => Console.WriteLine("Visitando TpNode"); }
        public class FNode { public void Accept() => Console.WriteLine("Visitando FNode"); }
        public class DsNode { public void Accept() => Console.WriteLine("Visitando DsNode"); }
        public class DspNode { public void Accept() => Console.WriteLine("Visitando DspNode"); }
        public class IoNode { public void Accept() => Console.WriteLine("Visitando IoNode"); }
        public class CeNode { public void Accept() => Console.WriteLine("Visitando CeNode"); }
        public class IfdNode { public void Accept() => Console.WriteLine("Visitando IfdNode"); }
        public class EdNode { public void Accept() => Console.WriteLine("Visitando EdNode"); }
        public class EdpNode { public void Accept() => Console.WriteLine("Visitando EdpNode"); }
        public class FdNode { public void Accept() => Console.WriteLine("Visitando FdNode"); }
        public class RtNode { public void Accept() => Console.WriteLine("Visitando RtNode"); }
        public class PlNode { public void Accept() => Console.WriteLine("Visitando PlNode"); }
        public class PlpNode { public void Accept() => Console.WriteLine("Visitando PlpNode"); }
        public class TyNode { public void Accept() => Console.WriteLine("Visitando TyNode"); }
        public class TypNode { public void Accept() => Console.WriteLine("Visitando TypNode"); }
        public class FcNode { public void Accept() => Console.WriteLine("Visitando FcNode"); }
        public class PNode { public void Accept() => Console.WriteLine("Visitando PNode"); }
        public class PpNode { public void Accept() => Console.WriteLine("Visitando PpNode"); }

    }
}
