using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Camera mainCamera;

    [SerializeField]
    List<string> persistentScenes = new List<string>();
    [SerializeField]
    List<string> mainmenuScenes = new List<string>();
    [SerializeField]
    List<string> gameplayScenes = new List<string>();
    [SerializeField]
    List<string> gameoverScenes = new List<string>();

    List<IManager> managers = new List<IManager>();

    public static Main Instance;

    private SceneHelper persistent;
    private SceneHelper mainmenu;
    private SceneHelper gameplay;
    private SceneHelper gameover;
    private SceneHelper current;
    private void Awake()
    {
        if (Instance == null) Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        persistent = new SceneHelper(persistentScenes);
        mainmenu = new SceneHelper(mainmenuScenes);
        gameplay = new SceneHelper(gameplayScenes);
        gameover = new SceneHelper(gameoverScenes);
        current = new SceneHelper();
        StartCoroutine(persistent.LoadScenes());
        LoadMainMenu();
    }

    public void LoadMainMenu()
    {
        if (current.GetSceneCount() > 0) StartCoroutine(current.UnloadScenes());
        StartCoroutine(mainmenu.LoadScenes());
        current.SetScenes(mainmenuScenes);
    }
    public void LoadGameplay()
    {
        if (current.GetSceneCount() > 0) StartCoroutine(current.UnloadScenes());
        StartCoroutine(gameplay.LoadScenes());
        current.SetScenes(gameplayScenes);
    }
    public void LoadGameover()
    {
        if (current.GetSceneCount() > 0) StartCoroutine(current.UnloadScenes());
        StartCoroutine(gameover.LoadScenes());
        current.SetScenes(gameoverScenes);
    }

    public void AddManager(IManager manager)
    {
        managers.Add(manager);
        Debug.Log($"Added {manager}...");
    }
    public void RemoveManager(IManager manager)
    {
        managers.Remove(manager);
        Debug.Log($"Removed {manager}...");
    }
    void OnDestroy()
    {

    }
}
public interface IWorker
{
    void Initialize(IManager manager);  //Give worker a reference to relevant manager to subscribe callbacks to manager events
}
public interface IManager
{
    public void Register(IWorker worker);
    public void Deregister(IWorker worker);
}