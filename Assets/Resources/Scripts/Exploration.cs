using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exploration : MonoBehaviour {

    private Mapping current_piece;
    private GameObject canvas;
    private Image fadePanel;
    public bool defeated;
    private bool transitioning;

	void Awake () {
        current_piece = GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece;
        fadePanel = GameObject.Find("fadePanel").GetComponent<Image>();
        canvas = GameObject.Find("FadeCanvas");
        defeated = false;
        transitioning = false;
        canvas.SetActive(false);
	}

	void Update ()
    {
        if (SceneManager.GetActiveScene().name == "Map")
            defeated = GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece.enemyDefeated;

        if (current_piece != GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece)
        {
            current_piece = GameObject.Find("GameManager").GetComponent<MapUpdater>().current_piece;
        }

        if (current_piece.enemyEncounter && !defeated && !transitioning)
        {
            transitioning = true;
            GameObject.Find("GameManager").GetComponent<TravelButtons>().transition = true;
            StartCoroutine(Encounter());
        }

	}

    private IEnumerator Encounter()
    {
        canvas.SetActive(true);
        fadePanel.CrossFadeAlpha(0, 0, true);
        fadePanel.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Combat");
        transitioning = false;
    }
}
