using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exploration : MonoBehaviour {

    private Mapping current_piece;
    private GameObject canvas;
    private Image fadePanel;
    public bool onTile;
    public bool defeated;

	void Awake () {
        current_piece = GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece;
        fadePanel = GameObject.Find("fadePanel").GetComponent<Image>();
        canvas = GameObject.Find("FadeCanvas");
        onTile = false;
        defeated = false;
        canvas.SetActive(false);
	}

	void Update () {
        if (current_piece.enemyEncounter && !onTile && !defeated)
        {
            onTile = true;
            GameObject.Find("GameManager").GetComponent<TravelButtons>().transition = true;
            StartCoroutine(Encounter());
        }

        if (current_piece != GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece)
        {
            current_piece = GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece;
            onTile = false;
        }

        if (SceneManager.GetActiveScene().name == "Map")
            defeated = GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece.enemyDefeated;
	}

    private IEnumerator Encounter()
    {
        canvas.SetActive(true);
        fadePanel.CrossFadeAlpha(0, 0, false);
        fadePanel.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Combat");
    }
}
