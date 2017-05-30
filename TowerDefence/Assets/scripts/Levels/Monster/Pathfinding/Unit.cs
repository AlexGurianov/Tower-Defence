using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour {

	const float minPathUpdateTime = .2f;
	const float pathUpdateMoveThreshold = .5f;

	public Transform target;
	public float speed = 20;
	public float turnSpeed = 3;
	public float turnDst = 5;
	public float stoppingDst = 10;

    public float lambdaStop = 0.2f;

	Path path;

    bool isStopped = false;

    bool goingToDie = false;

	void Start() {
        target = GameObject.Find("Target").transform;
        speed *= Random.Range(1f, 1.5f);
        StartCoroutine (UpdatePath ());
        GetNextStop();
	}

    void GetNextStop()
    {
        float time = RandomStops.TimeToStop(lambdaStop);
        StartCoroutine(WaitAndStop(time));
    }

    IEnumerator WaitAndStop(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        isStopped = true;
        float stopTime = RandomStops.TimeOfStop();
        yield return new WaitForSeconds(stopTime);
        isStopped = false;
        GetNextStop();
    }

	public void OnPathFound(Vector3[] waypoints, bool pathSuccessful) {
        if (pathSuccessful) {
            path = new Path(waypoints, transform.position, turnDst, stoppingDst);

			StopCoroutine("FollowPath");
			StartCoroutine("FollowPath");
		}
        else
            StopCoroutine("FollowPath");
    }

	public IEnumerator UpdatePath() {

		if (Time.timeSinceLevelLoad < .3f) {
			yield return new WaitForSeconds (.3f);
		}
		PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));

		float sqrMoveThreshold = pathUpdateMoveThreshold * pathUpdateMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds (minPathUpdateTime);
			//print (((target.position - targetPosOld).sqrMagnitude) + "    " + sqrMoveThreshold);
			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));
				targetPosOld = target.position;
			}
		}
	}

	IEnumerator FollowPath() {

		bool followingPath = true;
		int pathIndex = 0;
		transform.LookAt (path.lookPoints [0]);

		float speedPercent = 1;

		while (followingPath) {
			Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
                    if (!goingToDie)
                    {
                        DataStorage.dataStorage.IncrementMobsPassed();
                        GameObject.Find("MobsPassed Text").GetComponent<MobsPassedController>().UpdateMobsPassed();
                        StopAllCoroutines();
                        target = GameObject.Find("DeathPlace").transform;
                        DataStorage.dataStorage.monstersDictionary.Remove(GetComponent<MonsterController>().ID);
                        StartCoroutine(UpdatePath());
                        goingToDie = true;
                        GetComponent<MonsterController>().invinsible = true;
                    }
                    else
                    {
                        Destroy(gameObject);
                    }
                    break;
				} else {
					pathIndex++;
				}
			}

			if (followingPath && !isStopped) {

				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
					if (speedPercent < 0.01f) {
						followingPath = false;
					}
				}

				Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
				transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}

			yield return null;

		}
	}

    public void Stop()
    {
        StopCoroutine("FollowPath");
    }

	/*public void OnDrawGizmos() {
		if (path != null) {
			path.DrawWithGizmos ();
		}
	}*/
}
