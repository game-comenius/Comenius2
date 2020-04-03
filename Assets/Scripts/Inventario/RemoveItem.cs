using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveItem : MonoBehaviour {

    public ItemName target;
    bool done = false;

    public void deleteItem()
    {
        if (!done)
        {
            Player.Instance.Inventory.Remove(target);
            done = true;
        }
    }
}
