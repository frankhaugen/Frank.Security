using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
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
        var things = new List<string>();
        
        var cultures = GetCultures();
        foreach (var culture in cultures)
        {
            var regionName = culture.EnglishName;
            // var regionNamePart = Regex.Replace(regionName, @"[^a-zA-Z0-9]", string.Empty);
            things.Add(regionName);
        }
        
        _output.WriteCSharp(things.Where(x => x.Length > 3 && !x.Contains(' ') && !x.Contains('-')).Select(x => x.ToLowerInvariant()).Distinct().Order().ToHashSet());
    }   
    
    List<CultureInfo> GetCultures() => CultureInfo.GetCultures(CultureTypes.AllCultures)
        .OrderBy(x => x.Name)
        .DistinctBy(x => x.Name)
        .ToList();

    Dictionary<int, RegionInfo> GetRegions(List<CultureInfo> cultures)
    {
        var regions = new Dictionary<int, RegionInfo>();

        foreach (var culture in cultures)
        {
            if (TryGetRegionInfo(culture, out var regionInfo) && regionInfo != null && regionInfo.TwoLetterISORegionName.All(Char.IsLetter))
            {
                regions.TryAdd(regionInfo.GeoId, regionInfo);
            }
        }

        return regions;
    }

    bool TryGetRegionInfo(CultureInfo culture, out RegionInfo? regionInfo)
    {
        try
        {
            regionInfo = new RegionInfo(culture.Name);
            return true;
        }
        catch (Exception ex)
        {
            regionInfo = null;
            return false;
        }
    }
}