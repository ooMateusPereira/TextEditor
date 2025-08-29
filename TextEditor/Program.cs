using System;
using System.IO;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

namespace TextEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Menu();
        }
        
        static void Menu()
        {
            Console.Clear();
            Console.WriteLine("What do you want to do?");
            Console.WriteLine("1 - Open File");
            Console.WriteLine("2 - Create new File");
            Console.WriteLine("0 - Exit");1
            short option = short.Parse(Console.ReadLine());

            switch (option)
            {
                case 0: System.Environment.Exit(0); break;
                case 1: Open(); break;
                case 2: Edit(); break;
                default: Menu(); break;

            }
        }
        static void Open()
        {
            Console.Clear();
            Console.WriteLine("How is file path?");
            string path = Console.ReadLine();

            if (string.IsNullOrEmpty(path)) {
                Console.WriteLine("The path can't be null or empty.");
                Thread.Sleep(1000);
                Console.WriteLine("Backing to menu.");
                Thread.Sleep(2000);
                Menu();
            }
            try
            {
                using (var file = new StreamReader(path))
                {
                    string text = file.ReadToEnd();
                    Console.WriteLine(text);
                    Console.WriteLine("---------------------");
                    Console.WriteLine("Do you want to edit?");
                    Console.WriteLine("Type 1 to edit or 0 to keeping reading.");
                    int yes_no = int.Parse(Console.ReadLine());
                    if (yes_no == 0)
                    {
                        Console.WriteLine(text);
                    }
                    if (yes_no == 1) { 
                        Edit();
                    }
                }
            }
            catch (Exception ex) { 
                Console.WriteLine(ex.ToString());
            }

            Console.WriteLine("");
            Console.ReadLine();
            Menu();
        }

        static void Edit()
        {
            Console.Clear();
            Console.WriteLine("Type your text. (Press ESQ to leave)");
            Console.WriteLine("----------");
            string text = "";

            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                text += Console.ReadLine();
                text += Environment.NewLine;
            }

            Save(text);
        }
        static void Save(string text)
        {
            Console.Clear();
            Console.WriteLine("How is the path to save file?");
            var path = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(path))
            {
                Console.WriteLine("The path can't be null or empty.");
                Thread.Sleep(1500);
                Save(text);
            }
            try
            {
                using (var file = new StreamWriter(path))
                {
                    file.WriteLine(text);
                }

                Console.WriteLine($"File saved in {path}.");
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
                Console.WriteLine("Press any buttom to back to menu");
            }  

            Console.ReadLine();
            Menu();
        }

        
    }
    
}