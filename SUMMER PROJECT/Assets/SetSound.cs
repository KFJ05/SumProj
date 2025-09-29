using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSound : MonoBehaviour
{
    public AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        if (GameManager.Instance != null)
        {
            if (source.volume != GameManager.Instance.GetSoundVolume())
            {
                source.volume = GameManager.Instance.GetSoundVolume();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (source.volume != GameManager.Instance.GetSoundVolume())
            {
                source.volume = GameManager.Instance.GetSoundVolume();
            }
        }
    }
}
