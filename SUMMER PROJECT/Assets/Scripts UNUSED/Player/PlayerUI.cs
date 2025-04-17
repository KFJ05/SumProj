using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI Ptext;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void updateText(string text)
    {
        Ptext.text = text;
    }
}
