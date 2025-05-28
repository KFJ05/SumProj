using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string CurrentLevel;
    public string NextLevel;

    public AudioClip LevelMusic;

    public bool LoopMusic;

    public bool PlayMusic;

    public bool StopMusicOnLeave;

    private void Awake()
    {
        if(PlayMusic)
        {
            GameManager.Instance.PlayMusic(LevelMusic, LoopMusic);
        }
        if(!PlayMusic)
        {
            if(GameManager.Instance.GetMusic() == null)
            {
                GameManager.Instance.PlayMusic(LevelMusic, LoopMusic);
            }
        }
    }

    public void BackToMenu()
    {
        
        GameManager.Instance.StopMusic();
        
        GameManager.Instance.BacktomainMenu(CurrentLevel);

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RESETALL();
        }

        if (PartManager.Instance != null)
        {
            PartManager.Instance.ResetParts();
        }
    }
    public void LoadNextLevel()
    {
        if (StopMusicOnLeave)
        {
            GameManager.Instance.StopMusic();
        }
        GameManager.Instance.loadNextLevel(CurrentLevel, NextLevel);

        if (EnemyManager.Instance != null)
        {
            EnemyManager.Instance.RESETALL();
        }

        if (PartManager.Instance != null)
        {
            PartManager.Instance.ResetParts();
        }
    }






}
