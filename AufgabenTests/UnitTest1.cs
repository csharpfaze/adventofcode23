
using Day01;
namespace AufgabenTests;

[TestClass]
public class UnitTest1
{
    [DataTestMethod]
    [DataRow("1abc2", 12)]
    [DataRow("pqr3stu8vwx", 38)]
    [DataRow("a1b2c3d4e5f", 15)]
    [DataRow("treb7uchet", 77)]
    public void Test_Day01(string input, int result)
    {
        Assert.AreEqual(Parse.GetNumber(input), result);
    }

    [DataTestMethod]
    [DataRow("two1nine", 29)]
    [DataRow("eightwothree",83 )]
    [DataRow("abcone2threexyz",13 )]
    [DataRow("xtwone3four", 24)]
    [DataRow("4nineeightseven2", 42)]
    [DataRow("zoneight234", 14)]
    [DataRow("7pqrstsixteen", 76)]
    public void Test_Day01_Advanced(string input, int result)
    {
        Assert.AreEqual(Parse.GetNumberIncludingWritten(input), result);
    }
}