using Taller.Application.Emails;
using Xunit;

public class ApprovalEmailParserTests
{
    [Theory]
    [InlineData("APROBADO #123", "APROBADO", 123)]
    [InlineData("rechazado   #77", "RECHAZADO", 77)]
    public void Parse_DeberiaParsearCorrectamente(string body, string estadoEsperado, int idEsperado)
    {
        var result = ApprovalEmailParser.Parse(body);

        Assert.NotNull(result);
        Assert.Equal(estadoEsperado, result!.Value.estado);
        Assert.Equal(idEsperado, result.Value.presupuestoId);
    }

    [Fact]
    public void Parse_DeberiaRetornarNull_CuandoNoCoincide()
    {
        var result = ApprovalEmailParser.Parse("texto libre sin formato");
        Assert.Null(result);
    }
}
