using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    /* Atributos */
	public static PlayerController instance;
    public  Transform cached { get; private set; }
   
    public enum playerCurState
    {
        Idle, Walk, SideWalk, BackWalk, Run, Jump, Crouch, Action, Grab
    }

    [Header("Movimiento")]

    public bool canMove = true;
    public playerCurState playerState;
    public float WalkSpeed = 3f, RunSpeed = 7f, JumpForce = 300f;
    float curSpeed = 0f;
	Rigidbody rB;
    float aX, aY;
    bool jumpKey, runKey;

    [Header("Rol")]
    public float Health = 100f;
    public float Mana = 100f;

	/* Aplicacion al motor */
	void Awake () {
		instance = this;
        cached = this.transform;
		rB = GetComponent<Rigidbody> ();
	}

    void Update () {
		aX = Input.GetAxisRaw ("Horizontal");
        aY = Input.GetAxisRaw ("Vertical");

        MovementCore();
    }

	/* Metodos de la clase */
	void DoJump () {
		if (PlayerGroundChecker.instance.onGround) {
			rB.AddForce (Vector3.up * JumpForce, ForceMode.Force);
			playerState = playerCurState.Jump;
		} else {
			return;
		}
	}

    void MovementCore () {
        if (canMove) {

            jumpKey = Input.GetKey(KeyCode.Space);
            runKey = Input.GetKey(KeyCode.LeftShift);

            //Logica de movimiento forward y backward
            if(aY != 0) {
				
                if (aY > 0) {
					
                    if (runKey) {
						
                        playerState = playerCurState.Run;
                        curSpeed = RunSpeed;
                    } else {
						
                        playerState = playerCurState.Walk;
                        curSpeed = WalkSpeed;
                    }
                } else {
					
                    playerState = playerCurState.BackWalk;
                    curSpeed = WalkSpeed;
                }

                cached.Translate(Vector3.forward * aY *curSpeed * Time.deltaTime);
            }

            // Logica de movimiento lateral
            if(aX != 0) {
				
                if (aY != 0) {
					
                    cached.Translate(Vector3.right * aX * curSpeed * 0.666f * Time.deltaTime);
                } else {
					
                    playerState = playerCurState.SideWalk;
                    curSpeed = WalkSpeed;
                    cached.Translate(Vector3.right * aX * curSpeed * 0.666f * Time.deltaTime);
                }
            }

            // Volver a idle cuando los ejes son 0
            if (aX == 0 && aY == 0) {
				
                curSpeed = 0f;
                playerState = playerCurState.Idle;
            }

			// Salto
			if (jumpKey) {
				DoJump ();
			}
        }
    }
}