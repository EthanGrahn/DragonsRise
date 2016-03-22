using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapCanvas : MonoBehaviour {

    public bool generated;
    private GameObject map;
    public GameObject[] pieces = new GameObject[36];
    public int current_index;

    void Start()
    {
        generated = false;
        map = GameObject.Find("MiniMapCanvas");
        map.SetActive(false);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Map")
            map.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Map")
            current_index = GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index;
    }

    public void Check()
    {
        if (SceneManager.GetActiveScene().name == "Map")
        {
            map.SetActive(true);
            map.GetComponent<Canvas>().worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            if (pieces[0] == null)
                pieces = GameObject.Find("GameManager").GetComponent<MapGeneration>().pieces;
        }
        else
            map.SetActive(false);
    }
}
