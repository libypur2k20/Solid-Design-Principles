using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Dependency_Inversion_Principle
{

    public enum Relationships
    {
        Parent,
        Child,
        Sibling
    }


    public class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
    }


    public interface IRelationshipBrowser
    {
        public IEnumerable<Person> FindAllChildrenOf(String name);
    }


//Low Level
    public class Relationship : IRelationshipBrowser
    {
        private List<(Person, Relationships, Person)> _relationsList = new List<(Person, Relationships, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            _relationsList.Add((parent,Relationships.Parent,child));
            _relationsList.Add((child,Relationships.Child,parent));
        }

        //Exposing low level properties directly to the high level is a bad practice,
        //because if the structure of the list changes, the high level code must be refactored.
        //Dependency Inversion Principle states that we can use interfaces to expose low level
        //functionality to the high level.
       // public List<(Person, Relationships, Person)> RelationsList => _relationsList;


       //IRelationshipBrowser Implementation.
        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return _relationsList.Where(r => r.Item1.Name == name && r.Item2 == Relationships.Parent)
                .Select(r => r.Item3);
        }
    }


    //High Level
    public class Browser
    {
        private readonly Relationship _relationship;

        public Browser(Relationship relationship)
        {
            _relationship = relationship;
        }

        //public IEnumerable<Person> FindChildren(Person p)
        //{
            //Accessing low level data directly goes against good practices.

            //foreach (var rel in _relationship.RelationsList)
            //{
            //    if (rel.Item1.Name == p.Name && rel.Item2 == Relationships.Parent)
            //        yield return rel.Item3;
            //}


        //}

        public void FindChildren(Person p)
        {
            foreach (var child in _relationship.FindAllChildrenOf(p.Name))
            {
                Console.WriteLine(($"Parent {p.Name} has a child named {child.Name}"));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Person parent = new Person("John");
            Person child1 = new Person("James");
            Person child2 = new Person("Ann");

            Relationship rel = new Relationship();
            rel.AddParentAndChild(parent,child1);
            rel.AddParentAndChild(parent, child2);

            Browser browser = new Browser(rel);

            //BAD PRACTICE: Accessing low level directly from high level.

            //foreach(Person p in browser.FindChildren(parent))
            //    Console.WriteLine($"Parent: {parent.Name} has a child named {p.Name}");

            //GOOD PRACTICE: Dependency Inversion Principle.
            browser.FindChildren(parent);
        }
    }
}
