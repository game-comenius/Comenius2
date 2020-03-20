public enum SalaDeAula
{
    SalaDeCiencias,
    SalaDeHistoria,
    SalaDePortugues,
    SalaDeInformatica
}

public static class SalaDeAulaExtensions
{
    public static string NomeCompleto(this SalaDeAula salaDeAula)
    {
        switch (salaDeAula)
        {
            case SalaDeAula.SalaDeCiencias:
                return "Sala de ciências";
            case SalaDeAula.SalaDeHistoria:
                return "Sala de história";
            case SalaDeAula.SalaDePortugues:
                return "Sala de português";
            case SalaDeAula.SalaDeInformatica:
                return "Sala de informática";
            default:
                return "";
        }
    }
}