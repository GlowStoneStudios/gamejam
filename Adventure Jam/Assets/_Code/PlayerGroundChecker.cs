using UnityEngine;
using System.Collections;

public class PlayerGroundChecker : MonoBehaviour
{
	/* Atributos */
	public static PlayerGroundChecker instance;
	[Header ("Ground Checker")]
	public bool onGround;

	/* Aplicacion al motor */
	void Awake () {
		instance = this;
	}

	/* Metodos de la clase */
	/* Triggers */
	void OnTriggerEnter (Collider o) {
		if (o.tag == "Untagged") {
			onGround = true;
		}
	}

	void OnTriggerExit (Collider o) {
		if (o.tag == "Untagged") {
			onGround = false;
		}
	}
}