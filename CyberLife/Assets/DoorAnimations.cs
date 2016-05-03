using System.Collections;
using UnityEngine;

public class DoorAnimations : MonoBehaviour
{
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