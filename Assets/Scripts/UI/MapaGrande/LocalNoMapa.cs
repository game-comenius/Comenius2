using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class LocalNoMapa : MonoBehaviour, IPointerClickHandler {

    public string NomeDaCena { get; set; }

    private FolhaMapaGrande mapa;

    // Use this for initialization
    void Start () {
        mapa = GetComponentInParent<FolhaMapaGrande>();
	}

    public void OnPointerClick(PointerEventData eventData)
    {
        mapa.Teletransportar(this);
    }
}
