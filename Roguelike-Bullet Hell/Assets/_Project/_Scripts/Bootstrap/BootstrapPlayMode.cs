using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

[InitializeOnLoad]
public static class BootstrapPlayMode
{
    private const string BootstrapScene = "Assets/_Project/Scenes/Bootstrap.unity";
    private const string EditorPrefsKey = "PlayModeScene";

    static BootstrapPlayMode()
    {
        EditorApplication.playModeStateChanged += OnPlayModeChanged;
    }

    private static void OnPlayModeChanged(PlayModeStateChange state)
    {
        switch(state)
        {
            case PlayModeStateChange.ExitingEditMode:
                {
                    Scene activeScene = SceneManager.GetActiveScene();

                    if (activeScene.path == BootstrapScene)
                        return;

                    EditorPrefs.SetString(EditorPrefsKey, activeScene.path);

                    if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                    {
                        EditorApplication.isPlaying = false;
                        return;
                    }

                    EditorSceneManager.OpenScene(BootstrapScene);
                    break;
                }

            case PlayModeStateChange.EnteredEditMode:
                {
                    string scene = EditorPrefs.GetString(EditorPrefsKey, string.Empty);

                    if (!string.IsNullOrEmpty(scene))
                    {
                        EditorSceneManager.OpenScene(scene);
                        EditorPrefs.DeleteKey(EditorPrefsKey);
                    }

                    break;
                }
        }
    }

    public static string StartupScene => EditorPrefs.GetString(EditorPrefsKey, string.Empty);
}
