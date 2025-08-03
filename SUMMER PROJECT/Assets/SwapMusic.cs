using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapMusic : MonoBehaviour
{
    // Start is called before the first frame update

    public AudioClip Clip1;
    public AudioClip Clip2;
    public AudioSource source;

    bool triggered = false;

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // if(other.gameObject.tag == "Player")
        //{
        //    source.clip = Clip2;
        //}
        if (triggered == false)
        {
            GameManager.Instance.StopMusic();
            GameManager.Instance.PlayMusic(Clip1, true);
            triggered = true;
        }
    }
}
