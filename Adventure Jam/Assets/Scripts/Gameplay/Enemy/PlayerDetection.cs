using UnityEngine;
using System.Collections;

public class PlayerDetection : MonoBehaviour
{
	/* Atributos */
	[Header ("Player detector")]
	public bool playerDetected;

	/* Triggers */
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			playerDetected = true;
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			playerDetected = false;
		}
	}
}