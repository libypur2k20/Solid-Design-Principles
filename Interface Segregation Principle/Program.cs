using System;

namespace Interface_Segregation_Principle
{

    public class Document
    {

    }

    public interface IMachine
    {
        public void Print(Document d);
        public Document Scan();
        public void Copy(Document d);
    }


    public class MultifunctionMachine : IMachine
    {
        public void Print(Document d)
        {
            // O.K. A multifunction machine can print.
        }

        public Document Scan()
        {
            // O.K. A multifunction machine can scan.
            return null;
        }

        public void Copy(Document d)
        {
            // O.K. A multifunction machine can copy.
        }
    }


    /// <summary>
    /// The old printer machine is only able to print, so
    /// by using the IMachine interface, we are overloading
    /// the class with unnecessary methods.
    /// </summary>
    public class OldPrinterMachine : IMachine
    {
        public void Print(Document d)
        {
            // O.K. an old printer can print.
        }

        #region Unnecessary Overload

        public Document Scan()
        {
            // K.O. An old printer cannot scan.
            return null;
        }

        public void Copy(Document d)
        {
            // K.O. An old printer cannot copy.
        }

        #endregion
    }


    // Interface segregation principle states that if an interface
    // has too many methods and most of them are not going to be
    // implemented in a derived class, it must be segregated or
    // divided into some other small interfaces.

    public interface IPrinter
    {
        public void Print(Document d);
    }

    public interface IScanner
    {
        public Document Scan();
    }

    public interface ICopier
    {
        public void Copy(Document d);
    }


    public class SegregatedMultifunction : IPrinter, IScanner, ICopier
    {
        public void Print(Document d)
        {
            // O.K.
        }

        public Document Scan()
        {
            // O.K.
            return null;
        }

        public void Copy(Document d)
        {
            // O.K.
        }
    }


    public class SegregatedOldPrinter : IPrinter
    {
        public void Print(Document d)
        {
            // O.K. An old printer only can print.
        }
    }

    // We can create interfaces that implements other interfaces.
    public interface IMultifunction : IPrinter, IScanner
    {

    }


    public class OneInterfaceMultifunctionMachine : IMultifunction
    {
        private readonly IPrinter _printer;
        private readonly IScanner _scanner;

        public OneInterfaceMultifunctionMachine(IPrinter printer, IScanner scanner)
        {
            _printer = printer;
            _scanner = scanner;
        }

        public void Print(Document d)
        {
            _printer.Print(d);
        }

        public Document Scan()
        {
            return _scanner.Scan();
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
