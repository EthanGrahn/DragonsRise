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

        int rand = 0;
        for (int i = 0; i < pieces.Length; i++)
        {
            // Sets background image
            switch (pieces [i].GetComponent<Mapping>().index)
            {
                case 1:
                    pieces [i].GetComponent<Mapping>().background_img = Resources.Load<Sprite>("Sprites/Backgrounds/Exploration_bg");
                    break;
                default:
                    pieces[i].GetComponent<Mapping>().background_img = Resources.Load<Sprite>("Sprites/Backgrounds/Exploration_bg");
                    break;
            }

            // Generates enemy encounters randomly
            rand = Random.Range(1, 4);
            if (rand == 1 && pieces[i].GetComponent<Mapping>().index != 1)
                pieces[i].GetComponent<Mapping>().enemyEncounter = true;
            else
                pieces[i].GetComponent<Mapping>().enemyEncounter = false;

            pieces [i].GetComponent<Mapping>().enemyDefeated = false;
        }

        bool check = false;

        for (int i = 0; i < pieces.Length; i++)
        {
            check = DeleteCheck(pieces [i]);

            if (check)
            {
                // Deactivates random tiles
                rand = Random.Range(1, 36);
                if (rand <= 12 && pieces[i].GetComponent<Mapping>().index != 1 && pieces[i].GetComponent<Mapping>().index != 36)
                {
                    pieces[i].SetActive(false);
                    UpdateConnections(pieces[i].GetComponent<Mapping>().left);
                    UpdateConnections(pieces[i].GetComponent<Mapping>().right);
                    UpdateConnections(pieces[i].GetComponent<Mapping>().up);
                    UpdateConnections(pieces[i].GetComponent<Mapping>().down);
                }
            }
            check = false;
        }

        // Sets tile piece adapting to missing tiles
        for (int i = 0; i < pieces.Length; i++)
            UpdateConnections(pieces [i]);

        // Displays the map pieces
        Place();
    }

    private bool DeleteCheck(GameObject piece)
    {
        bool good = true;
        Mapping mapping = piece.GetComponent<Mapping>();
        GameObject next = null;
        GameObject nextLeft = null;
        GameObject nextRight = null;
        GameObject nextUp = null;
        GameObject nextDown = null;
        
        if (mapping.left != null)
            next = mapping.left;
        else if (mapping.right != null)
            next = mapping.right;
        else if(mapping.up != null)
            next = mapping.up;
        else if (mapping.down != null)
            next = mapping.down;

        if (mapping.left != null)
            nextLeft = mapping.left;

        if (mapping.right != null)
            nextRight = mapping.right;

        if (mapping.up != null)
            nextUp = mapping.up;

        if (mapping.down != null)
            nextDown = mapping.down;

        if (next != null)
        {
            piece.GetComponent<Mapping>().marked = true;
            FloodFill(next);

            if (nextLeft != null && !nextLeft.GetComponent<Mapping>().marked)
                good = false;
            if (nextRight != null && !nextRight.GetComponent<Mapping>().marked)
                good = false;
            if (nextUp != null && !nextUp.GetComponent<Mapping>().marked)
                good = false;
            if (nextDown != null && !nextDown.GetComponent<Mapping>().marked)
                good = false;

            for (int i = 0; i < pieces.Length; i++)
            {
                pieces[i].GetComponent<Mapping>().marked = false;
            }
        }

        return good;
    }

    private void FloodFill(GameObject piece)
    {
        Mapping mapping = piece.GetComponent<Mapping>();

        if (mapping.marked)
            return;

        GameObject nextLeft = null;
        GameObject nextRight = null;
        GameObject nextUp = null;
        GameObject nextDown = null;

        mapping.marked = true;
        nextLeft = mapping.left;
        nextRight = mapping.right;
        nextUp = mapping.up;
        nextDown = mapping.down;

        if (nextLeft != null)
            FloodFill(nextLeft);

        if (nextRight != null)
            FloodFill(nextRight);

        if (nextUp != null)
            FloodFill(nextUp);

        if (nextDown != null)
            FloodFill(nextDown);

        return;
    }

    private void UpdateConnections(GameObject piece)
    {
        if (piece == null)
            return;

        Mapping mapping = piece.GetComponent<Mapping>();

        if (mapping.down != null && mapping.down.activeSelf == false)
            mapping.down = null;
        if (mapping.up != null && mapping.up.activeSelf == false)
            mapping.up = null;
        if (mapping.right != null && mapping.right.activeSelf == false)
            mapping.right = null;
        if (mapping.left != null && mapping.left.activeSelf == false)
            mapping.left = null;
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
                pieces [i].GetComponent<SpriteRenderer>().sprite = TileSelector(pieces[i]);
                pieces [i].GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
            }
        }
    }

    private Sprite TileSelector( GameObject piece )
    {
        Sprite tile;
        bool up = true, right = true, down = true, left = true;
        Mapping mapping = piece.GetComponent<Mapping>();

        if (mapping.down == null)
            down = false;
        if (mapping.up == null)
            up = false;
        if (mapping.right == null)
            right = false;
        if (mapping.left == null)
            left = false;

        if (!up && right && down && left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_LRD");
        else if (up && !right && down && left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_LUD");
        else if (up && right && !down && left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_LUR");
        else if (up && right && down && !left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_URD");
        else if (!up && !right && down && left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_LD");
        else if (!up && right && !down && left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_RL");
        else if (!up && right && down && !left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_RD");
        else if (up && !right && !down && left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_LU");
        else if (up && !right && down && !left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_UD");
        else if (up && right && !down && !left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_RU");
        else if (up && !right && !down && !left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_U");
        else if (!up && right && !down && !left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_R");
        else if (!up && !right && down && !left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_D");
        else if (!up && !right && !down && left)
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_L");
        else if (!up && !right && !down && !left)
            tile = null;
        else
            tile = Resources.Load<Sprite>("Sprites/MapPieces/Map_default");
        
        return tile;
    }

    private void Done()
    {
        GameObject.Find("GameManager").GetComponent<MapUpdater>().Begin();
        GameObject.Find("GameManager").GetComponent<TravelButtons>().Begin();
    }
}
