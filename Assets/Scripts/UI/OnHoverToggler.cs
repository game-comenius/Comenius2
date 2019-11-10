using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnHoverToggler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField]
    private GameObject objectThatWillBeToggled;

    public void OnPointerEnter(PointerEventData eventData)
    {
        objectThatWillBeToggled.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        objectThatWillBeToggled.SetActive(false);
    }

    // Use this for initialization
    void Start () {
        objectThatWillBeToggled.SetActive(false);
	}


}
