using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider MusicvolumeSlider;

    public InputField VolumeField;

    public Slider SoundVolumeSlider;

    public InputField SoundField;

    public Slider SensSlider;

    public InputField SensField;


    bool volumeChanged = false;

    bool SoundChanged = false;

    bool SensitivityChanged = false;


    private void OnEnable()
    {
        MusicvolumeSlider.value = GameManager.Instance.GetMusicVolume();

        VolumeField.text = Convert.ToString(Math.Floor(100 * MusicvolumeSlider.value));
        VolumeField.text += "%";
        ///
        SoundVolumeSlider.value = GameManager.Instance.GetSoundVolume();

        SoundField.text = Convert.ToString(Math.Floor(100 * SoundVolumeSlider.value));
        SoundField.text += "%";
        ///
        SensSlider.value = GameManager.Instance.GetmouseSensitivity() / 1000;

        SensField.text = Convert.ToString(Math.Floor(100 * SensSlider.value));
        SensField.text += "%";
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
    /// <summary>
    /// ///////////////////////////
    /// </summary>
    public void ChangeSoundVol()
    {
        GameManager.Instance.ChangSoundVolume(SoundVolumeSlider.value);

        SoundChanged = true;
    }
    public void ChangeSoundVolByText()
    {
        float volume = float.Parse(SoundField.text);

        volume /= 100;

        GameManager.Instance.ChangSoundVolume(volume);

        SoundChanged = true;
    }
    /// <summary>
    /// /////////////////////////////
    /// </summary>
    public void ChangeSens()
    {
        GameManager.Instance.ChangemouseSensitivity(SensSlider.value * 1000);

        SensitivityChanged = true;
    }
    public void ChangeSensByText()
    {
        float sens = float.Parse(SensField.text);

        sens *= 10;

        GameManager.Instance.ChangemouseSensitivity(sens);

        SensitivityChanged = true;
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

        if (SoundChanged)
        {
            SoundChanged = false;
            SoundVolumeSlider.value = GameManager.Instance.GetSoundVolume();

            SoundField.text = Convert.ToString(Math.Floor(100 * SoundVolumeSlider.value));
            SoundField.text += "%";
        }

        if (SensitivityChanged)
        {
            SensitivityChanged = false;
            SensSlider.value = (GameManager.Instance.GetmouseSensitivity()/1000);

            SensField.text = Convert.ToString(Math.Floor(100 * SensSlider.value));
            SensField.text += "%";
        }
    }

}
