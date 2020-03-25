// enum com os agrupamentos que estarão no jogo
public enum Agrupamento
{
    Individual,
    Duplas,
    Grupos
}

public static class AgrupamentoExtensions
{
    public static string Nome(this Agrupamento agrupamento)
    {
        switch (agrupamento)
        {
            case Agrupamento.Individual: return "Individual";
            case Agrupamento.Duplas: return "Duplas";
            case Agrupamento.Grupos: return "Grupos";
            default: return "";
        }
    }
}