namespace Day03;

public class EngineValidator
{
    private char[][] chars;

    public EngineValidator(IEnumerable<string> map)
    {
        chars = ConvertToEngineArray(map);
    }

    public IEnumerable<int> GetEngineParts()
    {
        var parts = new List<int>();
        for (int y = 0; y < chars.Length; y++)
        {
            var line = chars[y];
            var numbers = FindNumbers(line);
            foreach (var number in numbers)
            {
                if (IsPartNumber(number, y))
                {
                    parts.Add(number.Value);
                }
            }
        }
        return parts;
    }

    private char[][] ConvertToEngineArray(IEnumerable<string> map)
    {
        var arr = new List<char[]>();
        for (int i = 0; i < map.Count(); i++)
        {
            var line = map.ElementAt(i);
            var arrLine = new List<char>();
            foreach (var c in line)
            {
                arrLine.Add(c);
            }
            arr.Add(arrLine.ToArray());

        }
        return arr.ToArray();
    }

    private bool IsSymbolAdjacent(int x, int y, Func<char, bool> symbolValidator = null)
    {
        if (symbolValidator == null)
            symbolValidator = IsSymbol;
        var symbolHere = false;
        //l
        if (CanMoveLeft(x) && symbolValidator(chars[y][x - 1]))
            symbolHere = true;
        //r
        else if (CanMoveRight(x, y) && symbolValidator(chars[y][x + 1]))
            symbolHere = true;
        //up
        else if (CanMoveUp(y) && symbolValidator(chars[y - 1][x]))
            symbolHere = true;
        //d
        else if (CanMoveDown(y) && symbolValidator(chars[y + 1][x]))
            symbolHere = true;
        //lu
        else if (CanMoveUp(y) && CanMoveLeft(x) && symbolValidator(chars[y - 1][x - 1]))
            symbolHere = true;
        //ru
        else if (CanMoveUp(y) && CanMoveRight(x, y) && symbolValidator(chars[y - 1][x + 1]))
            symbolHere = true;
        //ld
        else if (CanMoveDown(y) && CanMoveLeft(x) && symbolValidator(chars[y + 1][x - 1]))
            symbolHere = true;
        //rd
        else if (CanMoveDown(y) && CanMoveRight(x, y) && symbolValidator(chars[y + 1][x + 1]))
            symbolHere = true;

        return symbolHere;
    }

    #region navigate
    private bool CanMoveDown(int y)
    {
        return y < chars.Count() - 1;
    }

    private static bool CanMoveUp(int y)
    {
        return y > 0;
    }

    private bool CanMoveRight(int x, int y)
    {
        return x < chars[y].Count() - 1;
    }

    private static bool CanMoveLeft(int x)
    {
        return x > 0;
    }
    #endregion
    private bool IsSymbol(char c)
    {
        //IsSymbol does not work with e.g. '*'
        return c != '.' && !char.IsDigit(c);
    }

    private bool IsPartNumber(Number number, int y)
    {
        for (int i = number.StartIndex; i <= number.EndIndex; i++)
        {
            if (IsSymbolAdjacent(i, y))
                return true;
        }
        return false;
    }

    private IEnumerable<Number> FindNumbers(char[] chars)
    {
        var numbers = new List<Number>();
        for (int i = 0; i < chars.Length; i++)
        {
            if (char.IsNumber(chars[i]))
            {
                var start = i;
                while (i + 1 < chars.Length && char.IsNumber(chars[i + 1]))
                {
                    i += 1;
                }
                numbers.Add(new Number { StartIndex = start, EndIndex = i, Value = GetValue(chars, start, i) });
            }
        }
        return numbers;
    }

    private int GetValue(char[] chars, int start, int end)
    {
        var val = "";
        for (int i = start; i <= end; i++)
        {
            val += chars[i];
        }
        return Convert.ToInt32(val);
    }
}

public class Gear
{
    public Number Part1 { get; set; }
    public Number Part2 { get; set; }
}

public class Number
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public int Value { get; set; }
}