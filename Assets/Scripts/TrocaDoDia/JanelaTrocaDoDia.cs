using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JanelaTrocaDoDia : MonoBehaviour {

    [SerializeField]
    private GameObject slotMidia1;
    [SerializeField]
    private GameObject slotMidia2;
    [SerializeField]
    private GameObject slotMidia3;
    // Adicionar os 3 objetos acima neste array para resumir o código
    private GameObject[] slotsMidia;

    [SerializeField]
    private Sprite spriteEstrelaVazia;

    [SerializeField]
    private TextMeshProUGUI textoMidiasColetadas;

    private CanvasGroup canvasGroup;

    // Use this for initialization
    void Start () {
        slotsMidia = new GameObject[3] { slotMidia1, slotMidia2, slotMidia3 };

        canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
	}

    public void Esconder()
    {
        // Mais tarde, mudar para uma transição mais suave
        canvasGroup.alpha = 0;
    }

    public void Mostrar()
    {
        // Mais tarde, mudar para uma transição mais suave
        canvasGroup.alpha = 0.9f;

        Configurar();
    }

    private void Configurar()
    {
        var lurdinha = Player.Instance;
        var historicoMissao = lurdinha.MissionHistory[lurdinha.missionID];

        // Configurar o título da janela para a missão atual
        var tituloDaJanela = GetComponentInChildren<TextMeshProUGUI>();
        tituloDaJanela.text = "MÍDIAS SELECIONADAS PARA O PROFESSOR ";
        switch (lurdinha.missionID)
        {
            case 0: tituloDaJanela.text += "JEAN"; break;
            case 1: tituloDaJanela.text += "VLADMIR"; break;
            case 2: tituloDaJanela.text += "PAULINO"; break;
            case 3: tituloDaJanela.text += "CELESTINO"; break;
        }

        // Configurar as mídias nos slots com suas respectivas pontuações
        for (int i = 0; i < slotsMidia.Length; i++)
        {
            var midia = historicoMissao.chosenMedia[i];
            var pontos = historicoMissao.points[i];
            var slot = slotsMidia[i];
            DefinirMidiaNoSlot(midia, slot);
            DefinirPontosNoSlot(pontos, slot);
        }

        // Configurar texto com o número de mídias no inventário
        var disponiveis = GameManager.MidiasDisponiveisNaMissao1;
        var inventario = lurdinha.Inventory;
        var coletadas = 0;
        foreach (var midia in disponiveis)
            if (inventario.Contains(midia)) coletadas++;
        textoMidiasColetadas.text = "MÍDIAS NO INVENTÁRIO = ";
        textoMidiasColetadas.text += coletadas + "/" + disponiveis.Length;
    }

    private void DefinirMidiaNoSlot(ItemName midia, GameObject slot)
    {
        var imageMidia = slot.transform.GetChild(0).GetComponent<Image>();
        imageMidia.enabled = true;
        imageMidia.sprite = ItemSpriteDatabase.GetSpriteOf(midia);
        imageMidia.preserveAspect = true;
    }

    private void DefinirPontosNoSlot(double pontos, GameObject slot)
    {
        var filaDeEstrelas = slot.transform.GetChild(1);
        var estrelas = filaDeEstrelas.GetComponentsInChildren<Image>();

        var i = estrelas.Length - 1;
        // Pontuações possíveis: 10, 8, 7 e 0
        if (pontos < 10)
            estrelas[i--].sprite = spriteEstrelaVazia;
        if (pontos < 8)
            estrelas[i--].sprite = spriteEstrelaVazia;
        if (pontos < 7)
            estrelas[i].sprite = spriteEstrelaVazia;
    }
}
