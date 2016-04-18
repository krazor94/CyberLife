using UnityEngine;
using System.Collections;

public class DoorAnimations : MonoBehaviour 
{

	void OnTriggerEnter(Collider col)
	{

		if (col.tag == "Player") 
		{
			transform.GetChild(0).GetComponent<Animator> ().SetBool ("Door", true);
		}

	}
	void OnTriggerExit(Collider col)
	{

		if (col.tag == "Player") 
		{
			transform.GetChild(0).GetComponent<Animator> ().SetBool ("Door", false);
		}

	}


}
