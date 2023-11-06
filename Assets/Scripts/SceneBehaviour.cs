using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    [SerializeField] float LoadLevelDelay = 1.0f;   
    public static SceneBehaviour Instance;
     private void Awake()
    {
        // Set the Instance to this instance
        Instance = this;
    }

    public IEnumerator LoadNextScene()
    {
        yield return new WaitForSecondsRealtime(LoadLevelDelay);

        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void LoadStartingScene()
    {
        SceneManager.LoadScene(1);
        //FindObjectOfType<GameSession>()
    }
}
