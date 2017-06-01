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

    public float damage = 1f;

    private bool canFade = false;

    int monstersLayer = 10;

    public bool ifHadHit = false;

    public float Launch()
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

        //transform.LookAt(shotInfo.targetPosition + shotInfo.speed * T * shotInfo.movementDirection);

        float alpha = Mathf.Acos(((pos.x - target.x) * shotInfo.movementDirection.x + (pos.z - target.z) * shotInfo.movementDirection.z)/dist);

        float a = Mathf.Pow(Physics.gravity.y/(2 * Mathf.Tan(Mathf.Deg2Rad * angle)), 2);
        float b = 0;
        float c = Physics.gravity.y * H / Mathf.Pow(Mathf.Tan(Mathf.Deg2Rad * angle), 2) - Mathf.Pow(shotInfo.speed, 2);
        float d = 2 * dist * shotInfo.speed * Mathf.Cos(alpha);
        float e = Mathf.Pow(H / Mathf.Tan(Mathf.Deg2Rad * angle), 2) - Mathf.Pow(dist, 2);

        float t = SolveQuadraticNewton(a, b, c, d, e, T);
        
        Vi = (-Physics.gravity.y * t * t / 2 - H) / (Mathf.Sin(Mathf.Deg2Rad * angle) * t);

        Vy = Vi * Mathf.Sin(Mathf.Deg2Rad * angle);
        Vz = Vi * Mathf.Cos(Mathf.Deg2Rad * angle);

        transform.LookAt(shotInfo.targetPosition + shotInfo.speed * t * shotInfo.movementDirection);

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

        return t;
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

    public float quadratic_func(float x, float a, float b, float c, float d, float e)
    {
        return a * Mathf.Pow(x, 4) + b * Mathf.Pow(x, 3) + c * Mathf.Pow(x, 2) + d * x + e;
    }

    public float quadratic_func_der(float x, float a, float b, float c, float d, float e)
    {
        return 4 * a * Mathf.Pow(x, 3) + 3 * b * Mathf.Pow(x, 2) + 2 * c * x + d;
    }

    public float SolveQuadraticNewton(float a, float b, float c, float d, float e, float x0)
    {
        float x = x0;
        while (Mathf.Abs(quadratic_func(x, a, b, c, d, e)) > 0.05)
        {
            x = x - quadratic_func(x, a, b, c, d, e) / quadratic_func_der(x, a, b, c, d, e);
        }
        return x;
    }
}