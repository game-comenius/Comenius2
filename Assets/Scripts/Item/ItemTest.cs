using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemTest : MonoBehaviour {

    public Item2 item;

    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    //throw new System.NotImplementedException();
    //    Player.Instance.Inventory.Add(item.ItemName);
    //    Debug.Log("o vício do debug");
    //}

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonUp(0) && Input.mousePosition.x <= this.transform.position.x)
        {
            Player.Instance.Inventory.Add(item.ItemName);
            Debug.Log("foi?");
        }
    }


    
}
