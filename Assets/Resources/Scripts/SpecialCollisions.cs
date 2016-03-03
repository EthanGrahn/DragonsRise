using UnityEngine;
using System.Collections;

public class SpecialCollisions : MonoBehaviour {
    


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyCollider")
        { 
            Destroy(this.gameObject);
            GameObject.Find("GameManager").GetComponent<Combat>().SpecialHit();
        }
    }
}
