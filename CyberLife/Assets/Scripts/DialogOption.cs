using System.Collections;
using UnityEngine;

public class DialogOption : MonoBehaviour
{
    public GameObject gm;
    public GameObject player;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        player = GameObject.Find("Player");
    }

    public void Replace()
    {
        print("Replace");
        DialogSystem.pause = false;
        gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].Complete = true;
        gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].replaced = true;
        //TODO: Enable below when we have models
        //gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].human.SetActive(false);
        //gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].Robot.SetActive(true);

        if (CheckWinState())
        {
            print("Ending Game Code HERE");
            return;
        }
        gm.GetComponent<DialogSystem>().bodyPartNo++;
        transform.root.gameObject.SetActive(false);
        player.GetComponent<NavMeshAgent>().Resume();
    }

    public void Lose()
    {
        print("Lose");
        DialogSystem.pause = false;
        gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].Complete = true;
        gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].replaced = false;
        //TODO: Enable below when we have models
        //gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].human.SetActive(false);
        //gm.GetComponent<DialogSystem>().bodyPartCount[gm.GetComponent<DialogSystem>().bodyPartNo].Robot.SetActive(false);

        if (CheckWinState())
        {
            print("Ending Game Code HERE");
            return;
        }

        gm.GetComponent<DialogSystem>().bodyPartNo++;
        transform.root.gameObject.SetActive(false);
        player.GetComponent<NavMeshAgent>().Resume();
    }

    public bool CheckWinState()
    {
        if (gm.GetComponent<DialogSystem>().bodyPartNo == gm.GetComponent<DialogSystem>().bodyPartCount.Count - 1)
        {
            print("Game Ended!");
            Time.timeScale = 0;
            return true;
			//Activate End-game UI; "EndGameCanvas" with player's choices to
			//find out if they are Human or Transhuman!
        }
        return false;
    }
}