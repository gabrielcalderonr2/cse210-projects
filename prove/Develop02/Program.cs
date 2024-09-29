using System;
using System.Collections.Generic;
using System.IO;

public class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public string Date { get; set; }

    public Entry(string prompt, string response)
    {
        Prompt = prompt;
        Response = response;
        Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public override string ToString()
    {
        return $"[{Date}]\nPrompt: {Prompt}\nResponse: {Response}\n";
    }
}

public class Journal
{
    private List<Entry> _entries;
    private List<string> _prompts;

    public Journal()
    {
        _entries = new List<Entry>();
        _prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };
    }

    public void AddEntry()
    {
        var random = new Random();
        string prompt = _prompts[random.Next(_prompts.Count)];
        Console.WriteLine(prompt);
        Console.Write("Your response: ");
        string response = Console.ReadLine();
        Entry entry = new Entry(prompt, response);
        _entries.Add(entry);
        Console.WriteLine("Entry added successfully!\n");
    }

    public void DisplayEntries()
    {
        if (_entries.Count == 0)
        {
            Console.WriteLine("No entries to display.\n");
        }
        else
        {
            foreach (var entry in _entries)
            {
                Console.WriteLine(entry);
            }
        }
    }

    public void SaveToFile(string filename)
    {
        using (StreamWriter file = new StreamWriter(filename))
        {
            foreach (var entry in _entries)
            {
                file.WriteLine($"{entry.Date}|{entry.Prompt}|{entry.Response}");
            }
        }
        Console.WriteLine($"Journal saved to {filename}\n");
    }

    public void LoadFromFile(string filename)
    {
        try
        {
            _entries.Clear();
            using (StreamReader file = new StreamReader(filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var parts = line.Split('|');
                    var entry = new Entry(parts[1], parts[2]);
                    entry.Date = parts[0];
                    _entries.Add(entry);
                }
            }
            Console.WriteLine($"Journal loaded from {filename}\n");
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("Error: File not found.\n");
        }
    }
}

public class Program
{
    private Journal journal;

    public Program()
    {
        journal = new Journal();
    }

    public void DisplayMenu()
    {
        Console.WriteLine("Journal Menu");
        Console.WriteLine("1. Write a new entry");
        Console.WriteLine("2. Display the journal");
        Console.WriteLine("3. Save the journal to a file");
        Console.WriteLine("4. Load the journal from a file");
        Console.WriteLine("5. Exit");
    }

    public void HandleChoice(string choice)
    {
        switch (choice)
        {
            case "1":
                journal.AddEntry();
                break;
            case "2":
                journal.DisplayEntries();
                break;
            case "3":
                Console.Write("Enter the filename to save the journal: ");
                string saveFilename = Console.ReadLine();
                journal.SaveToFile(saveFilename);
                break;
            case "4":
                Console.Write("Enter the filename to load the journal: ");
                string loadFilename = Console.ReadLine();
                journal.LoadFromFile(loadFilename);
                break;
            case "5":
                Console.WriteLine("Exiting the program.");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice, please try again.");
                break;
        }
    }

    public void Run()
    {
        while (true)
        {
            DisplayMenu();
            Console.Write("Choose an option: ");
            string choice = Console.ReadLine();
            HandleChoice(choice);
        }
    }

    public static void Main(string[] args)
    {
        Program program = new Program();
        program.Run();
    }
}
