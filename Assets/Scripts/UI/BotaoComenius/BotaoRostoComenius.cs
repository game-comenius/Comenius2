using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class BotaoRostoComenius : MonoBehaviour, IPointerClickHandler {

    private ConselheiroComenius conselheiroComenius;

    // Use this for initialization
    void Start () {
        conselheiroComenius = GetComponentInParent<ConselheiroComenius>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        conselheiroComenius.HandlePointerClick();
    }
}
