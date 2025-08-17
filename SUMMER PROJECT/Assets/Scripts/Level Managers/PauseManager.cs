using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    // Start is called before the first frame update
    private static PauseManager instance;
    public static PauseManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PauseManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO Pause Manager Present");
            }

            return instance;
        }

    }

    public bool IsPaused;


    public void Pause()
    {
        IsPaused = true;
    }

    public void Resume()
    {
        IsPaused = false;
    }

}
