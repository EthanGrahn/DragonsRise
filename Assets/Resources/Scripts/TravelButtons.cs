using UnityEngine;
using System.Collections;

public class TravelButtons : MonoBehaviour {

    private int current_index;
    private GameObject[] pieces;
    private Mapping current_piece;

    public void Begin()
    {
        pieces = GameObject.Find("GameManager").GetComponent<MapGeneration>().pieces;
        current_index = GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index;
        current_piece = pieces [current_index - 1].GetComponent<Mapping>();
    }

	public void Left()
    {
        if (current_piece.left != null)
        {
            current_piece = current_piece.left.GetComponent<Mapping>();
            current_index = current_piece.index;
            Adjust();
        }
    }

    public void Right()
    {
        if (current_piece.right != null)
        {
            current_piece = current_piece.right.GetComponent<Mapping>();
            current_index = current_piece.index;
            Adjust();
        }
    }

    public void Up()
    {
        if (current_piece.up != null)
        {
            current_piece = current_piece.up.GetComponent<Mapping>();
            current_index = current_piece.index;
            Adjust();
        }
    }

    public void Down()
    {
        if (current_piece.down != null)
        {
            Debug.Log("Check");
            current_piece = current_piece.down.GetComponent<Mapping>();
            current_index = current_piece.index;
            Adjust();
        }
    }

    private void Adjust()
    {   // Changes the current in the MapGenerator object so it can do its work
        GameObject.Find("GameManager").GetComponent<MapGeneration>().current_index = current_index;
        // Refreshes the MapUpdater to keep track of colors and backgrounds
        GameObject.Find("GameManager").GetComponent<MapUpdater>().Refresh();
    }
}
