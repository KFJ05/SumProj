using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public Canvas mainMenu, ChapterSelect;
    public Canvas[] Chapters;
 
    
    public void LoadLevel(string Level)
    {
        GameManager.Instance.LoadLevelFromMenu(Level);
    }

    public void LoadChapterSelect()
    {
        mainMenu.gameObject.SetActive(false);

        ChapterSelect.gameObject.SetActive(true);
    }

    public void BackFromChapterSelect()
    {
        mainMenu.gameObject.SetActive(true);

        ChapterSelect.gameObject.SetActive(false);
    }

    public void SelectChapter(int i)
    {
        Chapters[i].gameObject.SetActive(true);

        ChapterSelect.gameObject.SetActive(false);
    }

    public void ReturntoChapterSelect(int i)
    {
        Chapters[i].gameObject.SetActive(false);

        ChapterSelect.gameObject.SetActive(true);
    }
    

    
    public void ExitGame()
    {
        GameManager.Instance.EndGame();
    }
}
