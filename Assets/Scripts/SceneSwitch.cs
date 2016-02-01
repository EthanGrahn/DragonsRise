using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneSwitch : MonoBehaviour {

    /*=======================================================
      Universal Scene Switcher
      Attach to empty game object
      Enter scene name to set scene that will be switched to
      =======================================================*/  

    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Debug.Log("Scene changed to " + sceneName);
    }
}
