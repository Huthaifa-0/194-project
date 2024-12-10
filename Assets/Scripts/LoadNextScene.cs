using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour
{
    
    // Start is called before the first frame update
    public void LoadUnderwater() { //Host
        SceneManager.LoadScene("UnderwaterScene");
    }

    public void LoadBeach() { //Client
        SceneManager.LoadScene("BeachScene");
    }
}
