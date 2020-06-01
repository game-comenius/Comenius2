public enum Poder
{
    Fraca = 0,
    Boa = 7,
    MuitoBoa = 8,
    Melhor = 10
}

public static class PoderExtensions
{
    public static string Nome(this Poder poder)
    {
        switch (poder)
        {
            case Poder.Fraca:
                return "Fraca";
            case Poder.Boa:
                return "Boa";
            case Poder.MuitoBoa:
                return "Muito Boa";
            case Poder.Melhor:
                return "Melhor";
            default:
                return "";
        }
    }
}