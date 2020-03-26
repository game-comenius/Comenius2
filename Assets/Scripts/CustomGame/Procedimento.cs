// enum com os procedimentos que estarão no jogo
public enum Procedimento
{
    Pesquisa,
    DiscussaoEntreAlunos,
    AulaExpositiva,
    Atividade,
    Seminario,
}

public static class ProcedimentoExtensions
{
    public static string Nome(this Procedimento procedimento)
    {
        switch (procedimento)
        {
            case Procedimento.Pesquisa: return "Pesquisa";
            case Procedimento.DiscussaoEntreAlunos: return "Discussão entre alunos";
            case Procedimento.AulaExpositiva: return "Aula expositiva";
            case Procedimento.Atividade: return "Atividade";
            case Procedimento.Seminario: return "Seminário";
            default: return "";
        }
    }
}