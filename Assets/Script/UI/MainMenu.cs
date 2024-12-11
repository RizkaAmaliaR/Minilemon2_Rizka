using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] Image loadingBar;

    public void StartGame()
    {
        StartCoroutine(LoadGame());
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadGame()
    {
        AsyncOperation loadingOperation = SceneManager.LoadSceneAsync("Scenes/Map1/Map1");

        while (!loadingOperation.isDone)
        {
            loadingBar.fillAmount = Mathf.Clamp01(loadingOperation.progress);
            yield return null;
        }
    }
}
