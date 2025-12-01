using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    // public Player player;

    [SerializeField] private GameData gameData;
    [SerializeField] private SceneHelper persistent;
    [SerializeField] private SceneHelper mainmenu;
    [SerializeField] private SceneHelper gameplay;
    [SerializeField] private SceneHelper gameover;
    [SerializeField] private SceneHelper current;
    [SerializeField] private SceneHelper previous;
    [SerializeField] int managerCount = 0;
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
        previous = new SceneHelper();
        StartCoroutine(persistent.LoadScenes());
        StartCoroutine(LoadMainMenu());
    }

    public IEnumerator LoadMainMenu()
    {
        yield return StartCoroutine(mainmenu.LoadScenes());
        yield return null;
        previous.SetScenes(current.GetScenes());
        current.SetScenes(mainmenuScenes);
        if (previous.GetSceneCount() > 0) yield return StartCoroutine(previous.UnloadScenes());
    }
    public IEnumerator LoadGameplay()
    {
        yield return null;
        yield return null;
        yield return null;
        yield return StartCoroutine(gameplay.LoadScenes());
        yield return null;
        yield return StartCoroutine(gameData.InitializeNewData());
        yield return null;
        yield return null;
        yield return null;
        yield return null;
        ProvideDataToManagers();
        previous.SetScenes(current.GetScenes());
        current.SetScenes(gameplayScenes);
        if (previous.GetSceneCount() > 0) yield return StartCoroutine(previous.UnloadScenes());
    }
    public IEnumerator LoadGameover()
    {
        yield return StartCoroutine(gameover.LoadScenes());
        yield return null;
        previous.SetScenes(current.GetScenes());
        current.SetScenes(gameoverScenes);
        if (previous.GetSceneCount() > 0) yield return StartCoroutine(previous.UnloadScenes());
    }
    public void AddManager(IManager manager)
    {
        managers.Add(manager);
        managerCount++;
        Debug.Log($"Added {manager}...");
    }
    public void RemoveManager(IManager manager)
    {
        managers.Remove(manager);
        managerCount--;
        Debug.Log($"Removed {manager}...");
    }
    void ProvideDataToManager(IDataUser user)
    {
        user.SetData(gameData);
    }
    public void ProvideDataToManagers()
    {
        foreach (var user in managers.OfType<IDataUser>())
        {
            user.SetData(gameData);
        }
    }
    public void SetGameData(GameData data)
    {
        gameData = data;
    }
}
public interface IWorker
{
    void Initialize();
}
public interface IManager
{
    public void Register(IWorker worker);
    public void Deregister(IWorker worker);
}