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

    private bool IsSymbolAdjacent(int x, int y, out (int, int) symbolPosition, Func<char, bool> symbolValidator = null)
    {
        if (symbolValidator == null)
            symbolValidator = IsSymbol;
        symbolPosition = default;
        //l
        if (CanMoveLeft(x) && symbolValidator(chars[y][x - 1]))
        {
            symbolPosition = (x - 1, y);
            return true;
        }
        //r
        else if (CanMoveRight(x, y) && symbolValidator(chars[y][x + 1]))
        {
            symbolPosition = (x + 1, y);
            return true;
        }
        //up
        else if (CanMoveUp(y) && symbolValidator(chars[y - 1][x]))
        {
            symbolPosition = (x, y - 1);
            return true;
        }
        //d
        else if (CanMoveDown(y) && symbolValidator(chars[y + 1][x]))
        {
            symbolPosition = (x, y + 1);
            return true;
        }
        //lu
        else if (CanMoveUp(y) && CanMoveLeft(x) && symbolValidator(chars[y - 1][x - 1]))
        {
            symbolPosition = (x - 1, y - 1);
            return true;
        }
        //ru
        else if (CanMoveUp(y) && CanMoveRight(x, y) && symbolValidator(chars[y - 1][x + 1]))
        {
            symbolPosition = (x + 1, y - 1);
            return true;
        }
        //ld
        else if (CanMoveDown(y) && CanMoveLeft(x) && symbolValidator(chars[y + 1][x - 1]))
        {
            symbolPosition = (x - 1, y + 1);
            return true;
        }
        //rd
        else if (CanMoveDown(y) && CanMoveRight(x, y) && symbolValidator(chars[y + 1][x + 1]))
        {
            symbolPosition = (x + 1, y + 1);
            return true;
        }
        return false;
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
            if (IsSymbolAdjacent(i, y, out _))
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

    private bool IsGear(char c)
    {
        return c == '*';
    }
    private bool IsGearNumber(Number number, int y, out (int, int) conn)
    {
        for (int i = number.StartIndex; i <= number.EndIndex; i++)
        {
            if (IsSymbolAdjacent(i, y, out conn, IsGear))
            {
                return true;
            }
        }
        conn = default;
        return false;
    }
    public IEnumerable<Gear> GetGears()
    {
        var parts = new List<GearPart>();
        for (int y = 0; y < chars.Length; y++)
        {
            var line = chars[y];
            var numbers = FindNumbers(line);
            foreach (var number in numbers)
            {
                if (IsGearNumber(number, y, out var conn))
                {
                    parts.Add(new GearPart { Number = number, Conneciton = (conn.Item1, conn.Item2) });
                }
            }
        }
        return ConstructGears(parts);
    }

    private IEnumerable<Gear> ConstructGears(IEnumerable<GearPart> parts)
    {
        var gears = new List<Gear>();
        foreach (var part in parts)
        {
            if (!part.Linked)
            {
                var match = parts.Where(p => p.Conneciton == part.Conneciton);
                if (match.Count() > 1)
                {
                    var m1 = match.First();
                    var m2 = match.ElementAt(1);
                    m1.Linked = true;
                    m2.Linked = true;
                    gears.Add(new Gear { Part1 = m1.Number, Part2 = m2.Number });
                }
            }
        }
        return gears;
    }
}

public class Gear
{
    public Number Part1 { get; set; }
    public Number Part2 { get; set; }

    public int GetRatio()
    {
        return Part1.Value * Part2.Value;
    }
}

public class GearPart
{
    public Number Number { get; set; }
    public (int x, int y) Conneciton { get; set; }

    public bool Linked { get; set; }
}

public class Number
{
    public int StartIndex { get; set; }
    public int EndIndex { get; set; }
    public int Value { get; set; }
}