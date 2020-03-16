using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.Linq;

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
        script.IndiceDaMissao = EditorGUILayout.IntPopup(script.IndiceDaMissao, questsDescriptions, questsIndexes);
        EditorGUILayout.Space();


        EditorGUILayout.LabelField("== Quando ==");
        EditorGUILayout.BeginHorizontal();
        if (EditorGUILayout.Toggle(script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.InicioDaCena, "Radio"))
            script.Momento = AdministradorDaJanelaDeMissoes.MomentoDaAcao.InicioDaCena;
        EditorGUILayout.LabelField("Início desta cena");

        // Para deixar os botões mais juntinhos, um espaço negativo entre eles
        GUILayout.Space(-70);

        if (EditorGUILayout.Toggle(script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposDialogo, "Radio"))
            script.Momento = AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposDialogo;
        EditorGUILayout.LabelField("Após diálogo");
        EditorGUILayout.EndHorizontal();

        // Pedir um NpcDialogo se o momento de adicionar/remover é ou antes ou
        // após um diálogo daquela cena
        if (script.Momento == AdministradorDaJanelaDeMissoes.MomentoDaAcao.AposDialogo)
        {
            EditorGUILayout.Space();
            script.Dialogo = (NpcDialogo)EditorGUILayout.ObjectField(script.Dialogo, typeof(NpcDialogo), true);
        }

        EditorGUILayout.Space();

        serializedObject.ApplyModifiedProperties();
    }
}
