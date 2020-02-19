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

    private int[] ordensMissao;

    [SerializeField]
    private CorpoMissaoJanelaMissoes prefabCorpoMissao;
    private CorpoMissaoJanelaMissoes corpoMissaoAtivo;

    
    private void Awake()
    {
        titulo = GetComponentInChildren<TextMeshProUGUI>();
    }

    public bool Configurar(QuestGroup quest)
    {
        this.titulo.text = quest.name;

        var quantidadeMaxDescricoes = 3;

        if (quest.indexes.Length > quantidadeMaxDescricoes)
        {
            Debug.Log("Erro: tentando adicionar missão com mais de 3 descrições!");
            return false;
        }

        List<int> descriptions = new List<int>();


        for (int i = 0; i < quest.indexes.Length; i++) 
        {
            if (ManagerQuest.VerifyQuestIsAvailable(quest.indexes[i]))
            {
                descriptions.Add(quest.indexes[i]);
            }
        }

        if (descriptions.Count!= 0)
        {
            ordensMissao = descriptions.ToArray();
            return true;
        }

        return false;
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

            abertoPeloMenosUmaVez = true;
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

    private void OnDestroy()
    {
        if (corpoMissaoAtivo != null)
        {
            Destroy(corpoMissaoAtivo.gameObject);
        }
    }
}
