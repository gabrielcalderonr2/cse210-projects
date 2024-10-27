using System;
using System.Collections.Generic;

// Base class for all goals
abstract class Goal
{
    public string Name { get; protected set; }
    protected int points;
    public int Points
    {
        get { return points; }
        protected set { points = value; }
    }

    public abstract void RecordEvent();
    public abstract bool IsComplete();
    public abstract string GetDetails();
}

// Simple goal that is completed once and earns points
class SimpleGoal : Goal
{
    private bool _isComplete;

    public SimpleGoal(string name, int points)
    {
        Name = name;
        Points = points;
        _isComplete = false;
    }

    public override void RecordEvent()
    {
        if (!_isComplete)
        {
            _isComplete = true;
            Console.WriteLine($"Goal '{Name}' completed! You earned {Points} points.");
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already completed.");
        }
    }

    public override bool IsComplete() => _isComplete;

    public override string GetDetails()
    {
        return _isComplete ? $"[X] {Name} - {Points} points" : $"[ ] {Name} - {Points} points";
    }
}

// Eternal goal that earns points every time it's recorded
class EternalGoal : Goal
{
    public EternalGoal(string name, int points)
    {
        Name = name;
        Points = points;
    }

    public override void RecordEvent()
    {
        Console.WriteLine($"Recorded '{Name}'. You earned {Points} points.");
    }

    public override bool IsComplete() => false;

    public override string GetDetails()
    {
        return $"[∞] {Name} - {Points} points per entry";
    }
}

// Checklist goal requiring multiple completions
class ChecklistGoal : Goal
{
    private int _targetCount;
    private int _currentCount;
    private int _bonus;

    public ChecklistGoal(string name, int points, int targetCount, int bonus)
    {
        Name = name;
        Points = points;
        _targetCount = targetCount;
        _currentCount = 0;
        _bonus = bonus;
    }

    public int Bonus => _bonus;

    public override void RecordEvent()
    {
        if (_currentCount < _targetCount)
        {
            _currentCount++;
            Console.WriteLine($"Recorded '{Name}'. You earned {Points} points.");

            if (_currentCount == _targetCount)
            {
                Console.WriteLine($"Checklist complete! You earned a bonus of {_bonus} points.");
            }
        }
        else
        {
            Console.WriteLine($"Goal '{Name}' is already complete.");
        }
    }

    public override bool IsComplete() => _currentCount >= _targetCount;

    public override string GetDetails()
    {
        return IsComplete() 
            ? $"[X] {Name} - Completed {_currentCount}/{_targetCount} times" 
            : $"[ ] {Name} - Completed {_currentCount}/{_targetCount} times, {Points} points each, {_bonus} bonus points";
    }
}

class GoalManager
{
    private List<Goal> _goals = new List<Goal>();
    private int _score = 0;

    public void CreateGoal(Goal goal)
    {
        _goals.Add(goal);
    }

    public void RecordEvent(int goalIndex)
    {
        if (goalIndex < 0 || goalIndex >= _goals.Count)
        {
            Console.WriteLine("Invalid goal index.");
            return;
        }

        Goal goal = _goals[goalIndex];
        goal.RecordEvent();

        int earnedPoints = goal.Points;
        if (goal is ChecklistGoal checklistGoal && checklistGoal.IsComplete())
        {
            earnedPoints += checklistGoal.Bonus;
        }
        _score += earnedPoints;
    }

    public void DisplayGoals()
    {
        for (int i = 0; i < _goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_goals[i].GetDetails()}");
        }
    }

    public void DisplayScore()
    {
        Console.WriteLine($"Total Score: {_score}");
    }

    public void SaveGoals()
    {
        Console.WriteLine("Goals saved.");
    }

    public void LoadGoals()
    {
        Console.WriteLine("Goals loaded.");
    }
}

class Program
{
    static void Main()
    {
        GoalManager manager = new GoalManager();

        while (true)
        {
            Console.WriteLine("\nEternal Quest");
            Console.WriteLine("1. Create Simple Goal");
            Console.WriteLine("2. Create Eternal Goal");
            Console.WriteLine("3. Create Checklist Goal");
            Console.WriteLine("4. Record Event");
            Console.WriteLine("5. Show Goals");
            Console.WriteLine("6. Show Score");
            Console.WriteLine("7. Save Goals");
            Console.WriteLine("8. Load Goals");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter goal name: ");
                    string simpleName = Console.ReadLine();
                    int simplePoints = ReadInteger("Enter points: ");
                    manager.CreateGoal(new SimpleGoal(simpleName, simplePoints));
                    break;
                case "2":
                    Console.Write("Enter goal name: ");
                    string eternalName = Console.ReadLine();
                    int eternalPoints = ReadInteger("Enter points: ");
                    manager.CreateGoal(new EternalGoal(eternalName, eternalPoints));
                    break;
                case "3":
                    Console.Write("Enter goal name: ");
                    string checklistName = Console.ReadLine();
                    int checklistPoints = ReadInteger("Enter points per completion: ");
                    int targetCount = ReadInteger("Enter target count: ");
                    int bonus = ReadInteger("Enter bonus on completion: ");
                    manager.CreateGoal(new ChecklistGoal(checklistName, checklistPoints, targetCount, bonus));
                    break;
                case "4":
                    int goalIndex = ReadInteger("Enter goal index to record: ") - 1;
                    manager.RecordEvent(goalIndex);
                    break;
                case "5":
                    manager.DisplayGoals();
                    break;
                case "6":
                    manager.DisplayScore();
                    break;
                case "7":
                    manager.SaveGoals();
                    break;
                case "8":
                    manager.LoadGoals();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static int ReadInteger(string prompt)
    {
        int number;
        while (true)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (int.TryParse(input, out number))
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }
        return number;
    }
}
