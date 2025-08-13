using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public Canvas mainMenu, ChapterSelect, ChooseChapterSelect,ChapterSelectEX,Credits,Settings;
    public Canvas[] Chapters;
    public Canvas[] EXChapters;

    public AudioClip MenuMusic;

    private void Awake()
    {
        if (GameManager.Instance.GetMusic() == null)
        {
            GameManager.Instance.PlayMusic(MenuMusic, true);
        }
    }

    public void LoadLevel(string Level)
    {
        GameManager.Instance.LoadLevelFromMenu(Level);
    }

    public void LoadChapterSelect()
    {
        mainMenu.gameObject.SetActive(false);

        if (GameManager.Instance.NormalGameCompleted == true)
        {
            ChooseChapterSelect.gameObject.SetActive(false);
        }

        ChapterSelect.gameObject.SetActive(true);
    }

    public void LoadCredits()
    {
        mainMenu.gameObject.SetActive (false);

        Credits.gameObject.SetActive(true);
    }
    public void ExitCredits()
    {
        mainMenu.gameObject.SetActive(true);

        Credits.gameObject.SetActive(false);
    }

    public void LoadSettings()
    {
        mainMenu.gameObject.SetActive(false);

        Settings.gameObject.SetActive(true);
    }
    public void ExitSettings()
    {
        mainMenu.gameObject.SetActive(true);

        Settings.gameObject.SetActive(false);
    }


    public void LoadEXChapterSelect()
    {
        mainMenu.gameObject.SetActive(false);

        if (GameManager.Instance.NormalGameCompleted == true)
        {
            ChooseChapterSelect.gameObject.SetActive(false);
        }

        ChapterSelectEX.gameObject.SetActive(true);
    }

    public void LoadChapterNormalOrEX()
    {
        if (GameManager.Instance.NormalGameCompleted == true)
        {
            mainMenu.gameObject.SetActive(false);

            ChooseChapterSelect.gameObject.SetActive(true);
        }
        else
        {
            LoadChapterSelect();
        }
    }

    

    public void BackFromChapterSelect()
    {
        if (GameManager.Instance.NormalGameCompleted == true)
        {
            ChooseChapterSelect.gameObject.SetActive(true);

            ChapterSelect.gameObject.SetActive(false);
        }
        else
        {
            mainMenu.gameObject.SetActive(true);

            ChapterSelect.gameObject.SetActive(false);
        }
    }

    public void BackFromEXChapterSelect()
    {
        if (GameManager.Instance.NormalGameCompleted == true)
        {
            ChooseChapterSelect.gameObject.SetActive(true);

            ChapterSelectEX.gameObject.SetActive(false);
        }
        else
        {
            mainMenu.gameObject.SetActive(true);

            ChapterSelectEX.gameObject.SetActive(false);
        }
    }


    public void BackFromChooseChapterSelect()
    {
        mainMenu.gameObject.SetActive(true);

        ChooseChapterSelect.gameObject.SetActive(false);
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
    public void SelectEXChapter(int i)
    {
        EXChapters[i].gameObject.SetActive(true);

        ChapterSelectEX.gameObject.SetActive(false);
    }
    public void ReturntoEXChapterSelect(int i)
    {
        Chapters[i].gameObject.SetActive(false);

        ChapterSelectEX.gameObject.SetActive(true);
    }




    public void ExitGame()
    {
        GameManager.Instance.EndGame();
    }
}
