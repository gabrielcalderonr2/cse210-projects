using System;
using System.Collections.Generic;
using System.Linq;

namespace ScriptureMemorization
{
    class Program
    {
        static void Main(string[] args)
        {
            // List of available scriptures with their references and texts
            List<(Reference, string)> scriptures = new List<(Reference, string)>
            {
                (new Reference("Alma", "37", "37"), "Counsel with the Lord in all thy doings, and he will direct thee for good; yea, when thou liest down at night lie down unto the Lord, that he may watch over you in your sleep; and when thou risest in the morning let thy heart be full of thanks unto God; and if ye do these things, ye shall be lifted up at the last day."),
                (new Reference("2 Nephi", "2", "26"), "And the Messiah cometh in the fulness of time, that he may redeem the children of men from the fall. And because that they are redeemed from the fall they have become free forever, knowing good from evil; to act for themselves and not to be acted upon, save it be by the punishment of the law at the great and last day, according to the commandments which God hath given."),
                (new Reference("Amos", "3", "7"), "Surely the Lord God will do nothing, but he revealeth his secret unto his servants the prophets."),
                (new Reference("Ether", "12", "6"), "And now, I, Moroni, would speak somewhat concerning these things; I would show unto the world that faith is things which are hoped for and not seen; wherefore, dispute not because ye see not, for ye receive no witness until after the trial of your faith."),
                (new Reference("Alma", "39", "9"), "Now my son, I would that ye should repent and forsake your sins, and go no more after the lusts of your eyes, but cross yourself in all these things; for except ye do this ye can in nowise inherit the kingdom of God. Oh, remember, and take it upon you, and cross yourself in these things.")
            };

            Random random = new Random();

            // Main Loop
            while (true)
            {
                // Select a random scripture from the list
                var (selectedReference, selectedText) = scriptures[random.Next(scriptures.Count)];
                Scripture scripture = new Scripture(selectedReference, selectedText);

                // Inner loop to interact with the chosen scripture
                while (!scripture.AllWordsHidden())
                {
                    Console.Clear();
                    Console.WriteLine(scripture.GetDisplayText());
                    Console.WriteLine("\nPress Enter to hide words or type 'quit' to exit.");
                    string input = Console.ReadLine().Trim().ToLower();

                    if (input == "quit")
                    {
                        return;
                    }

                    scripture.HideRandomWords(3); // Hides 3 random words at each step
                }

                Console.WriteLine("\nAll words are hidden. Press Enter to continue with a new scripture or type 'quit' to exit.");
                string nextInput = Console.ReadLine().Trim().ToLower();
                if (nextInput == "quit")
                {
                    break;
                }
            }

            Console.WriteLine("\nThank you for using the scripture memorization tool. Goodbye!");
        }
    }

    class Reference
    {
        public string Book { get; private set; }
        public string Chapter { get; private set; }
        public string StartVerse { get; private set; }
        public string EndVerse { get; private set; }

        // Constructor for single verse reference
        public Reference(string book, string chapter, string verse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = verse;
            EndVerse = verse;
        }

        // Constructor for verse range reference
        public Reference(string book, string chapter, string startVerse, string endVerse)
        {
            Book = book;
            Chapter = chapter;
            StartVerse = startVerse;
            EndVerse = endVerse;
        }

        public string GetDisplayText()
        {
            return StartVerse == EndVerse ? $"{Book} {Chapter}:{StartVerse}" : $"{Book} {Chapter}:{StartVerse}-{EndVerse}";
        }
    }

    class Word
    {
        public string Text { get; private set; }
        public bool IsHidden { get; private set; }

        public Word(string text)
        {
            Text = text;
            IsHidden = false;
        }

        public void Hide()
        {
            IsHidden = true;
        }

        public string GetDisplayText()
        {
            return IsHidden ? "_____" : Text;
        }
    }

    class Scripture
    {
        public Reference Reference { get; private set; }
        private List<Word> Words { get; set; }

        public Scripture(Reference reference, string scriptureText)
        {
            Reference = reference;
            Words = scriptureText.Split(' ').Select(w => new Word(w)).ToList();
        }

        public void HideRandomWords(int count)
        {
            Random random = new Random();
            for (int i = 0; i < count; i++)
            {
                int index = random.Next(Words.Count);
                Words[index].Hide();
            }
        }

        public string GetDisplayText()
        {
            string scriptureText = string.Join(" ", Words.Select(w => w.GetDisplayText()));
            return $"{Reference.GetDisplayText()}: {scriptureText}";
        }

        public bool AllWordsHidden()
        {
            return Words.All(w => w.IsHidden);
        }
    }
}
