using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    
	public static PlayerController instance;
    public  Transform cached { get; private set; }
   
    public enum playerCurState
    {
        Idle, Walk, SideWalk, BackWalk, Run, Jump, Crouch, Action, Grab
    }

    [Header("Movimiento")]

    public bool canMove = true;
    public playerCurState playerState;
    public float WalkSpeed = 3f, RunSpeed = 7f;
    float curSpeed = 0f;

    float aX, aY;
    bool jumpKey, runKey;

    [Header("Rol")]
    public float Health = 100f;
    public float Mana = 100f;




	void Awake (){
		instance = this;
        cached = this.transform;
	}

    void Update(){

        aX = Input.GetAxisRaw("Horizontal");
        aY = Input.GetAxisRaw("Vertical");

        MovementCore();
    }
    void MovementCore()
    {
        if (canMove)
        {

            jumpKey = Input.GetKey(KeyCode.Space);
            runKey = Input.GetKey(KeyCode.LeftShift);

            //Logica de movimiento forward y backward

            if(aY != 0)
            {
                if (aY > 0)
                {
                    if (runKey)
                    {
                        playerState = playerCurState.Run;
                        curSpeed = RunSpeed;
                    }
                    else
                    {
                        playerState = playerCurState.Walk;
                        curSpeed = WalkSpeed;
                    }
                }
                else
                {
                    playerState = playerCurState.BackWalk;
                    curSpeed = WalkSpeed;
                }

                cached.Translate(Vector3.forward * aY *curSpeed * Time.deltaTime);

            }
            // Logica de movimiento lateral

            if(aX != 0)
            {
                if (aY != 0)
                {
                    cached.Translate(Vector3.right * aX * curSpeed * 0.666f * Time.deltaTime);
                }
                else
                {
                    playerState = playerCurState.SideWalk;
                    curSpeed = WalkSpeed;
                    cached.Translate(Vector3.right * aX * curSpeed * 0.666f * Time.deltaTime);
                }
            }

            //Volver a idle cuando los ejes son 0

            if (aX == 0 && aY == 0)
            {
                curSpeed = 0f;
                playerState = playerCurState.Idle;
            }
        }
    }
}
