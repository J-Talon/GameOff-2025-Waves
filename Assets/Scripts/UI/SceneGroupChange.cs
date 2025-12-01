using UnityEngine;
public class SceneGroupChange : MonoBehaviour
{
    void OnPauseGame()
    {

    }
    public void OnStartGame()
    {
        StartCoroutine(Main.Instance.LoadGameplay());
    }
    public void OnEndGame()
    {
        StartCoroutine(Main.Instance.LoadGameover());
    }
    public void OnReturnToMainMenu()
    {
        StartCoroutine(Main.Instance.LoadMainMenu());
    }
}
