using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxManager : MonoBehaviour
{

    private static SkyBoxManager instance;
    public static SkyBoxManager Instance
    {
        get
        {

            if (instance == null)
            {
                instance = FindAnyObjectByType<SkyBoxManager>();
            }

            if (!instance)
            {
                Debug.LogError("NO skybox Manager Present");
            }

            return instance;
        }
    }

    public Material Skybox;

    // Start is called before the first frame update
    void Start()
    {
        Skybox = RenderSettings.skybox;
    }

    // Update is called once per frame
    public void SetNewSkybox(Material NewSkybox)
    {
        Skybox = NewSkybox;
        RenderSettings.skybox = Skybox;
    }
}
