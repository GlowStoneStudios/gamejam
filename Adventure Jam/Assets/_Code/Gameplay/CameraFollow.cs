using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    Transform cached;

    public Transform TargetPosition;
    public Transform LookAtTarget;
    public float FollowSpeed = 3f, RotationSmooth = 3f;

	void Awake () {
        cached = this.transform;
	}

	void LateUpdate () {

        Quaternion rotation = Quaternion.LookRotation(LookAtTarget.position - cached.position);
            cached.rotation = Quaternion.Slerp(cached.rotation, rotation, Time.deltaTime * RotationSmooth);
        cached.position = Vector3.Lerp(cached.position, (TargetPosition.position), Time.deltaTime * FollowSpeed);

	}
}
