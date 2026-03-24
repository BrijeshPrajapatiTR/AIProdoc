using System.Text;
using System.Text.RegularExpressions;
using NetProcalc23Feb.Application.Interfaces;
using NetProcalc23Feb.Domain.Entities;

namespace NetProcalc23Feb.Application.Services;

public class ClarionTxAParser : IClarionTxAParser
{
    // Very lightweight TXA parser focused on MENU and PROCEDURE names/captions
    // Works with exported TXA from procalc.txa
    public async Task<(IReadOnlyList<MenuItem> Menus, IReadOnlyList<ProcedureDef> Procedures)> ParseAsync(string txaPath, CancellationToken ct = default)
    {
        if (!File.Exists(txaPath)) throw new FileNotFoundException($"TXA not found at {txaPath}");
        var text = await File.ReadAllTextAsync(txaPath, Encoding.UTF8, ct);

        // Extract procedures
        var procRegex = new Regex(@"(?mi)^\s*PROCEDURE\(\s*Name\s*=\s*'(?<name>[^']+)'\s*,\s*Type\s*=\s*'?(?<type>[^,'\r\n\)]*)", RegexOptions.Compiled);
        var procedures = procRegex.Matches(text)
            .Select(m => new ProcedureDef { Name = Sanitize(m.Groups["name"].Value), Type = m.Groups["type"].Value.Trim() })
            .GroupBy(p => p.Name)
            .Select(g => g.First())
            .ToList();

        // Extract menus: look for ITEM('Caption') and Procedure='X'
        var menuRegex = new Regex(@"(?mi)^\s*ITEM\(\s*'(?<caption>[^']+)'[^\)]*\)\s*(?:,\s*PROCEDURE\s*=\s*'?(?<proc>[^,'\r\n\)]*)") , RegexOptions.Compiled);
        var menus = new List<MenuItem>();
        int order = 0;
        foreach (Match m in menuRegex.Matches(text))
        {
            menus.Add(new MenuItem
            {
                Caption = m.Groups["caption"].Value.Trim(),
                ProcedureName = string.IsNullOrWhiteSpace(m.Groups["proc"].Value) ? null : Sanitize(m.Groups["proc"].Value),
                Order = order++
            });
        }

        // Also try to infer menu hierarchy SECTION('Caption') ... END
        var sectionRegex = new Regex(@"(?mi)^\s*SECTION\(\s*'(?<caption>[^']+)'\s*\)", RegexOptions.Compiled);
        string? currentSection = null;
        order = 0;
        foreach (var line in text.Split('\n'))
        {
            var sm = sectionRegex.Match(line);
            if (sm.Success)
            {
                currentSection = sm.Groups["caption"].Value.Trim();
                order = 0;
                continue;
            }
            var im = Regex.Match(line, @"(?mi)^\s*ITEM\(\s*'(?<caption>[^']+)'", RegexOptions.Compiled);
            if (im.Success && currentSection != null)
            {
                var mi = menus.FirstOrDefault(x => x.Caption == im.Groups["caption"].Value.Trim());
                if (mi != null) mi.ParentCaption = currentSection;
            }
        }

        return (menus, procedures);
    }

    private static string Sanitize(string input)
        => new string(input.Where(c => char.IsLetterOrDigit(c) || c=='_' || c=='-').ToArray());
}
