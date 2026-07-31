using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bootstrap : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        string scene = BootstrapPlayMode.StartupScene;

        if(!string.IsNullOrEmpty(scene))
        {
            SceneManager.LoadScene(scene);
            return;
        }
#endif

        SceneManager.LoadScene("Main Menu");
    }
}