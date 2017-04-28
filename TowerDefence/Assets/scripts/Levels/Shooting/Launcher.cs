using UnityEngine;
using System.Collections;

public class Launcher : MonoBehaviour
{
    /*// Editor variables
    [Range(20f, 70f)]
    public float _angle;      // shooting angle*/

    //float V = 10f;

    float angle = 60;

    public ShotInfo shotInfo;

    private bool canFade = false;

    int monstersLayer = 10;

    public void Launch()
    {
        Vector3 target = shotInfo.targetPosition;

        // source and target positions
        Vector3 pos = transform.position;
        //Vector3 target = new Vector3(0, 0, 0);

        // distance between target and source
        float dist = Vector2.Distance(new Vector2(pos.x, pos.z), new Vector2(target.x, target.z)); //Vector3.Distance(pos, target);
        float H = pos.y;

        // calculate initival velocity required to land the cube on target using the formula (9)
        //float Vi = Mathf.Sqrt(dist * -Physics.gravity.y / (Mathf.Sin(Mathf.Deg2Rad * angle * 2)));

        float Vi = dist * Mathf.Sqrt(-Physics.gravity.y/(2*(H + dist * Mathf.Tan(Mathf.Deg2Rad * angle)))) / Mathf.Cos(Mathf.Deg2Rad * angle);
        float Vy, Vz;   // y,z components of the initial velocity

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * angle);

        float T = dist/Vz;

        transform.LookAt(shotInfo.targetPosition + shotInfo.speed * T * shotInfo.movementDirection);

        /*float Determinant = 4 * Mathf.Pow(V, 4) / Mathf.Pow(dist * -Physics.gravity.y, 2) - 4 * (1 - 2 * V * V * H / (dist * dist * -Physics.gravity.y));
        float angle = Mathf.Atan(V * V / (dist * -Physics.gravity.y) + Mathf.Sqrt(Determinant) / 2);

        float Vy = V * Mathf.Sin(angle);
        float Vz = V * Mathf.Cos(angle);

        float T = dist / (V * Mathf.Cos(angle));
        */

        //Debug.Log(angle);

        // rotate the object to face the target
        //transform.LookAt(target);

        // create the velocity vector in local space
        Vector3 localVelocity = new Vector3(0f, Vy, Vz);

        // transform it to global vector
        Vector3 globalVelocity = transform.TransformVector(localVelocity);

        // launch the cube by setting its initial velocity
        GetComponent<Rigidbody>().velocity = globalVelocity;

        canFade = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == monstersLayer && transform.position.y > 1)
        {
            Destroy(gameObject);
        }
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