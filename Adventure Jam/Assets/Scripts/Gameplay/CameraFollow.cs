using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    Transform cached;
    public bool FollowPlayer = true;
    public Vector3 TargetPositionOffset;
    public Vector3 TargetPosition { get ; private set;} 
    public Vector3 LookAtPosition { get ; private set;}
    public float FollowSpeed = 3f, RotationSmooth = 3f;

	// Use this for initialization
	void Awake () {
        cached = this.transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        if (FollowPlayer)
        {
            Quaternion rotation = Quaternion.LookRotation(PlayerController.instance.cached.position - cached.position);

            cached.rotation = Quaternion.Slerp(cached.rotation, rotation, Time.deltaTime * RotationSmooth);
             
            cached.position = Vector3.Lerp(cached.position, (PlayerController.instance.cached.position + TargetPositionOffset), Time.deltaTime * FollowSpeed);
        }
        else
        {
            Quaternion rotation = Quaternion.LookRotation(TargetPosition - cached.position);
            cached.rotation = Quaternion.Slerp(cached.rotation, rotation, Time.deltaTime * RotationSmooth);

            cached.position = Vector3.Lerp(cached.position, (TargetPosition - TargetPositionOffset), Time.deltaTime * FollowSpeed);
        }
	}
}
