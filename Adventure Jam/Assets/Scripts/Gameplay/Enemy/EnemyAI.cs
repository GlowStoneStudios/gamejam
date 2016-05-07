using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
	/* Atributos */
	PlayerDetection detector;
	Transform selfTrans;
	bool goToPlayer;
	[Header ("Movement")]
	public float speed = 2.0f;
	float actualSpeed;

	/* Aplicacion al motor */
	void Start () {
		selfTrans = this.transform;
		detector = selfTrans.GetChild (0).gameObject.GetComponent<PlayerDetection> ();
		actualSpeed = speed;
	}

	void Update () {
		if (!goToPlayer) {
			return;
		} else {
			selfTrans.LookAt (PlayerController.instance.selfTrans);
			selfTrans.Translate (0, 0, actualSpeed * Time.deltaTime);
		}
	}

	void FixedUpdate () {
		if (!detector.playerDetected) {
			return;
		} else {
			Ray hit;
			float rDist = 20.0f;
			if (Physics.Raycast (selfTrans.position, PlayerController.instance.selfTrans.position - selfTrans.position, out hit, rDist)) {
				if (hit.transform.gameobject.name != "Player") { // D:??
					return;
				} else {
					goToPlayer = true;
				}
			}
		}
	}

	/* Metodos de la clase */
	void Attack (float atackType) {
		// Animacion de ataque
		// Podrían haber distintas animaciones para que se vea mas dinamico el temilla~
	}

	/* Triggers */
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			actualSpeed = 0.0f;
			Attack (Random.Range (0, 1));
		}
	}

	void OnTriggerExit (Collider other) {
		if (other.tag == "Player") {
			actualSpeed = speed;
		}
	}
}