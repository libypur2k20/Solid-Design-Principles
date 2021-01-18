using System;
using System.ComponentModel.Design;
using System.Reflection;

namespace Liskov_Substitution_Principle
{
    class Program
    {

        public static int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Rectangle re = new Rectangle(2, 3);
            Console.WriteLine($"Rectangle: {re} has an area of {Area(re)}");

            Rectangle sq = new Square();// {Height = 4};
            sq.Height = 4;
            Console.WriteLine($"Square: {sq} has an area of {Area(sq)}");
        }
    }

    public class Rectangle
    {
        //Violates the Liskov Substitution Principle
        /*
        public int Width { get; set; }
        public int Height { get; set; }
        */

        //Complies with Liskov Substitution Principle
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {
            
        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }


        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }


    }

    public class Square : Rectangle
    {

        /* Violates the Liskov Substitution Principle
         
        public new int Width
        {
            set => base.Width = base.Height = value; 
        }

        public new int Height
        {
            set => base.Width = base.Height = value;
        }

        */

        //Complies with the Liskov Substitution Principle
        public override int Height
        {
            set => base.Width = base.Height = value;
        }

        public override int Width
        {
            set => base.Width = base.Height = value;
        }
    }
}
