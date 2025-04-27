using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuButton : MonoBehaviour
{
    // Start is called before the first frame update

    public string Level;

    // Update is called once per frame
   
    public void LoadLevel()
    {
        MainMenu Menu = GameObject.FindWithTag("MainMenu").GetComponent<MainMenu>();

        Menu.LoadLevel(Level);
    }
    public void BackToMenu()
    {
        GameManager.Instance.BacktomainMenu(Level);
    }
}
