using System;
using System.Collections;
using UnityEngine;

public class CustomConfigSalaProfessores : MonoBehaviour {

    private string nomeObjetoProfessor;

    public static QuestClass questFalarComProfessor;
    public static QuestClass questFazerPlanejamento;
    public static QuestClass questIrParaAula;

    private void Awake()
    {
        // Definir que este é um jogo custom
        GameManager.IsCustomGame = true;

        // A sala dos professores do custom é baseada/cópia da sala da missão 1
        nomeObjetoProfessor = "JeanSalaProfessores";

        // Remover diálogos desnecessários que fazem parte da missão 1 do jogo
        // tradicional menos o diálogo do professor que será útil
        var npcDialogos = FindObjectsOfType<NpcDialogo>();
        for (var i = npcDialogos.Length - 1; i >= 0; i--)
        {
            var npcDialogo = npcDialogos[i];

            if (npcDialogo.name == nomeObjetoProfessor)
            {
                var prof = npcDialogo.gameObject;
                GetComponent<CustomProfessor>().ConfigurarProfessor(prof, CustomGameSettings.CurrentSettings);
                continue;
            }

            Destroy(npcDialogo);
        }

        // Apagar todas as quests da sala, menos a do professor pois o diálogo
        // dele depende de ele ter um componente QuestGuest
        var questGuests = FindObjectsOfType<QuestGuest>();
        for (var i = questGuests.Length - 1; i >= 0; i--)
        {
            var questGuest = questGuests[i];

            if (questGuest.name == nomeObjetoProfessor) continue;

            Destroy(questGuest);
        }

        // Remover todas as trocas de cursor, menos a do professor, a da
        // prancheta de planejamento e a da porta, estes 3 permanecerão úteis
        var dynamicCursors = FindObjectsOfType<DynamicCursor>();
        for (int i = dynamicCursors.Length - 1; i >= 0; i--)
        {
            var dynamicCursor = dynamicCursors[i];

            // Não retirar troca de cursor para objetos da UI
            var uiLayer = 5;
            if (dynamicCursor.gameObject.layer == uiLayer) return;

            bool casoEspecial = dynamicCursor.name == nomeObjetoProfessor ||
                dynamicCursor.GetComponent<PranchetaPlanejamento>() ||
                dynamicCursor.GetComponent<DoorTransition>();
            if (casoEspecial) continue;

            Destroy(dynamicCursor);
        }
    }

    private IEnumerator Start () {
        // Ativar o ConselheiroComenius
        ConselheiroComenius.Visivel = true;
        // Ativar janela de missões
        yield return new WaitUntil(() => ConselheiroComenius.JanelaMissoes != null);
        ConselheiroComenius.JanelaMissoes.Ativa = true;

        // Ativar o botão que abre o fichário
        FindObjectOfType<BotaoAbrirFichario>().Visivel = true;
        FindObjectOfType<BotaoAbrirFichario>().Ativo = true;

        // Remover ajuda da janela de missões se ela existir na cena
        var ajudaJanelaMissoes = FindObjectOfType<AjudaComeniusJanelaMissoes>();
        if (ajudaJanelaMissoes) Destroy(ajudaJanelaMissoes.gameObject);

        // Adicionar quest falar com o professor
        questFalarComProfessor = new QuestClass(1, "Falar com o professor", new DoQuest(), new int[]{}, "Fale com o professor");
        yield return new WaitUntil(() => ConselheiroComenius.JanelaMissoes != null);
        ConselheiroComenius.JanelaMissoes.AdicionarMissao(questFalarComProfessor);

        var dialogoProfessor = GameObject.Find(nomeObjetoProfessor).GetComponent<NpcDialogo>();
        Action funcaoRemoverMissaoFalarComProfessor = null;
        funcaoRemoverMissaoFalarComProfessor = () =>
        {
            ConselheiroComenius.JanelaMissoes.RemoverMissao(questFalarComProfessor);
            dialogoProfessor.OnEndDialogueEvent -= funcaoRemoverMissaoFalarComProfessor;
        };
        dialogoProfessor.OnEndDialogueEvent += funcaoRemoverMissaoFalarComProfessor;

        // Adicionar quest fazer planejamento
        questFazerPlanejamento = new QuestClass(2, "Fazer planejamento", new DoQuest(), new int[] { }, "Faça o planejamento");
        Action funcaoAdicionarMissaoFazerPlanejamento = null;
        funcaoAdicionarMissaoFazerPlanejamento = () =>
        {
            ConselheiroComenius.JanelaMissoes.AdicionarMissao(questFazerPlanejamento);
            dialogoProfessor.OnEndDialogueEvent -= funcaoAdicionarMissaoFazerPlanejamento;
        };
        dialogoProfessor.OnEndDialogueEvent += funcaoAdicionarMissaoFazerPlanejamento;

        var plan = FindObjectOfType<Planejamento>();
        Action funcaoRemoverMissaoFazerPlanejamento = null;
        funcaoRemoverMissaoFazerPlanejamento = () =>
        {
            ConselheiroComenius.JanelaMissoes.RemoverMissao(questFazerPlanejamento);
            plan.QuandoConfirmarPlanejamentoEvent -= funcaoRemoverMissaoFazerPlanejamento;
        };
        plan.QuandoConfirmarPlanejamentoEvent += funcaoRemoverMissaoFazerPlanejamento;

        // Adicionar quest ir para a aula
        questIrParaAula = new QuestClass(3, "Ir para a aula", new DoQuest(), new int[] { }, "Saia da sala dos professores", "Vá para a sala de aula");
        Action funcaoAdicionarMissaoIrParaAula = null;
        funcaoAdicionarMissaoIrParaAula = () =>
        {
            ConselheiroComenius.JanelaMissoes.AdicionarMissao(questIrParaAula);
            plan.QuandoConfirmarPlanejamentoEvent -= funcaoAdicionarMissaoIrParaAula;
        };
        plan.QuandoConfirmarPlanejamentoEvent += funcaoAdicionarMissaoIrParaAula;
    }
}
