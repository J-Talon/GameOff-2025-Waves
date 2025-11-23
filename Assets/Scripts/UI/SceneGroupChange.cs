using UnityEngine;
public class SceneGroupChange : MonoBehaviour
{
    void OnPauseGame()
    {

    }
    public void OnStartGame()
    {
        Main.Instance.LoadGameplay();
    }
    public void OnEndGame()
    {
        Main.Instance.LoadGameover();
    }
    public void OnReturnToMainMenu()
    {
        Main.Instance.LoadMainMenu();
    }
}
