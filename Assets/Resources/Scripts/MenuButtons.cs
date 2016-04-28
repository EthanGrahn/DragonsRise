using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {

    private Text txtStart;
    private Text txtExit;
    private int index;
    public Material orange;
    public Material white;
    private SceneSwitch sceneSwitch;

	void Start () {
        txtStart = GameObject.Find("txtStart").GetComponent<Text>();
        txtExit = GameObject.Find("txtExit").GetComponent<Text>();
        sceneSwitch = GameObject.Find("SceneManager").GetComponent<SceneSwitch>();
        index = 1;
        txtStart.material = orange;
    }
	

	void Update () {
        if (Input.GetKeyUp("up") && index == 2)
        {
            index--;
            txtExit.material = white;
            txtStart.material = orange;
        }
        else if (Input.GetKeyUp("down") && index == 1)
        {
            index++;
            txtStart.material = white;
            txtExit.material = orange;
        }

        if (Input.GetKeyUp("x"))
        {
            if (index == 1)
                sceneSwitch.SwitchScene("Map");
            else
                sceneSwitch.EndGame();
        }
	}
}
