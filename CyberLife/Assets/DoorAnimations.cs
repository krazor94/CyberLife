using System.Collections;
using UnityEngine;

public class DoorAnimations : MonoBehaviour
{
    public PlayerController.State state;

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Door", true);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            transform.GetChild(0).GetComponent<Animator>().SetBool("Door", false);
        }
    }
}