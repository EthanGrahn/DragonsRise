using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {

    // 5 x 5 map
    //testing changes

    private GameObject[] pieces = new GameObject[36];

	// Begins Generation
	void Start ()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;

        // Stores all map pieces and initializes bool array to false
        for (int i = 0; i < pieces.Length; i++)
            pieces [i] = GameObject.Find("Map_" + (i + 1));

        // Scramble the array for indexing purposes
        Scramble();

        // Sets the index and where each direction leads
        Generate();
    }

    private void Generate()
    {
        // Sets the index of the map pieces in the scrambled array
        for (int i = 0; i < pieces.Length; i++)
            pieces [i].GetComponent<Mapping>().index = i + 1;

        // Displays the map pieces
        Place();

        // Sets the corresponding scene of the map piece ===============================================================================
        for (int i = 0; i < pieces.Length; i++)
        {
            switch (pieces [i].GetComponent<Mapping>().index)
            {
                case 1:
                    pieces [i].GetComponent<Mapping>().scene = "Hub";
                    break;
                case 36:
                    pieces [i].GetComponent<Mapping>().scene = "Last_Room";
                    break;
            }
        }
    }

    private void Scramble()
    {
        for (int t = 0; t < pieces.Length; t++ )
        {
            GameObject tmp = pieces[t];
            int r = Random.Range(t, pieces.Length);
            pieces[t] = pieces[r];
            pieces[r] = tmp;
        }
    }

    private void Place()
    {
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces [i].GetComponent<Mapping>().index == 1)
                pieces [i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/MapPieces/Map_hub");
            else
                pieces [i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/MapPieces/Map");
        }
    }
}
