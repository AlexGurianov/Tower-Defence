using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{
    // Editor variables
    [Range(1f, 6f)]
    public float _targetRange;
    [Range(20f, 70f)]
    public float _angle;      // shooting angle


    private bool canFade = false;

    public void Launch()
    {
        // source and target positions
        Vector3 pos = transform.position;
        Vector3 target = new Vector3(0, 0, 0);

        // distance between target and source
        float dist = Vector3.Distance(pos, target);

        // rotate the object to face the target
        transform.LookAt(target);

        // calculate initival velocity required to land the cube on target using the formula (9)
        float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * _angle * 2)));
        float Vy, Vz;   // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * _angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * _angle);

        // create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        // transform it to global vector
        Vector3 globalVelocity = transform.TransformVector(localVelocity);

        // launch the cube by setting its initial velocity
        GetComponent<Rigidbody>().velocity = globalVelocity;
        
        canFade = true;
    }
    

    void Update()
    {
        if (canFade && GetComponent<Rigidbody>().velocity.magnitude < 0.01)
        {
            canFade = false;
            GetComponent<PumpkinFadeOut>().FadePumpkinOut();
        }
    }
}