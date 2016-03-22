using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Preload : MonoBehaviour {


	void Awake () {
        DontDestroyOnLoad(GameObject.Find("Inventory"));
        DontDestroyOnLoad(GameObject.Find("MiniMapCanvas"));
        DontDestroyOnLoad(GameObject.Find("MapManager"));
        SceneManager.LoadScene("MainMenu");
	}
}
