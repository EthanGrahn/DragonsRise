using UnityEngine;
using System.Collections;

public class TravelButtons : MonoBehaviour {

    private int current_index;
    private GameObject[] pieces;

    void Start()
    {
        pieces = GameObject.Find("MapGenerator").GetComponent<MapGeneration>().pieces;
    }

    void Update()
    {
        current_index = GameObject.Find("MapGenerator").GetComponent<MapGeneration>().current_index;
    }

	public void Left()
    {
        
    }

    public void Right()
    {

    }

    public void Up()
    {

    }

    public void Down()
    {

    }
}
