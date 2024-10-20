using System;
using System.Collections.Generic;
using System.Threading;

namespace MindfulnessApp
{
    // Base class for shared attributes and behaviors
    abstract class MindfulnessActivity
    {
        protected string Name { get; set; }
        protected string Description { get; set; }
        protected int Duration { get; set; }

        public void Start()
        {
            Console.WriteLine($"Starting {Name} Activity");
            Console.WriteLine(Description);
            Console.Write("Enter the duration for the activity in seconds: ");
            Duration = int.Parse(Console.ReadLine());
            Console.WriteLine("Prepare to begin...");
            ShowSpinner(3);
            PerformActivity();
            Finish();
        }

        protected abstract void PerformActivity();

        protected void ShowSpinner(int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                Console.Write(".");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }

        protected void Finish()
        {
            Console.WriteLine("Well done! You have completed the activity.");
            Console.WriteLine($"You completed the {Name} activity for {Duration} seconds.");
            ShowSpinner(3);
        }
    }

    // Breathing Activity
    class BreathingActivity : MindfulnessActivity
    {
        public BreathingActivity()
        {
            Name = "Breathing";
            Description = "This activity will help you relax by walking you through breathing in and out slowly. Clear your mind and focus on your breathing.";
        }

        protected override void PerformActivity()
        {
            int secondsPerBreath = 4;
            int cycles = Duration / (2 * secondsPerBreath); // One cycle is breathe in + breathe out

            for (int i = 0; i < cycles; i++)
            {
                Console.WriteLine("Breathe in...");
                ShowCountdown(secondsPerBreath);
                Console.WriteLine("Breathe out...");
                ShowCountdown(secondsPerBreath);
            }
        }

        private void ShowCountdown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i + " ");
                Thread.Sleep(1000);
            }
            Console.WriteLine();
        }
    }

    // Reflection Activity
    class ReflectionActivity : MindfulnessActivity
    {
        private List<string> prompts = new List<string>
        {
            "Think of a time when you stood up for someone else.",
            "Think of a time when you did something really difficult.",
            "Think of a time when you helped someone in need.",
            "Think of a time when you did something truly selfless."
        };

        private List<string> questions = new List<string>
        {
            "Why was this experience meaningful to you?",
            "Have you ever done anything like this before?",
            "How did you get started?",
            "How did you feel when it was complete?",
            "What made this time different than other times when you were not as successful?",
            "What is your favorite thing about this experience?",
            "What could you learn from this experience that applies to other situations?",
            "What did you learn about yourself through this experience?",
            "How can you keep this experience in mind in the future?"
        };

        public ReflectionActivity()
        {
            Name = "Reflection";
            Description = "This activity will help you reflect on times in your life when you have shown strength and resilience.";
        }

        protected override void PerformActivity()
        {
            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine(prompt);

            int timePerQuestion = Duration / questions.Count;
            foreach (string question in questions)
            {
                if (timePerQuestion > 0)
                {
                    Console.WriteLine(question);
                    ShowSpinner(timePerQuestion);
                }
            }
        }
    }

    // Listing Activity
    class ListingActivity : MindfulnessActivity
    {
        private List<string> prompts = new List<string>
        {
            "Who are people that you appreciate?",
            "What are personal strengths of yours?",
            "Who are people that you have helped this week?",
            "When have you felt particularly inspired?",
            "Who are some of your personal heroes?"
        };

        public ListingActivity()
        {
            Name = "Listing";
            Description = "This activity will help you reflect on the good things in your life by having you list as many things as you can in a certain area.";
        }

        protected override void PerformActivity()
        {
            Random random = new Random();
            string prompt = prompts[random.Next(prompts.Count)];
            Console.WriteLine(prompt);

            Console.WriteLine("You will have some time to think about this prompt and list items.");
            ShowSpinner(5); // Countdown to start listing

            DateTime endTime = DateTime.Now.AddSeconds(Duration);
            List<string> items = new List<string>();

            while (DateTime.Now < endTime)
            {
                Console.Write("Item: ");
                string item = Console.ReadLine();
                if (!string.IsNullOrEmpty(item))
                {
                    items.Add(item);
                }
            }

            Console.WriteLine($"You listed {items.Count} items.");
        }
    }

    class Program
    {
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Mindfulness App");
                Console.WriteLine("Choose an activity:");
                Console.WriteLine("1 - Breathing Activity");
                Console.WriteLine("2 - Reflection Activity");
                Console.WriteLine("3 - Listing Activity");
                Console.WriteLine("0 - Exit");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                MindfulnessActivity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        break;
                    case "2":
                        activity = new ReflectionActivity();
                        break;
                    case "3":
                        activity = new ListingActivity();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        continue;
                }

                activity?.Start();
            }
        }
    }
}
