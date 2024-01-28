using System.Text;
using Frank.Security.Resources;
using Xunit.Abstractions;

namespace Frank.Security.Tests;

public class UnitTest1
{
    private readonly ITestOutputHelper _output;

    public UnitTest1(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void GetNouns()
    {
        _output.WriteCSharp(ListOfString);
    }

    private List<string> ListOfString
        = new List<string>()
        {
            "a",
            "abaft",
            "aboard",
            "about",
            "above",
            "absent",
            "across",
            "afore",
            "after",
            "against",
            "along",
            "alongside",
            "amid",
            "amidst",
            "among",
            "amongst",
            "an",
            "anenst",
            "apropos",
            "apud",
            "around",
            "as",
            "aside",
            "astride",
            "at",
            "athwart",
            "atop",
            "barring",
            "before",
            "behind",
            "below",
            "beneath",
            "beside",
            "besides",
            "between",
            "beyond",
            "but",
            "by",
            "circa",
            "concerning",
            "despite",
            "down",
            "during",
            "except",
            "excluding",
            "failing",
            "following",
            "for",
            "forenenst",
            "from",
            "given",
            "in",
            "including",
            "inside",
            "into",
            "lest",
            "like",
            "mid",
            "midst",
            "minus",
            "modulo",
            "near",
            "next",
            "notwithstanding",
            "of",
            "off",
            "on",
            "onto",
            "opposite",
            "out",
            "outside",
            "over",
            "pace",
            "past",
            "per",
            "plus",
            "pro",
            "qua",
            "regarding",
            "round",
            "sans",
            "save",
            "since",
            "than",
            "through",
            "throughout",
            "till",
            "times",
            "to",
            "toward",
            "towards",
            "under",
            "underneath",
            "unlike",
            "until",
            "unto",
            "up",
            "upon",
            "versus",
            "via",
            "vice",
            "with",
            "within",
            "without",
            "worth"
        };
}