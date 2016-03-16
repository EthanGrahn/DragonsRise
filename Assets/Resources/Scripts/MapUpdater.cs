using UnityEngine;
using System.Collections;

public class MapUpdater : MonoBehaviour {

    private int current_index;
    private GameObject[] pieces;
    private Mapping current_piece;
    private SpriteRenderer current_piece_img;
    private SpriteRenderer background;
    private bool started = false;

	public void Begin () 
    {
        started = true;
        pieces = GameObject.Find("GameManager").GetComponent<MapGeneration>().pieces;
        current_index = GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index;
        current_piece = pieces [current_index - 1].GetComponent<Mapping>();
        current_piece_img = pieces [current_index - 1].GetComponent<SpriteRenderer>();
        background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
	}
	
	void Update () 
    {
        if (started) // only starts the update after the Map Generation is complete
        {
            switch (current_index)
            {
                case 1:
                    background.sprite = Resources.Load<Sprite>("Sprites/Backgrounds/Exploration_bg");
                    break;
                default:
                    background.sprite = Resources.Load<Sprite>("Sprites/Backgrounds/Exploration_bg");
                    break;
            }  	

            ColorUpdate();
        }
    }

    private void ColorUpdate()
    {   // Sets the highlighting on the map tile
        current_piece_img.color = new Color(0, 0, 0, 1f);
        current_piece_img.color = Color.gray;
    }

    public void Refresh()
    {   // Operates similarly to Begin()
        current_index = GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index;
        current_piece = pieces [current_index - 1].GetComponent<Mapping>();
        current_piece_img.color = Color.white; // Resets the highlight color of the map tile
        current_piece_img = pieces [current_index - 1].GetComponent<SpriteRenderer>();
    }
}
