using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    public GameObject dialogOptions;
    public static bool pause;
    public int bodyPartNo;
    public bool fillDialog;
    public List<bodyPartsUPto> bodyPartCount = new List<bodyPartsUPto>();
    public float incChar, nextChar;
    private int dialogNo;
    public bool runOnce = false;

    [System.Serializable]
    public class Dialog
    {
        public BodyParts bodypart;
        public bool Complete;
        public string text;

        public int c, totalChars;
    };

    public List<Dialog> Dialogs = new List<Dialog>();

    public Text text;

    public enum BodyParts
    {
        arm,
        leg,
        hand,
        eye,
        head,
        foot
    }

    [System.Serializable]
    public class bodyPartsUPto
    {
        public BodyParts bodypart;
        public bool Complete;
        public bool replaced;

        public GameObject human;
        public GameObject Robot;
    };

    private void Start()
    {
        bodyPartNo = 0;
        for (int i = 0; i < Dialogs.Count; i++)
        {
            Dialogs[i].totalChars = Dialogs[i].text.Length;
        }
    }

    private void FixedUpdate()
    {
        if (fillDialog)
        {
            if (Dialogs[dialogNo].c < Dialogs[dialogNo].totalChars)
            {
                if (Time.time > nextChar)
                {
                    text.text = text.text + Dialogs[dialogNo].text[Dialogs[dialogNo].c];
                    Dialogs[dialogNo].c++;
                    nextChar = Time.time + 0.1f;
                }
            }
        }
    }

    private void Update()
    {
        if (pause && runOnce)
        {
            SelectDialog();
            print("Run Once");
            text.text = string.Empty;
            runOnce = false;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (pause)
            {
                //Time.timeScale = 1;
                pause = false;
            }
            else if (!pause)
            {
                //Time.timeScale = 0;
                pause = true;
            }
        }
    }

    //Need to figure otu a way to select dialogs for each body part
    public void SelectDialog()
    {
        dialogNo = Random.Range(0, Dialogs.Count);
        print("Randomly Select a Dialog: " + dialogNo);

        if (Dialogs[dialogNo].bodypart == bodyPartCount[bodyPartNo].bodypart)
        {
            print("Dialog has not been done");
            if (!bodyPartCount[bodyPartNo].Complete)
            {
                print("Offer options");
                if (dialogOptions.activeSelf == false)
                {
                    dialogOptions.SetActive(true);
                    fillDialog = true;
                }
            }
        }
        else
            SelectDialog();
    }
}