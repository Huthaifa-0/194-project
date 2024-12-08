using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controllers : MonoBehaviour
{
    public void Play()
    {
        SceneController.LoadScene(3);
    }
    public void Restart()
    {
        SceneController.Restart();
    }
    public void SceneLoad(int sceneIndex)
    {
        SceneController.LoadScene(sceneIndex);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
