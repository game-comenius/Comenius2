using System;

[Serializable]
public sealed class AreaDeConhecimento
{
    public static readonly AreaDeConhecimento EuOutroNos = new AreaDeConhecimento("O eu, o outro e o nós");
    public static readonly AreaDeConhecimento CorpoGestosMovimentos = new AreaDeConhecimento("Corpo, gestos e movimentos");
    public static readonly AreaDeConhecimento TracosSonsCoresFormas = new AreaDeConhecimento("Traços, sons, cores e formas");
    public static readonly AreaDeConhecimento EscutaFalaPensamentoImaginacao = new AreaDeConhecimento("Escuta, fala, pensamento e imaginação");
    public static readonly AreaDeConhecimento EspacosTemposQuantidadesRelacoesTransformacoes = new AreaDeConhecimento("Espaços, tempos, quantidades, relações e transformações");

    public static readonly AreaDeConhecimento LinguaPortuguesa = new AreaDeConhecimento("Língua Portuguesa");
    public static readonly AreaDeConhecimento Artes = new AreaDeConhecimento("Artes");
    public static readonly AreaDeConhecimento EducacaoFisica = new AreaDeConhecimento("Educação Física");
    public static readonly AreaDeConhecimento Matematica = new AreaDeConhecimento("Matemática");
    public static readonly AreaDeConhecimento CienciasDaNatureza = new AreaDeConhecimento("Ciências da Natureza");
    public static readonly AreaDeConhecimento Geografia = new AreaDeConhecimento("Geografia");
    public static readonly AreaDeConhecimento Historia = new AreaDeConhecimento("História");
    public static readonly AreaDeConhecimento EnsinoReligioso = new AreaDeConhecimento("Ensino Religioso");

    public static readonly AreaDeConhecimento LinguaInglesa = new AreaDeConhecimento("Língua Inglesa");

    public static readonly AreaDeConhecimento LinguagensSuasTecnologias = new AreaDeConhecimento("Linguagens e suas Tecnologias");
    public static readonly AreaDeConhecimento CienciasHumanas = new AreaDeConhecimento("Ciências Humanas");
    public static readonly AreaDeConhecimento CienciasSociaisAplicadas = new AreaDeConhecimento("Ciências Sociais Aplicadas");

    public static readonly AreaDeConhecimento EstudosDaSociedadeDaNatureza = new AreaDeConhecimento("Estudos da Sociedade e da Natureza");

    public static readonly AreaDeConhecimento LinguaEstrangeira = new AreaDeConhecimento("Língua Estrangeira");

    public static readonly AreaDeConhecimento CienciasExatasDaTerra = new AreaDeConhecimento("Ciências Exatas e da Terra");
    public static readonly AreaDeConhecimento CienciasBiologicas = new AreaDeConhecimento("Ciências Biológicas");
    public static readonly AreaDeConhecimento Engenharias = new AreaDeConhecimento("Engenharias");
    public static readonly AreaDeConhecimento CienciasDaSaude = new AreaDeConhecimento("Ciências da Saúde");
    public static readonly AreaDeConhecimento CienciasAgrarias = new AreaDeConhecimento("Ciências Agrárias");


    public readonly string nome;


    private AreaDeConhecimento(string nome)
    {
        this.nome = nome;
    }

    public static AreaDeConhecimento[] TodasAsAreasDeConhecimento()
    {
        AreaDeConhecimento[] areas =
        {
            EuOutroNos,
            CorpoGestosMovimentos,
            TracosSonsCoresFormas,
            EscutaFalaPensamentoImaginacao,
            EspacosTemposQuantidadesRelacoesTransformacoes,

            LinguaPortuguesa,
            Artes,
            EducacaoFisica,
            Matematica,
            CienciasDaNatureza,
            Geografia,
            Historia,
            EnsinoReligioso,

            LinguaInglesa,
            LinguagensSuasTecnologias,
            CienciasHumanas,
            CienciasSociaisAplicadas,

            EstudosDaSociedadeDaNatureza,

            LinguaEstrangeira,

            CienciasExatasDaTerra,
            CienciasBiologicas,
            Engenharias,
            CienciasDaSaude,
            CienciasAgrarias
        };
        return areas;
    }

    public static AreaDeConhecimento Get(string nome)
    {
        foreach (var area in TodasAsAreasDeConhecimento())
        {
            if (nome.Equals(area.nome))
                return area;
        }
        return null;
    }

    public override string ToString()
    {
        return nome;
    }
}