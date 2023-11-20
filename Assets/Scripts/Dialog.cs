using System;
using System.Collections;

using TMPro;

using UnityEngine;


public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float speed;
    int index;
    bool select2 = true;
    bool select4 = true;
    bool select0 = true;
    bool select6 = true;
    bool select7 = true;
    int trackIndex = 0;

    private void Start()
    {
        textComponent.text = string.Empty;
        startDialog(0);
    }

    public void startDialog(int index)
    {
        StartCoroutine(textLine(index));
    }

    IEnumerator textLine(int index)
    {
        
        if (index == trackIndex)
        {
            textComponent.text = string.Empty;
            trackIndex += 1;
            foreach (char c in lines[index])
            {
                textComponent.text += c;
                yield return new WaitForSeconds(speed);
            }

        }
        else Debug.Log("statement");
    }
    public void Index2(int index)
    {
        if (select2 && trackIndex == 2)
        {
            select2 = false;
            StartCoroutine(textLine(index));
            StartCoroutine(waitFor4Sec(3));
        }
    }
    IEnumerator waitFor4Sec(int index)
    {
        yield return new WaitForSeconds(4);
        StartCoroutine(textLine(index));
    }
    public void index4(int index)
    {
        if (select4 && trackIndex == 4)
        {
            select4 = false;
            StartCoroutine(textLine(index));
            StartCoroutine(waitFor4Sec(5));
        }
    }
    public void onButtonClick()
    {
        if (select0 && trackIndex == 1)
        {
            select0 = false;
            StartCoroutine(textLine(1));
        }
        else if (select6 && trackIndex == 6)
        {
            select6 = false;
            StartCoroutine(textLine(6));
        }
        else if (select7 && trackIndex == 7)
        {
            select7 = false;
            StartCoroutine(textLine(7));
        }
    }
    
}