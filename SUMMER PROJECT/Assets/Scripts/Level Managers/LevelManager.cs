using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public string CurrentLevel;
    public string NextLevel;
    public void BackToMenu()
    {
        GameManager.Instance.BacktomainMenu(CurrentLevel);
    }
    public void LoadNextLevel()
    {
        GameManager.Instance.loadNextLevel(CurrentLevel, NextLevel);
    }
}
