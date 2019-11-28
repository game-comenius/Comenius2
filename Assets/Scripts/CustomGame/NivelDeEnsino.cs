using System;

[Serializable]
public sealed class NivelDeEnsino
{
    public static readonly NivelDeEnsino EducacaoInfantil = new NivelDeEnsino
    (
        "Educação Infantil",
        new AreaDeConhecimento[]
        {
            AreaDeConhecimento.EuOutroNos,
            AreaDeConhecimento.CorpoGestosMovimentos,
            AreaDeConhecimento.TracosSonsCoresFormas,
            AreaDeConhecimento.EscutaFalaPensamentoImaginacao,
            AreaDeConhecimento.EspacosTemposQuantidadesRelacoesTransformacoes
        }
    );

    public static readonly NivelDeEnsino FundamentalAnosIniciais = new NivelDeEnsino
    (
        "Fundamental Anos Iniciais",
        new AreaDeConhecimento[]
        {
            AreaDeConhecimento.LinguaPortuguesa,
            AreaDeConhecimento.Artes,
            AreaDeConhecimento.EducacaoFisica,
            AreaDeConhecimento.Matematica,
            AreaDeConhecimento.CienciasDaNatureza,
            AreaDeConhecimento.Geografia,
            AreaDeConhecimento.Historia,
            AreaDeConhecimento.EnsinoReligioso
        }
    );

    public static readonly NivelDeEnsino FundamentalAnosFinais = new NivelDeEnsino
    (
        "Fundamental Anos Finais",
        new AreaDeConhecimento[]
        {
            AreaDeConhecimento.LinguaPortuguesa,
            AreaDeConhecimento.Artes,
            AreaDeConhecimento.EducacaoFisica,
            AreaDeConhecimento.LinguaInglesa,
            AreaDeConhecimento.Matematica,
            AreaDeConhecimento.CienciasDaNatureza,
            AreaDeConhecimento.Geografia,
            AreaDeConhecimento.Historia,
            AreaDeConhecimento.EnsinoReligioso
        }
    );

    public static readonly NivelDeEnsino EnsinoMedio = new NivelDeEnsino
    (
        "Ensino Médio",
        new AreaDeConhecimento[]
        {
            AreaDeConhecimento.LinguaPortuguesa,
            AreaDeConhecimento.LinguagensSuasTecnologias,
            AreaDeConhecimento.Matematica,
            AreaDeConhecimento.CienciasDaNatureza,
            AreaDeConhecimento.CienciasHumanas,
            AreaDeConhecimento.CienciasSociaisAplicadas
        }
    );

    public static readonly NivelDeEnsino EJA1 = new NivelDeEnsino
    (
        "EJA 1.º Segmento",
        new AreaDeConhecimento[]
        {
            AreaDeConhecimento.LinguaPortuguesa,
            AreaDeConhecimento.Matematica,
            AreaDeConhecimento.EstudosDaSociedadeDaNatureza
        }
    );

    public static readonly NivelDeEnsino EJA2 = new NivelDeEnsino
    (
        "EJA 2.º Segmento",
        new AreaDeConhecimento[]
        {
            AreaDeConhecimento.Artes,
            AreaDeConhecimento.EducacaoFisica,
            AreaDeConhecimento.Geografia,
            AreaDeConhecimento.Historia,
            AreaDeConhecimento.LinguaEstrangeira,
            AreaDeConhecimento.LinguaPortuguesa,
            AreaDeConhecimento.Matematica
        }
    );

    public static readonly NivelDeEnsino EnsinoSuperior = new NivelDeEnsino
    (
        "Ensino Superior",
        new AreaDeConhecimento[]
        {
            AreaDeConhecimento.CienciasExatasDaTerra,
            AreaDeConhecimento.CienciasBiologicas,
            AreaDeConhecimento.Engenharias,
            AreaDeConhecimento.CienciasDaSaude,
            AreaDeConhecimento.CienciasAgrarias,
            AreaDeConhecimento.CienciasSociaisAplicadas,
            AreaDeConhecimento.CienciasHumanas,
            AreaDeConhecimento.LinguagensSuasTecnologias,
            AreaDeConhecimento.Artes
        }
    );

    public readonly string nome;
    public readonly AreaDeConhecimento[] areasDeConhecimento;

    private NivelDeEnsino(string nome, AreaDeConhecimento[] areasDeConhecimento)
    {
        this.nome = nome;
        this.areasDeConhecimento = areasDeConhecimento;
    }

    public static NivelDeEnsino[] TodosOsNiveisDeEnsino()
    {
        NivelDeEnsino[] todosOsNiveisDeEnsino =
        {
            EducacaoInfantil,
            FundamentalAnosIniciais,
            FundamentalAnosFinais,
            EnsinoMedio,
            EJA1,
            EJA2,
            EnsinoSuperior
        };
        return todosOsNiveisDeEnsino;
    }

    public static NivelDeEnsino Get(string nome)
    {
        foreach (var nivelDeEnsino in TodosOsNiveisDeEnsino())
        {
            if (nome.Equals(nivelDeEnsino.nome))
                return nivelDeEnsino;
        }
        return null;
    }

    public override string ToString()
    {
        return nome;
    }
}