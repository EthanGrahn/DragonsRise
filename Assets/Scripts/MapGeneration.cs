using UnityEngine;
using System.Collections;

public class MapGeneration : MonoBehaviour {

    // Height and width of map grid
    // Will possibly be a static value later
    public int height;
    public int width;

	// Begins Generation
	void Start ()
    {
        bool[,] map = new bool[height, width];
    }
}
