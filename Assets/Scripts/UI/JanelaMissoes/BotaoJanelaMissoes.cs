using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BotaoJanelaMissoes : MonoBehaviour, IPointerClickHandler {

    private JanelaMissoes janelaMissoes;

    // Use this for initialization
    void Start () {
        janelaMissoes = transform.GetComponentInParent<JanelaMissoes>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        janelaMissoes.Toggle();
        GetComponent<RectTransform>().Rotate(Vector3.forward, 180);
    }
}
