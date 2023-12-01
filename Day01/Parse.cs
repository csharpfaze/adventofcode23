namespace Day01
{
    public static class Parse
    {
        public static int GetFirstNumber(string input)
        {
            int result = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (int.TryParse("" + input[i], out result))
                    break;
            }

            return result;
        }

        public static int GetLastNumber(string input)
        {
            int result = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if (int.TryParse("" + input[i], out result))
                    break;
            }

            return result;
        }

        public static int GetNumber(string input)
        {
            var n1 = Parse.GetFirstNumber(input);
            var n2 = Parse.GetLastNumber(input);
            return Convert.ToInt32($"{n1}{n2}");
        }

        private static string[] numbers = { "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
        private static int? ResolveWrittenNumber(string writtenNumber)
        {
            switch (writtenNumber)
            {
                case "one": return 1;
                case "two": return 2;
                case "three": return 3;
                case "four": return 4;
                case "five": return 5;
                case "six": return 6;
                case "seven": return 7;
                case "eight": return 8;
                case "nine": return 9;
                default:
                    throw new ArgumentException();
            }
        }
        private static List<Match> FindRealNumbers(string input)
        {
            var matches = new List<Match>();
            for (int i = 0; i < input.Length; i++)
            {
                int number;
                if (int.TryParse("" + input[i], out number))
                {
                    matches.Add(new Match(i, number));
                }
            }
            return matches;
        }

        private static List<Match> FindWrittenNumbers(string input)
        {
            var matches = new List<Match>();
            foreach (var number in numbers)
            {
                if (input.Contains(number))
                {
                    if (input.IndexOf(number) == input.LastIndexOf(number))
                    {
                        matches.Add(new Match(input.IndexOf(number), ResolveWrittenNumber(number)));
                    }
                    else
                    {
                        matches.Add(new Match(input.IndexOf(number), ResolveWrittenNumber(number)));
                        matches.Add(new Match(input.LastIndexOf(number), ResolveWrittenNumber(number)));
                    }
                }
            }
            return matches;
        }

        public static int GetNumberIncludingWritten(string input)
        {
            var numbers = FindRealNumbers(input);
            numbers.AddRange(FindWrittenNumbers(input));

            var n1 = numbers.First(n => n.Index == numbers.Min(r => r.Index)).Number;
            var n2 = numbers.First(n => n.Index == numbers.Max(r => r.Index)).Number; ;
            return Convert.ToInt32($"{n1}{n2}");
        }

    }

    public record Match(int? Index, int? Number);
}