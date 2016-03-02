using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public int inventoryCount;
    public List<GameObject> items = new List<GameObject>();

    void Start()
    {
        inventoryCount = 1;
    }
}
