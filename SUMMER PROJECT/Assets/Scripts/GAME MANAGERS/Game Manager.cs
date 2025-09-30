using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public string MainMenuName;

    public List<string> CompletedLevels = new List<string>();

    public AudioSource MasterMusicSource;

    public AudioSource MasterSoundSourceBullets, MasterSoundSourceExplosions, MasterSoundSourceVoices;

    public bool NormalGameCompleted;

    float mouseSensitivity = 500f;

    float SoundVolume = .25f;


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

    public void PlayMusic(AudioClip clip, bool LoopMusic)
    {
        MasterMusicSource.clip = clip;
        MasterMusicSource.loop = LoopMusic;
        MasterMusicSource.Play();

    }
    public void StopMusic()
    {
        MasterMusicSource.Pause();
        MasterMusicSource.clip = null;
        MasterMusicSource.loop = false;
    }

    public void PlayBulletSound(AudioClip clip)
    {
        MasterSoundSourceBullets.clip = clip;
        MasterSoundSourceBullets.Play();

    }
    public void StopBulletSound()
    {
        MasterSoundSourceBullets.Pause();
        MasterSoundSourceBullets.clip = null;
    }

    public void ChangeMusicVolume(float NewVolume)
    {
        MasterMusicSource.volume = NewVolume;
    }

    public float GetMusicVolume()
    {
        return MasterMusicSource.volume;
    }

    public void ChangSoundVolume(float NewVolume)
    {
        SoundVolume = NewVolume;
    }

    public float GetSoundVolume()
    {
        return SoundVolume;
    }

    public float GetmouseSensitivity()
    {
        return mouseSensitivity;
    }
    public void ChangemouseSensitivity(float NewSensitivity)
    {
        mouseSensitivity = NewSensitivity;
    }


    public AudioClip GetMusic()
    {
        return MasterMusicSource.clip;
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
