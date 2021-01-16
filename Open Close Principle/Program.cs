using System;
using System.Collections.Generic;
using System.Linq;

namespace Open_Close_Principle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Product[] products =
            {
                new Product("Yuoof",Color.Green, Size.Medium),
                new Product("Riao",Color.Green, Size.Large),
                new Product("Muuf",Color.Green, Size.Medium),
                new Product("Scroing", Color.Blue, Size.XLarge),
                new Product("Riang",Color.Blue,Size.XLarge),
                new Product("Tzing", Color.Black, Size.Large)
            };

            //Old Way
            ProductFilter pFilter = new ProductFilter();
            foreach (Product p in pFilter.FilterByColorAndSize(products, Color.Blue, Size.XLarge))
                Console.WriteLine($"Product: {p.name}, Color: {p.color}, Size: {p.size}");


            //New Way
            ColorSpecification colorSpec = new ColorSpecification(Color.Green);
            SizeSpecification sizeSpec = new SizeSpecification(Size.Medium);

            BetterFilter filter = new BetterFilter();
            foreach (Product p in filter.Filter(products, new ISpecification<Product>[] { colorSpec, sizeSpec }))
                Console.WriteLine($"Product: {p.name}, Color: {p.color}, Size: {p.size}");


        }
    }


    public enum Color
    {
        Green, Blue, White, Yellow, Black
    }

    public enum Size
    {
        Small, Medium, Large, XLarge, XXLarge
    }

    public class Product{

        public string name;
        public Color color;
        public Size size;

        public Product(string name, Color color, Size size)
        {
            this.name = name;
            this.color = color;
            this.size = size;
        }
    }



    #region OLD WAY (MULTIPLE METHODS WITH DIFFERENT SIGNATURES)

    public class ProductFilter
    {
        public IEnumerable<Product> FilterByColor(IEnumerable<Product> prods, Color color)
        {
            foreach(Product p in prods)
                if(p.color == color)
                    yield return p;
        }

        public IEnumerable<Product> FilterBySize(IEnumerable<Product> prods, Size size)
        {
            foreach (Product p in prods)
                if (p.size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColorAndSize(IEnumerable<Product> prods, Color color, Size size)
        {
            foreach (Product p in prods)
                if (p.color == color && p.size == size)
                    yield return p;
        }
    }

    #endregion


    #region NEW WAY (OPEN - CLOSE PRINCIPLE)

    public interface ISpecification<T>
    {
        public bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        public IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T>[] specs);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return (t.color == this.color);
        }
    }


    public class SizeSpecification : ISpecification<Product>
    {
        Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return (t.size == this.size);
        }
    }


    /// <summary>
    /// In the Open Close Principle, the implementation of the Filter method cannot be modified (Close),
    /// but it can be extensible (Open) through an array of any number of specifications.
    /// </summary>
    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product>[] specs)
        {
            foreach (Product p in items)
            {
                if (specs.All(s => s.IsSatisfied(p)))
                        yield return p;
            }
        }
    }

    #endregion
}
