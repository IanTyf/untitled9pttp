using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisplay : MonoBehaviour
{
    public string text;

    public List<letterGroup> letterGroups = new List<letterGroup>();

    private void Awake()
    {
        Services.textDisplay = this;
    }

    void LateUpdate()
    {
        UpdateDisplayText();
    }

    public void AddLetterGroup(int id, int[] indexes, Color color)
    {
        letterGroup newLetterGroup = new letterGroup
        {
            id = id,
            indexes = indexes,
            color = color
        };
        letterGroups.Add(newLetterGroup);
    }

    public void RemoveLetterGroup(int id)
    {
        for (int i=letterGroups.Count-1; i>=0; i--)
        {
            if (letterGroups[i].id == id) letterGroups.RemoveAt(i);
        }
    }

    public void UpdateDisplayText()
    {
        string[] newTextArr = new string[text.Length];
        for (int i=0; i<text.Length; i++)
        {
            newTextArr[i] = text[i].ToString();
        }
        foreach (letterGroup lg in letterGroups)
        {
            string cHex = ColorUtility.ToHtmlStringRGB(lg.color);
            foreach (int i in lg.indexes)
            {
                newTextArr[i] = "<color=#" + cHex + "><size=+7>" + newTextArr[i] + "</size></color>";
            }
        }

        string displayText = "";
        foreach (string s in newTextArr)
        {
            displayText += s;
        }

        GetComponent<TMP_Text>().text = displayText;
    }
}

public struct letterGroup
{
    public int id;
    public int[] indexes;
    public Color color;
}