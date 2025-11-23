using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBootstrapper : MonoBehaviour
{
    [SerializeField]
    List<GameObject> prefabs;
    Scene bootstrapperScene;
    void Awake()
    {
        bootstrapperScene = gameObject.scene;
        //SceneManager.activeSceneChanged += OnActiveSceneChangedbySceneBootstrapper;
    }
    void Start()
    {
        //SceneManager.SetActiveScene(bootstrapperScene);
        StartCoroutine(InstantiateAllPrefabsSequentially());
    }

    IEnumerator InstantiateAllPrefabsSequentially()
    {
        foreach (var prefab in prefabs)
        {
            var go = Instantiate(prefab);
            SceneManager.MoveGameObjectToScene(go, bootstrapperScene);
            yield return null;
        }
        yield return null;
    }
    void OnActiveSceneChangedbySceneBootstrapper(Scene current, Scene next)
    {
        Debug.Log($"{this} has changed the active scene from {current.name} to {next.name}");
    }
}
