using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
	/* Atributos */
	public static PlayerController instance;
	public Transform selfTrans;

	/* Aplicacion al motor */
	void Awake () {
		instance = this;
	}

	void Start () {
		selfTrans = this.transform;
	}
}
