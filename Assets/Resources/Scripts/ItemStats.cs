using UnityEngine;
using System.Collections;

public class ItemStats : MonoBehaviour {

    public string itemName;
    public string description;
    public Sprite itemSprite;
    public int cooldown;
    public int quantity;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
