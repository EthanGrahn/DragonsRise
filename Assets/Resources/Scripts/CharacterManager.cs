using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CharacterManager : MonoBehaviour {

    private Canvas canvas;
    private GameObject character;
    private bool completed;


    void Start()
    {
        character = GameObject.Find("Character");
        canvas = GameObject.Find("buttonCanvas").GetComponentInChildren<Canvas>();
        character.SetActive(false);
        completed = false;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Combat" && !completed)
        {
            character.SetActive(true);
            canvas.worldCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
            GameObject.Find("GameManager").GetComponent<Combat>().CombatStart();
            GameObject.Find("Enemy").GetComponent<EnemyAI>().EnemyStart();
            completed = true;
        } 
        else if (SceneManager.GetActiveScene().name != "Combat" && character.activeSelf)
        {
            character.SetActive(false);
            completed = false;
        }
    }
}
