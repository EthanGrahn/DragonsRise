using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour {

	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        SceneManager.LoadScene("MainMenu");
	}
}
