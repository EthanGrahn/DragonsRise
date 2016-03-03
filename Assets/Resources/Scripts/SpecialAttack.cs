using UnityEngine;
using System.Collections;

public class SpecialAttack : MonoBehaviour {

    public Rigidbody2D fireBall;
    public float projSpeed;

    void Start () {
	
	}
	

	public void Fire () {
        Rigidbody2D fireInstance = Instantiate(fireBall, transform.position, Quaternion.Euler(new Vector3(0, 0, 0))) as Rigidbody2D;
        fireInstance.velocity = new Vector2(projSpeed, 0);
	}
}
