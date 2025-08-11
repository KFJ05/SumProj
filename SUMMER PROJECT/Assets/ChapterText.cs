using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChapterText : MonoBehaviour
{
    public TextMeshProUGUI ChapterTitle;
    public TextMeshProUGUI ChapterDesc;
    public TextMeshProUGUI ChapterTitleOff;
    public TextMeshProUGUI ChapterDescOff;

    Button button;

    private void Start()
    {
        button = gameObject.GetComponent<Button>();
        if(button.interactable == true)
        {
            ChapterTitle.gameObject.SetActive(true);
            ChapterDesc.gameObject.SetActive(true);
            ChapterTitleOff.gameObject.SetActive(false);
            ChapterDescOff.gameObject.SetActive(false);
        }
    }


    // Start is called before the first frame update
    // Update is called once per frame
    void Update()
    {
        if (button.interactable == true)
        {
            ChapterTitle.gameObject.SetActive(true);
            ChapterDesc.gameObject.SetActive(true);
            ChapterTitleOff.gameObject.SetActive(false);
            ChapterDescOff.gameObject.SetActive(false);
        }
        else
        {
            ChapterTitle.gameObject.SetActive(false);
            ChapterDesc.gameObject.SetActive(false);
            ChapterTitleOff.gameObject.SetActive(true);
            ChapterDescOff.gameObject.SetActive(true);   
        }
    }
}
