using UnityEngine;
using System.Collections;

public class MapUpdater : MonoBehaviour {

    private int current_index;
    private GameObject[] pieces;
    public Mapping current_piece;
    private SpriteRenderer current_piece_img;
    private SpriteRenderer background;
    public bool started = false;
    private GameObject leftArrow, rightArrow, upArrow, downArrow;

	public void Begin () 
    {
        started = true;
        pieces = GameObject.Find("GameManager").GetComponent<MapGeneration>().pieces;
        current_index = GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index;
        current_piece = pieces [current_index - 1].GetComponent<Mapping>();
        current_piece_img = pieces [current_index - 1].GetComponent<SpriteRenderer>();
        background = GameObject.Find("Background").GetComponent<SpriteRenderer>();
        leftArrow = GameObject.Find("Left_btn");
        rightArrow = GameObject.Find("Right_btn");
        upArrow = GameObject.Find("Up_btn");
        downArrow = GameObject.Find("Down_btn");
    }
	
	void Update () 
    {
        if (started)
        {
            background.sprite = current_piece.GetComponent<Mapping>().background_img;	

            ColorUpdate();
            ArrowUpdate();
        }
    }

    private void ColorUpdate()
    {   // Sets the highlighting on the map tile
        current_piece_img.color = new Color(0, 0, 0, 1f);
        current_piece_img.color = Color.gray;
    }

    private void ArrowUpdate()
    {
        GameObject left = current_piece.GetComponent<Mapping>().left;
        GameObject right = current_piece.GetComponent<Mapping>().right;
        GameObject up = current_piece.GetComponent<Mapping>().up;
        GameObject down = current_piece.GetComponent<Mapping>().down;

        if (left == null)
            leftArrow.SetActive(false);
        else
            leftArrow.SetActive(true);

        if (right == null)
            rightArrow.SetActive(false);
        else
            rightArrow.SetActive(true);

        if (up == null)
            upArrow.SetActive(false);
        else
            upArrow.SetActive(true);

        if (down == null)
            downArrow.SetActive(false);
        else
            downArrow.SetActive(true);
    }

    public void Defeat()
    {
        current_piece.enemyDefeated = true;
    }

    public void Refresh()
    {   // Operates similarly to Begin()
        current_index = GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index;
        current_piece = pieces [current_index - 1].GetComponent<Mapping>();
        current_piece_img.color = Color.white; // Resets the highlight color of the map tile
        current_piece_img = pieces [current_index - 1].GetComponent<SpriteRenderer>();
    }
}
