using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class SceneHelper
{
    [SerializeField] List<string> scenes = new List<string>();
    public SceneHelper() { }
    public SceneHelper(List<string> scenes)
    {
        this.scenes = new List<string>(scenes);
    }
    public IEnumerator LoadScenes()
    {
        Debug.Log("Starting to load scenes...");
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
        Debug.Log("Finished loading scenes...");
    }
    public IEnumerator UnloadScenes()
    {
        Debug.Log("Starting to unload scenes...");
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
        Debug.Log("Finished unloading scenes...");
    }
    public void SetScenes(List<string> newScenes)
    {
        this.scenes = new List<string>(newScenes);
    }
    public List<string> GetScenes()
    {
        return this.scenes;
    }
    public int GetSceneCount() { return scenes.Count; }
}
