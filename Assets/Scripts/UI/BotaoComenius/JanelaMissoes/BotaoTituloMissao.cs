using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotaoTituloMissao : MonoBehaviour, IPointerClickHandler {

    private bool aberto;
    [HideInInspector]
    public bool abertoPeloMenosUmaVez;

    private TextMeshProUGUI titulo;

    private string[] passosDaMissao;

    [SerializeField]
    private CorpoMissaoJanelaMissoes prefabCorpoMissao;
    private CorpoMissaoJanelaMissoes corpoMissaoAtivo;

    
    private void Awake()
    {
        titulo = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Keyword params permite passar um número variável de passos da missão
    // Por exemplo, pode chamar this.Configurar("a", "b", "c", "d") ou
    // this.Configurar("a", "b", "c") ou this.Configurar("a", "b")
    public void Configurar(QuestClass quest)
    {
        this.titulo.text = quest.description;

        var quantidadeMaxPassos = 3;
        if (quest.passosDaQuest.Length > quantidadeMaxPassos)
            Debug.LogWarning("Tentando adicionar missão com mais de 3 descrições");

        var quantidadeDePassos = Math.Min(quest.passosDaQuest.Length, 3);
        passosDaMissao = new string[quantidadeDePassos];
        for (var i = 0; i < quantidadeDePassos; i++)
            passosDaMissao[i] = quest.passosDaQuest[i];
    }

    public void OnPointerClick(PointerEventData eventData) { Toggle(); }

    public void Toggle()
    {
        if (!aberto)
        {
            var siblingIndex = transform.GetSiblingIndex();
            corpoMissaoAtivo = Instantiate(prefabCorpoMissao);

            foreach (var passo in passosDaMissao)
                corpoMissaoAtivo.AdicionarOrdemMissao(passo);

            corpoMissaoAtivo.transform.SetParent(this.transform.parent);
            corpoMissaoAtivo.transform.SetSiblingIndex(siblingIndex + 1);
            corpoMissaoAtivo.transform.localScale = Vector3.one;

            abertoPeloMenosUmaVez = true;
        }
        else
        {
            Destroy(corpoMissaoAtivo.gameObject);
        }

        // Girar a seta que abre este botão
        var setaVermelha = transform.GetChild(transform.childCount - 1);
        setaVermelha.GetComponent<RectTransform>().Rotate(Vector3.forward, 180);

        aberto = !aberto;
    }

    // Quando este botão é destruído, ele leva junto o corpo da missão com seus
    // passos da missão
    private void OnDestroy()
    {
        if (corpoMissaoAtivo != null)
        {
            Destroy(corpoMissaoAtivo.gameObject);
        }
    }
}
