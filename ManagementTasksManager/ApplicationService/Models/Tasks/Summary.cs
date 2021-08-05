namespace ApplicationService.Models.Tasks
{
    using System;

    public class Summary
    {
        private const int MaxSize = 2500;

        public Summary(string summary)
        {
            if (summary.Length > MaxSize)
                throw new ArgumentException($"Summary can't have more than {MaxSize} characters");

            Content = summary;
        }

        public string Content { get; set; }
    }
}