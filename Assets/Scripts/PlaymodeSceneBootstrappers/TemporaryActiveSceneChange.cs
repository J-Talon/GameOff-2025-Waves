using UnityEngine;
using UnityEngine.SceneManagement;

public class TemporaryActiveSceneChange : MonoBehaviour
{
    Scene previous;
    Scene temporary;
    void Start()
    {
        temporary = SceneManager.GetSceneByName("Temporary");
        previous = SceneManager.GetActiveScene();
        SceneManager.SetActiveScene(temporary);
    }
    private void OnDestroy()
    {
        SceneManager.SetActiveScene(previous);
    }
}
