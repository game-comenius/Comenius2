using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoTituloMissao : MonoBehaviour, IPointerClickHandler {

    private bool aberto;

    private string[] descricoesDaMissao { get; set; }

    private TextMeshProUGUI titulo;

    [SerializeField]
    private CorpoMissaoJanelaMissoes corpoMissaoPrefab;
    private CorpoMissaoJanelaMissoes corpoMissao;

    
    private void Awake()
    {
        titulo = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Configurar(string titulo, string[] descricoes)
    {
        this.titulo.text = titulo;

        var quantidadeMaxDescricoes = 3;
        if (descricoes.Length > quantidadeMaxDescricoes)
        {
            Debug.Log("Erro: tentando adicionar missão com mais de 3 descrições!");
            return;
        }
        this.descricoesDaMissao = descricoes;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Toggle();
    }

    public void Toggle()
    {
        if (!aberto)
        {
            var siblingIndex = transform.GetSiblingIndex();
            corpoMissao = Instantiate(corpoMissaoPrefab);
            corpoMissao.transform.SetParent(this.transform.parent);
            corpoMissao.transform.SetSiblingIndex(siblingIndex + 1);
            corpoMissao.transform.localScale = Vector3.one;
        }
        else
        {
            Destroy(corpoMissao.gameObject);
        }

        // Girar a seta vermelha
        var setaVermelha = transform.GetChild(transform.childCount - 1);
        setaVermelha.GetComponent<RectTransform>().Rotate(Vector3.forward, 180);

        aberto = !aberto;
    }
}
