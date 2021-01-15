
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Single_Responsibility_Principle
{
    class Program
    {
        private const string fileName = @"C:\journal2.txt";

        static void Main(string[] args)
        {
            Journal j = new Journal();
            j.AddEntry("I was crying today");
            j.AddEntry("I ate a bug");
            Console.WriteLine(j);

            //The class must not be responsible for entry persistence.
            /*
            j.Save(@"C:\journal.txt");
            Journal jo = Journal.Load("C:\\journal.txt");
            Console.WriteLine(jo);
            */


            Persistence.SaveToFile(fileName, j.ToString(), true);
            Journal j2 = new Journal();
            Array.ForEach<string>(Persistence.LoadFromFile(fileName).Split(Environment.NewLine,StringSplitOptions.RemoveEmptyEntries), e => j2.AddEntry(e));
            Console.WriteLine(j2);
            Process.Start("notepad.exe",fileName);
            

        }
    }

    /// <summary>
    /// All methods inside the class are supposed to manage the list of entries.
    /// </summary>
    public class Journal
    {
        private readonly List<string> entries = new List<string>();

        private static int count = 0;

        public int AddEntry(string entry)
        {
            this.entries.Add($"{++count}: {entry}");
            return count; //memento design pattern.
        }


        public void RemoveEntry(int index)
        {
            this.entries.RemoveAt(index);
        }


        public override string ToString()
        {
            return string.Join(Environment.NewLine, entries);
        }


        #region ADDING PRESISTENCE RESPONSIBILITY (THIS IS AGAINST THE SRP)

        /* All this code is translated to the Persistence class.

        public void Save(string filename)
        {
            File.WriteAllText(filename, ToString());
        }


        public static Journal Load(String filename)
        {
            Journal j = new Journal();
            var fileEntries =File.ReadAllText(filename).Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            foreach (string entry in fileEntries)
                j.AddEntry(entry);
            return j;
        
        }
        */
        #endregion

    }



    /// <summary>
    /// This class is responsible for persistence. 
    /// </summary>
    public class Persistence
    {
        public static void SaveToFile(String fileName, String content, bool overwrite = false)
        {
            if (overwrite || !File.Exists(fileName)) 
                File.WriteAllText(fileName, content);
        }


        public static String LoadFromFile(String fileName)
        {
            String result = String.Empty;

            if(File.Exists(fileName))
                result =  File.ReadAllText(fileName);

            return result;
        }
    }
}
