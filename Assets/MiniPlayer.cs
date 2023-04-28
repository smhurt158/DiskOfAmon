using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniPlayer : MonoBehaviour
{
    bool sceneLoaded = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (sceneLoaded)
            {
                SceneManager.UnloadSceneAsync("TestScene");
            }
            else
            {
                SceneManager.LoadScene("TestScene", LoadSceneMode.Additive);
            }
            sceneLoaded = !sceneLoaded;
        }
    }
}
