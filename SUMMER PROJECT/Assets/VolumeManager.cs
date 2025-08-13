using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider MusicvolumeSlider;

    public InputField VolumeField;

    bool volumeChanged = false;


    private void OnEnable()
    {
        MusicvolumeSlider.value = GameManager.Instance.GetMusicVolume();

        VolumeField.text = Convert.ToString(Math.Floor(100 * MusicvolumeSlider.value));
        VolumeField.text += "%";
    }

    public void ChangeVolume()
    {
        GameManager.Instance.ChangeMusicVolume(MusicvolumeSlider.value);

        volumeChanged = true;
    }
    public void ChangeVolumeByText()
    {
        float volume = float.Parse(VolumeField.text);

        volume /= 100;

        GameManager.Instance.ChangeMusicVolume(volume);

        volumeChanged = true;
    }

    private void Update()
    {
        if (volumeChanged)
        {
            volumeChanged = false;
            MusicvolumeSlider.value = GameManager.Instance.GetMusicVolume();

            VolumeField.text = Convert.ToString(Math.Floor(100 * MusicvolumeSlider.value));
            VolumeField.text += "%";

        }
    }

}
