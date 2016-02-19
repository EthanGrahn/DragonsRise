using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitch : MonoBehaviour {

    /*=======================================================
      Universal Scene Switcher
      Attach to empty game object
      Enter scene name to set scene that will be switched to
      =======================================================*/  

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Debug.Log("Escape");
        }
    }

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene changed to " + sceneName);
    }

    public void EndGame()
    {
        Application.Quit();
    }
}
