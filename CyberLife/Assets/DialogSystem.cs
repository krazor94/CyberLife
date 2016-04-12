using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [System.Serializable]
    public class Dialog
    {
        public bool Complete;
        public string text;
        public GameObject human;
        public GameObject Robot;
    };

    public List<Dialog> Dialogs = new List<Dialog>();
    
    public Text text;
    public float incChar, nextChar;
    public int c, totalChars;
    public bool FillDialog;

    void Start()
    {
        totalChars = Dialogs[0].text.Length;
    }

    
    void FixedUpdate()
    {

        if (FillDialog)
        {
            if (c < totalChars)
            {
                if (Time.time > nextChar)
                {
                    text.text = text.text + Dialogs[0].text[c];
                    c++;
                    nextChar = Time.time + 0.1f;
                }
            }
        }
    }
}