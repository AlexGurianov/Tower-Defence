using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ShotInfo {
    public Vector3 targetPosition;
    public Vector3 movementDirection;
    public float speed;

    public ShotInfo(Vector3 tP, Vector3 mD, float sp)
    {
        targetPosition = tP;
        movementDirection = mD;
        speed = sp;
    }
}
