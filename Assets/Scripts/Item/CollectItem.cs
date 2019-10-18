using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItem : MonoBehaviour {

    public ItemName target;
    bool done = false;    

    public void addItem() {
        if (!done) {
            Player.Instance.Inventory.Add(target);
            done = true;
        }
    }
	
}
