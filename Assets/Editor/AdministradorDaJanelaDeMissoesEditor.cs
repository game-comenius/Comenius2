using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;
using GameComenius.Dialogo;

[CustomEditor(typeof(AdministradorDaJanelaDeMissoes))]
public class AdministradorDaJanelaDeMissoesEditor : Editor
{
    private AdministradorDaJanelaDeMissoes script;

    private void OnEnable()
    {
        // Pegar o objeto AdministradorDaJanelaDeMissoes que ta sendo editado
        script = (AdministradorDaJanelaDeMissoes)target;
    }

    public override void OnInspectorGUI()
    {
        // Tirar o comentário da linha abaixo se quiser que o Unity Inspector
        // mostre os campos/propriedades do script ItemSpriteDatabase do jeito
        // padrão, ou seja, como se não estivéssemos reescrevendo este jeito
        // base.OnInspectorGUI();

        serializedObject.Update();

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("== Objetivo ==");
        EditorGUILayout.BeginHorizontal();
        var adicionarMissao = script.Objetivo == AdministradorDaJanelaDeMissoes.ObjetivoDaAcao.Adicionar;
        adicionarMissao = EditorGUILayout.Toggle(adicionarMissao, "Radio");
        EditorGUILayout.LabelField("Adicionar Missão");
        if (adicionarMissao)
            script.Objetivo = AdministradorDaJanelaDeMissoes.ObjetivoDaAcao.Adicionar;

        // Para deixar os botões mais juntinhos, um espaço negativo entre eles
        GUILayout.Space(-70);

        var removerMissao = script.Objetivo == AdministradorDaJanelaDeMissoes.ObjetivoDaAcao.Remover;
        removerMissao = EditorGUILayout.Toggle(removerMissao, "Radio");
        EditorGUILayout.LabelField("Remover Missão");
        if (removerMissao)
            script.Objetivo = AdministradorDaJanelaDeMissoes.ObjetivoDaAcao.Remover;
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("== Missão ==");
        var quests = ManagerQuest.mainQuests.Union(ManagerQuest.sideQuests).ToArray();
        var questsDescriptions = new string[quests.Length];
        var questsIndexes = new int[quests.Length];
        for (var i = 0; i < quests.Length; i++)
        {
            questsIndexes[i] = quests[i].index;
            questsDescriptions[i] = quests[i].index + ": " + quests[i].description;
        }
        script.IndiceDaMissaoAlvo = EditorGUILayout.IntPopup(script.IndiceDaMissaoAlvo, questsDescriptions, questsIndexes);
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("== Quando ==");
        EditorGUILayout.BeginHorizontal();
        if (EditorGUILayout.Toggle(script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.InicioDaCena, "Radio", GUILayout.Width(20)))
            script.Momento = AdministradorDaJanelaDeMissoes.MomentoDaAcao.InicioDaCena;
        EditorGUILayout.LabelField("Início desta cena");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (EditorGUILayout.Toggle(script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposDialogo, "Radio", GUILayout.Width(20)))
            script.Momento = AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposDialogo;
        EditorGUILayout.LabelField("Após diálogo");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if (EditorGUILayout.Toggle(script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposResposta, "Radio", GUILayout.Width(20)))
            script.Momento = AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposResposta;
        EditorGUILayout.LabelField("Após resposta");
        EditorGUILayout.EndHorizontal();

        // Pedir um NpcDialogo se o momento de adicionar/remover é após um
        // diálogo daquela cena ou após uma resposta de desse diálogo
        if (script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposDialogo ||
            script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposResposta)
        {
            EditorGUILayout.Space();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Diálogo", GUILayout.Width(50));
            script.Dialogo = (NpcDialogo)EditorGUILayout.ObjectField(script.Dialogo, typeof(NpcDialogo), true);
            EditorGUILayout.EndHorizontal();
        }

        // Se o momento para adicionar/remover for após uma resposta, pedir uma
        if (script.Dialogo && script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposResposta)
        {
            EditorGUILayout.BeginHorizontal();

            var dialogos = new List<Dialogo>();
            dialogos.Add(script.Dialogo.dialogoPrincipal);
            foreach (var dialogo in script.Dialogo.dialogosSecundarios)
                dialogos.Add(dialogo);

            string[] respostas = null;
            foreach (var dialogo in dialogos)
            {
                foreach (var nodulo in dialogo.nodulos)
                {
                    if (nodulo.respostas.Count > 0)
                    {
                        // Pegar as respostas e sair desse grande comando for
                        respostas = nodulo.respostas.Select((resposta) => resposta.fala).ToArray();
                        break;
                    }
                }
            }
            if (respostas != null)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Resposta Alvo", GUILayout.Width(80));
                script.IndiceDaRespostaAlvo = EditorGUILayout.Popup(script.IndiceDaRespostaAlvo, respostas);
                EditorGUILayout.EndHorizontal();
            }
            else
            {
                EditorGUILayout.LabelField("Não há perguntas neste diálogo");
            }

            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }
}
