using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGeneration : MonoBehaviour {

    // 6 x 6 map

    public GameObject[] pieces = new GameObject[36];
    public int current_index;

	// Begins Generation
	void Awake ()
    {
        GameObject.Find("MapManager").GetComponent<MapCanvas>().Check();

        if (GameObject.Find("MapManager").GetComponent<MapCanvas>().generated == false)
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

            GameObject.Find("MapManager").GetComponent<MapCanvas>().generated = true;
        }
        else
        {
            pieces = GameObject.Find("MapManager").GetComponent<MapCanvas>().pieces;
            current_index = GameObject.Find("MapManager").GetComponent<MapCanvas>().current_index;
        }

        Done();
    }

    private void Generate()
    {
        // Sets the index of the map pieces in the scrambled array
        for (int i = 0; i < pieces.Length; i++)
            pieces [i].GetComponent<Mapping>().index = i + 1;

        // Displays the map pieces
        Place();

        int rand = 0;
        for (int i = 0; i < pieces.Length; i++)
        {
            switch (pieces [i].GetComponent<Mapping>().index)
            {
                case 1:
                    pieces [i].GetComponent<Mapping>().background_img = Resources.Load<Sprite>("Sprites/Backgrounds/Exploration_bg");
                    break;
                default:
                    pieces[i].GetComponent<Mapping>().background_img = Resources.Load<Sprite>("Sprites/Backgrounds/Exploration_bg");
                    break;
            }

            rand = Random.Range(1, 4);
            if (rand == 4)
                pieces[i].GetComponent<Mapping>().enemyEncounter = true;
            else
                pieces[i].GetComponent<Mapping>().enemyEncounter = false;
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
            {
                pieces [i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/MapPieces/Map_hub");
                pieces [i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f);
            } else
            {
                pieces [i].GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/MapPieces/Map_default");
                pieces [i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    private void Done()
    {
        GameObject.Find("GameManager").GetComponent<MapUpdater>().Begin();
        GameObject.Find("GameManager").GetComponent<TravelButtons>().Begin();
    }
}
