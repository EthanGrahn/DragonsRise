using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour {


	void Start () {
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        SceneManager.LoadScene("MainMenu");
	}
}
