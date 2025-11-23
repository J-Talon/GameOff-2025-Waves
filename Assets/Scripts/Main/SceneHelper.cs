using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper
{
    List<string> scenes = new List<string>();
    public SceneHelper() { }
    public SceneHelper(List<string> scenes)
    {
        this.scenes = scenes;
    }
    public IEnumerator LoadScenes()
    {
        foreach (var scene in scenes)
        {
            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(scene, LoadSceneMode.Additive);
            if (asyncOp == null)
            {
                Debug.LogError($"Scene {scene} failed to start loading!");
                continue;
            }
            while (!asyncOp.isDone) yield return null;
        }
    }
    public IEnumerator UnloadScenes()
    {
        foreach (var scene in scenes)
        {
            AsyncOperation asyncOp = SceneManager.UnloadSceneAsync(scene);
            if (asyncOp == null)
            {
                Debug.LogError($"Scene {scene} failed to start unloading!");
                continue;
            }
            while (!asyncOp.isDone) yield return null;
        }
    }
    public void SetScenes(List<string> newScenes)
    {
        scenes = newScenes;
    }
    public int GetSceneCount() { return scenes.Count; }
}
