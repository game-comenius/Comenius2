using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotaoTituloMissao : MonoBehaviour, IPointerClickHandler {

    private bool aberto;

    private TextMeshProUGUI titulo;

    private string[] ordensMissao;

    [SerializeField]
    private CorpoMissaoJanelaMissoes prefabCorpoMissao;
    private CorpoMissaoJanelaMissoes corpoMissaoAtivo;

    
    private void Awake()
    {
        titulo = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Configurar(string tituloMissao, string[] ordensMissao)
    {
        this.titulo.text = tituloMissao;

        var quantidadeMaxDescricoes = 3;
        if (ordensMissao.Length > quantidadeMaxDescricoes)
        {
            Debug.Log("Erro: tentando adicionar missão com mais de 3 descrições!");
            return;
        }
        this.ordensMissao = ordensMissao;
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
            corpoMissaoAtivo = Instantiate(prefabCorpoMissao);

            foreach (var ordem in ordensMissao)
                corpoMissaoAtivo.AdicionarOrdemMissao(ordem);

            corpoMissaoAtivo.transform.SetParent(this.transform.parent);
            corpoMissaoAtivo.transform.SetSiblingIndex(siblingIndex + 1);
            corpoMissaoAtivo.transform.localScale = Vector3.one;
        }
        else
        {
            Destroy(corpoMissaoAtivo.gameObject);
        }

        // Girar a seta vermelha
        var setaVermelha = transform.GetChild(transform.childCount - 1);
        setaVermelha.GetComponent<RectTransform>().Rotate(Vector3.forward, 180);

        aberto = !aberto;
    }
}
