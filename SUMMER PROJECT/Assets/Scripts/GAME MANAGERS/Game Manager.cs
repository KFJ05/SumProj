using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public string MainMenuName;

    public List<string> CompletedLevels = new List<string>();

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindAnyObjectByType<GameManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO GAME Manager Present");
            }

            return instance;
        }

    }

    private void Start()
    {
        SceneManager.LoadScene(MainMenuName, LoadSceneMode.Additive);
    }

    public void BacktomainMenu(string currentLevel)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentLevel);

        SceneManager.LoadScene(MainMenuName, LoadSceneMode.Additive);
    }
    
    public void LoadLevelFromMenu(string LevelName)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(MainMenuName);

        SceneManager.LoadScene(LevelName, LoadSceneMode.Additive);
    }

    public void loadNextLevel(string currentLevel, string NextLevel)
    {
        AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(currentLevel);

        SceneManager.LoadScene(NextLevel, LoadSceneMode.Additive);
    }



    public void EndGame()
    {
#if UNITY_STANDALONE
        Application.Quit();
#endif

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }


}
