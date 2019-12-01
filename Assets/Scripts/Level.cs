using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    // Automatic change to another scene
    [SerializeField] float delayUntilNextScene = 2.0f;
    [SerializeField] string nextSceneName = "";

    //--------------------------------------------------------------------------
    void Start()
    {
        if (nextSceneName.Length > 0 && delayUntilNextScene > 0.0f)
        {
            StartCoroutine(WaitThenLoadScene(delayUntilNextScene, nextSceneName));
        }
    }

    //--------------------------------------------------------------------------
    public void LoadGetReady()
    {
        SceneManager.LoadScene("GetReady");
    }

    //--------------------------------------------------------------------------
    public void LoadGameOver()
    {
        StartCoroutine(WaitThenLoadScene(delayUntilNextScene, "GameOver"));
    }

    //--------------------------------------------------------------------------
    public void QuitGame()
    {
        Application.Quit();
    }

    //--------------------------------------------------------------------------
    IEnumerator WaitThenLoadScene(float delayS, string scene)
    {
        yield return new WaitForSeconds(delayS);
        SceneManager.LoadScene(scene);
    }

    //--------------------------------------------------------------------------
}
