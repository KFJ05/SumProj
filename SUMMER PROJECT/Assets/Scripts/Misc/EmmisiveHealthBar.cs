using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class EmmisiveHealthBar : MonoBehaviour
{
    // Start is called before the first frame update

    public Health health;

    public Renderer Render;

    public bool GetRenderer = true;

    public Material Mat;

    float Emis;

    public Color colour;

    public bool usingColour = true;

    public bool UseR, UseG, UseB;


    private void Start()
    {
        if (GetRenderer)
        {
            Render = GetComponent<Renderer>();
        }
        Mat = Render.material;
        colour = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        Emis = ((health.CurrentHealth * 255) / (health.MaxHealth * 255));
        

        if(UseR )
        {
            colour.r = Emis;
        }
        if (UseG) 
        {
            colour.g = Emis; 
        }
        if (UseB) 
        {
            colour.b = Emis;
        }
        if (usingColour)
        {
            Render.material.color = colour;
        }
        Mat.SetColor("_EmissionColor", colour);
    }
}
