﻿using System;

[Serializable]
public sealed class NivelDeEnsino
{
    public static readonly NivelDeEnsino EducacaoInfantil = new NivelDeEnsino
    (
        0,
        "Educação Infantil",
        new int[]
        {
            AreaDeConhecimento.EuOutroNos.valor,
            AreaDeConhecimento.CorpoGestosMovimentos.valor,
            AreaDeConhecimento.TracosSonsCoresFormas.valor,
            AreaDeConhecimento.EscutaFalaPensamentoImaginacao.valor,
            AreaDeConhecimento.EspacosTemposQuantidadesRelacoesTransformacoes.valor
        }
    );

    public static readonly NivelDeEnsino FundamentalAnosIniciais = new NivelDeEnsino
    (
        1,
        "Fundamental Anos Iniciais",
        new int[]
        {
            AreaDeConhecimento.LinguaPortuguesa.valor,
            AreaDeConhecimento.Artes.valor,
            AreaDeConhecimento.EducacaoFisica.valor,
            AreaDeConhecimento.Matematica.valor,
            AreaDeConhecimento.CienciasDaNatureza.valor,
            AreaDeConhecimento.Geografia.valor,
            AreaDeConhecimento.Historia.valor,
            AreaDeConhecimento.EnsinoReligioso.valor
        }
    );

    public static readonly NivelDeEnsino FundamentalAnosFinais = new NivelDeEnsino
    (
        2,
        "Fundamental Anos Finais",
        new int[]
        {
            AreaDeConhecimento.LinguaPortuguesa.valor,
            AreaDeConhecimento.Artes.valor,
            AreaDeConhecimento.EducacaoFisica.valor,
            AreaDeConhecimento.LinguaInglesa.valor,
            AreaDeConhecimento.Matematica.valor,
            AreaDeConhecimento.CienciasDaNatureza.valor,
            AreaDeConhecimento.Geografia.valor,
            AreaDeConhecimento.Historia.valor,
            AreaDeConhecimento.EnsinoReligioso.valor
        }
    );

    public static readonly NivelDeEnsino EnsinoMedio = new NivelDeEnsino
    (
        3,
        "Ensino Médio",
        new int[]
        {
            AreaDeConhecimento.LinguaPortuguesa.valor,
            AreaDeConhecimento.LinguisticaLetrasArtes.valor,
            AreaDeConhecimento.Matematica.valor,
            AreaDeConhecimento.CienciasDaNatureza.valor,
            AreaDeConhecimento.CienciasHumanas.valor,
            AreaDeConhecimento.CienciasSociaisAplicadas.valor
        }
    );

    public static readonly NivelDeEnsino EJA1 = new NivelDeEnsino
    (
        4,
        "EJA 1.º Segmento",
        new int[]
        {
            AreaDeConhecimento.LinguaPortuguesa.valor,
            AreaDeConhecimento.Matematica.valor,
            AreaDeConhecimento.EstudosDaSociedadeDaNatureza.valor
        }
    );

    public static readonly NivelDeEnsino EJA2 = new NivelDeEnsino
    (
        5,
        "EJA 2.º Segmento",
        new int[]
        {
            AreaDeConhecimento.Artes.valor,
            AreaDeConhecimento.EducacaoFisica.valor,
            AreaDeConhecimento.Geografia.valor,
            AreaDeConhecimento.Historia.valor,
            AreaDeConhecimento.LinguaEstrangeira.valor,
            AreaDeConhecimento.LinguaPortuguesa.valor,
            AreaDeConhecimento.Matematica.valor
        }
    );

    public static readonly NivelDeEnsino EnsinoSuperior = new NivelDeEnsino
    (
        6,
        "Ensino Superior",
        new int[]
        {
            AreaDeConhecimento.CienciasExatasDaTerra.valor,
            AreaDeConhecimento.CienciasBiologicas.valor,
            AreaDeConhecimento.Engenharias.valor,
            AreaDeConhecimento.CienciasDaSaude.valor,
            AreaDeConhecimento.CienciasAgrarias.valor,
            AreaDeConhecimento.CienciasSociaisAplicadas.valor,
            AreaDeConhecimento.CienciasHumanas.valor,
            AreaDeConhecimento.LinguisticaLetrasArtes.valor,
        }
    );

    public readonly int valor;
    public readonly string nome;
    private readonly int[] areasDeConhecimento;
    public AreaDeConhecimento[] AreasDeConhecimento
    {
        get
        {
            var length = areasDeConhecimento.Length;
            var returnValues = new AreaDeConhecimento[length];
            for (int i = 0; i < length; i++)
            {
                returnValues[i] = AreaDeConhecimento.Get(areasDeConhecimento[i]);
            }
            return returnValues;
        }
    }

    private NivelDeEnsino(int valor, string nome, int[] areasDeConhecimento)
    {
        this.valor = valor;
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

    public static NivelDeEnsino Get(int valor)
    {
        foreach (var nivelDeEnsino in TodosOsNiveisDeEnsino())
        {
            if (valor == nivelDeEnsino.valor)
                return nivelDeEnsino;
        }
        return null;
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