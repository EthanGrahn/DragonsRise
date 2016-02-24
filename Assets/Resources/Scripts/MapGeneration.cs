using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {

    // 6 x 6 map

    public GameObject[] pieces = new GameObject[36];
    public int current_index;

	// Begins Generation
	void Start ()
    {
        Random.seed = (int)System.DateTime.Now.Ticks;
        current_index = 1;

        // Stores all map pieces
        for (int i = 0; i < pieces.Length; i++)
            pieces [i] = GameObject.Find("Map_" + (i + 1));

        // Scramble the array for indexing purposes
        Scramble();

        // Sets the index and where each direction leads
        Generate();

        // Calls the start of the MapUpdater
        Done();
    }

    private void Generate()
    {
        // Sets the index of the map pieces in the scrambled array
        for (int i = 0; i < pieces.Length; i++)
            pieces [i].GetComponent<Mapping>().index = i + 1;

        // Displays the map pieces
        Place();

        // Sets the corresponding backround of the map piece ===============================================================================
        for (int i = 0; i < pieces.Length; i++)
        {
            switch (pieces [i].GetComponent<Mapping>().index)
            {
                case 1:
                    pieces [i].GetComponent<Mapping>().background_img = Resources.Load<Sprite>("Sprites/Backgrounds/Background_hub");
                    break;
                case 36:
                    
                    break;
            }
        }
    }

    private void Scramble()
    {   // Used to randomize the indexes by scrambling the array
        for (int t = 0; t < pieces.Length; t++ )
        {
            GameObject tmp = pieces[t];
            int r = Random.Range(t, pieces.Length);
            pieces[t] = pieces[r];
            pieces[r] = tmp;
        }
    }

    private void Place()
    {   // Sets the sprites for the map overlay
        for (int i = 0; i < pieces.Length; i++)
        {
            if (pieces [i].GetComponent<Mapping>().index == 1)
                pieces [i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/MapPieces/Map_hub");
            else
                pieces [i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/MapPieces/Map");
        }
    }

    private void Done()
    {
        GameObject.Find("MapUpdater").GetComponent<MapUpdater>().Begin();
    }
}
