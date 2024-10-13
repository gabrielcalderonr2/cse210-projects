using System;
using System.Collections.Generic;

namespace YouTubeTracking
{
    // Comment class to store comment details
    class Comment
    {
        public string CommenterName { get; private set; }
        public string Text { get; private set; }

        public Comment(string commenterName, string text)
        {
            CommenterName = commenterName;
            Text = text;
        }

        public void Display()
        {
            Console.WriteLine($"{CommenterName}: {Text}");
        }
    }

    // Video class to store video details and associated comments
    class Video
    {
        public string Title { get; private set; }
        public string Author { get; private set; }
        public int Length { get; private set; } // in seconds
        private List<Comment> Comments { get; set; }

        public Video(string title, string author, int length)
        {
            Title = title;
            Author = author;
            Length = length;
            Comments = new List<Comment>();
        }

        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
        }

        public int GetCommentCount()
        {
            return Comments.Count;
        }

        public void Display()
        {
            Console.WriteLine($"Title: {Title}");
            Console.WriteLine($"Author: {Author}");
            Console.WriteLine($"Length: {Length} seconds");
            Console.WriteLine($"Number of Comments: {GetCommentCount()}");
            Console.WriteLine("Comments:");
            foreach (var comment in Comments)
            {
                comment.Display();
            }
            Console.WriteLine();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Create videos
            Video video1 = new Video("How to Code in C#", "John Doe", 300);
            Video video2 = new Video("Cooking with Jamie", "Jamie Oliver", 600);
            Video video3 = new Video("Travel Vlog: Paris", "Emily Travel", 420);

            // Create comments for video1
            video1.AddComment(new Comment("Alice", "Great tutorial!"));
            video1.AddComment(new Comment("Bob", "Very helpful, thanks!"));
            video1.AddComment(new Comment("Charlie", "I learned a lot!"));

            // Create comments for video2
            video2.AddComment(new Comment("Dave", "Delicious recipe!"));
            video2.AddComment(new Comment("Eve", "Trying this tonight."));
            video2.AddComment(new Comment("Frank", "Love your videos!"));

            // Create comments for video3
            video3.AddComment(new Comment("Grace", "Beautiful footage!"));
            video3.AddComment(new Comment("Heidi", "Paris is amazing!"));
            video3.AddComment(new Comment("Ivan", "Can't wait to travel there."));

            // Add videos to list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Display each video and its comments
            foreach (var video in videos)
            {
                video.Display();
            }
        }
    }
}
