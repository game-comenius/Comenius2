using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CorpoMissaoJanelaMissoes : MonoBehaviour {

    [SerializeField]
    private GameObject prefabOrdemMissao;

    private int ordens;
    private float alturaOrdem;

    private void Awake()
    {
        // Coletar altura do prefab de uma ordem da missão, é necessário
        // instanciar para coletar infelizmente
        var ordem = Instantiate(prefabOrdemMissao);
        var rectTransform = ordem.GetComponent<RectTransform>();
        alturaOrdem = rectTransform.sizeDelta.y;
        Destroy(ordem);
    }

    public void AdicionarOrdemMissao(string textoDaOrdem)
    {
        var ordem = Instantiate(prefabOrdemMissao);

        // Tamanho cresce de acordo com o número de ordens desta missão
        ordens++;
        var r = GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(r.sizeDelta.x, (ordens * alturaOrdem));

        var icone = ordem.transform.GetChild(0);
        var minWidth = icone.GetComponent<Image>().sprite.bounds.size.x * 100;
        icone.GetComponent<LayoutElement>().minWidth = minWidth;

        var textObject = ordem.GetComponentInChildren<TextMeshProUGUI>();
        textObject.text = textoDaOrdem;
        ordem.transform.SetParent(this.transform);
        ordem.transform.localScale = Vector3.one;
    }
}
