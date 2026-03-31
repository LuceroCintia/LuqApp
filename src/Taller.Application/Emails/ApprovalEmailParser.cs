using System.Text.RegularExpressions;

namespace Taller.Application.Emails;

public static class ApprovalEmailParser
{
    private static readonly Regex Pattern = new("(APROBADO|RECHAZADO)\\s*#(\\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static (string estado, int presupuestoId)? Parse(string body)
    {
        if (string.IsNullOrWhiteSpace(body)) return null;

        var match = Pattern.Match(body);
        if (!match.Success) return null;

        return (match.Groups[1].Value.ToUpperInvariant(), int.Parse(match.Groups[2].Value));
    }
}
