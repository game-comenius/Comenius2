using UnityEngine;

public class CustomConfigSalaProfessores : MonoBehaviour {

    private void Awake()
    {
        var nomeDoProfessorAntigo = "JeanSalaProfessores";

        // Remover diálogos desnecessários que fazem parte da missão 1 do jogo
        // tradicional menos o diálogo do professor que será útil
        var npcDialogos = FindObjectsOfType<NpcDialogo>();
        for (var i = npcDialogos.Length - 1; i >= 0; i--)
        {
            var npcDialogo = npcDialogos[i];

            if (npcDialogo.name == nomeDoProfessorAntigo)
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

            if (questGuest.name == nomeDoProfessorAntigo) continue;

            Destroy(questGuest);
        }

        // Remover todas as trocas de cursor, menos a do professor, a da
        // prancheta de planejamento e a da porta, estes 3 permanecerão úteis
        var dynamicCursors = FindObjectsOfType<DynamicCursor>();
        for (int i = dynamicCursors.Length - 1; i >= 0; i--)
        {
            var dynamicCursor = dynamicCursors[i];

            bool casoEspecial = dynamicCursor.name == nomeDoProfessorAntigo ||
                dynamicCursor.GetComponent<PranchetaPlanejamento>() ||
                dynamicCursor.GetComponent<DoorTransition>();
            if (casoEspecial) continue;

            Destroy(dynamicCursor);
        }
    }

    void Start () {
        // Remover ajuda da janela de missões se ela existir na cena
        var ajudaJanelaMissoes = FindObjectOfType<AjudaComeniusJanelaMissoes>();
        if (ajudaJanelaMissoes) Destroy(ajudaJanelaMissoes.gameObject);
	}
}
