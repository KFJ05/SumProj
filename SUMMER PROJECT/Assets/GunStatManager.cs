using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunStatManager : MonoBehaviour
{
    // Start is called before the first frame update

    private static GunStatManager instance;
    public static GunStatManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindAnyObjectByType<GunStatManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO Gun_Stat Manager Present");
            }

            return instance;
        }

    }

    [Header("Image")]
    public UnityEngine.UI.Image image;

    [Header("Text")]
    public TextMeshProUGUI GunName;
    public TextMeshProUGUI GunDescription;
    public TextMeshProUGUI GunStats;
    public TextMeshProUGUI GunEffects;

    public void turnOnPicture()
    {
        image.gameObject.SetActive(true);
    }
    public void turnOffPicture()
    {
        image.gameObject.SetActive(false);
    }

    public void setName(string name)
    {
        GunName.text = name;
    }
    public void SetDesc(string Desc)
    {
        GunDescription.text = Desc;
    }
    public void SetStats(string Stats)
    {
        GunStats.text = Stats;
    }
    public void SetEffects(string Effects) 
        {
            GunEffects.text = Effects;
        }
}
