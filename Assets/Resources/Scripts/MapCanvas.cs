using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MapCanvas : MonoBehaviour {

    public bool generated;
    public GameObject map;
    public GameObject[] pieces = new GameObject[36];
    public Mapping[] savedPieces = new Mapping[36];
    public int current_index;
    public bool defeated;

    void Start()
    {
        generated = false;
        map = GameObject.Find("MiniMapCanvas");
        map.SetActive(true);
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Map")
            map.SetActive(false);

        if (SceneManager.GetActiveScene().name == "Map" && Input.GetKeyUp("space"))
        {
            if (map.activeSelf)
                map.SetActive(false);
            else
                map.SetActive(true);
        }

        if (DefeatCheck())
        {
            GameObject.Find("GameManager").GetComponent<MapUpdater>().Defeat();
            defeated = false;
        }

        if (SceneManager.GetActiveScene().name == "Map" && generated)
        {
            current_index = GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index;
            defeated = GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece.enemyDefeated;
        }

        if (SceneManager.GetActiveScene().name == "MainMenu" && generated)
        {
            generated = false;
            current_index = 1;

            for (int i = 0; i < pieces.Length; i++)
                pieces [i].SetActive(true);
        }
    }

    private bool DefeatCheck()
    {
        bool check = true;

        if (!defeated || SceneManager.GetActiveScene().name != "Map" || GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece.enemyEncounter == false)
            check = false;

        return check;
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

    public void ResetConnections()
    {
        Mapping mapping = null;

        for (int i = 0; i < pieces.Length; i++)
        {
            mapping = pieces [i].GetComponent<Mapping>();

            mapping.left = savedPieces[i].left;
            mapping.right = savedPieces[i].right;
            mapping.up = savedPieces[i].up;
            mapping.down = savedPieces[i].down;
        }
    }
}
