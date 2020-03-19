using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AdministradorDaJanelaDeMissoes : MonoBehaviour {

    public enum ObjetivoDaAcao { Adicionar, Remover }
    public enum MomentoDaAcao { InicioDaCena, AposDialogo, AposResposta }

    public ObjetivoDaAcao Objetivo;

    public int IndiceDaMissaoAlvo;
    public int IndiceDaRespostaAlvo;

    public MomentoDaAcao Momento;

    public NpcDialogo Dialogo;


    private void Start()
    {
        // Pegar a quest com o índice = IndiceDaMissao
        var questArray = ManagerQuest.mainQuests.Union(ManagerQuest.sideQuests);
        var buscaQuest = questArray.Where((q) => q.index == IndiceDaMissaoAlvo);
        var quest = buscaQuest.FirstOrDefault();
        if (quest == null)
        {
            Debug.LogWarning("Quest não definida, defina uma quest para adicionar à janela de missões");
            return;
        }

        switch (Objetivo)
        {
            case ObjetivoDaAcao.Adicionar:
                StartCoroutine(AdicionarMissaoNaJanela(quest, Momento));
                break;
            case ObjetivoDaAcao.Remover:
                StartCoroutine(RemoverMissaoNaJanela(quest, Momento));
                break;
        }
    }

    private IEnumerator AdicionarMissaoNaJanela(QuestClass quest, MomentoDaAcao momento)
    {
        switch (momento)
        {
            case MomentoDaAcao.InicioDaCena:
                yield return new WaitUntil(() => ConselheiroComenius.JanelaMissoes != null);
                ConselheiroComenius.JanelaMissoes.AdicionarMissao(quest);
                break;
            case MomentoDaAcao.AposDialogo:
                if (!Dialogo)
                {
                    Debug.LogError("Diálogo == null. Impossível adicionar uma missão após um diálogo sem especificar o mesmo.");
                    break;
                }
                Action funcaoAdicionarMissao = null;
                funcaoAdicionarMissao = () =>
                {
                    ConselheiroComenius.JanelaMissoes.AdicionarMissao(quest);
                    Dialogo.OnEndDialogueEvent -= funcaoAdicionarMissao;
                };
                Dialogo.OnEndDialogueEvent += funcaoAdicionarMissao;
                break;
            case MomentoDaAcao.AposResposta:
                if (!Dialogo)
                {
                    Debug.LogError("Diálogo == null. Impossível adicionar uma missão após um diálogo sem especificar o mesmo.");
                    break;
                }
                Action<int> funcaoAdicionarMissaoAposResposta = null;
                funcaoAdicionarMissaoAposResposta = (respostaDoJogador) =>
                {
                    if (respostaDoJogador == IndiceDaRespostaAlvo)
                    {
                        ConselheiroComenius.JanelaMissoes.AdicionarMissao(quest);
                        Dialogo.QuandoEscolherRespostaEvent -= funcaoAdicionarMissaoAposResposta;
                    }
                };
                Dialogo.QuandoEscolherRespostaEvent += funcaoAdicionarMissaoAposResposta;
                break;
        }
    }

    private IEnumerator RemoverMissaoNaJanela(QuestClass quest, MomentoDaAcao momento)
    {
        switch (momento)
        {
            case MomentoDaAcao.InicioDaCena:
                yield return new WaitUntil(() => ConselheiroComenius.JanelaMissoes != null);
                ConselheiroComenius.JanelaMissoes.RemoverMissao(quest);
                break;
            case MomentoDaAcao.AposDialogo:
                if (!Dialogo)
                {
                    Debug.LogError("Diálogo == null. Impossível remover uma missão após um diálogo sem especificar o mesmo.");
                    break;
                }
                Action funcaoRemoverMissao = null;
                funcaoRemoverMissao = () =>
                {
                    ConselheiroComenius.JanelaMissoes.RemoverMissao(quest);
                    Dialogo.OnEndDialogueEvent -= funcaoRemoverMissao;
                };
                Dialogo.OnEndDialogueEvent += funcaoRemoverMissao;
                break;
            case MomentoDaAcao.AposResposta:
                if (!Dialogo)
                {
                    Debug.LogError("Diálogo == null. Impossível remover uma missão após um diálogo sem especificar o mesmo.");
                    break;
                }
                Action<int> funcaoRemoverMissaoAposResposta = null;
                funcaoRemoverMissaoAposResposta = (respostaDoJogador) =>
                {
                    if (respostaDoJogador == IndiceDaRespostaAlvo)
                    {
                        ConselheiroComenius.JanelaMissoes.RemoverMissao(quest);
                        Dialogo.QuandoEscolherRespostaEvent -= funcaoRemoverMissaoAposResposta;
                    }
                };
                Dialogo.QuandoEscolherRespostaEvent += funcaoRemoverMissaoAposResposta;
                break;
        }
    }
}
