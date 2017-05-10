using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour {

    public int ID;
    public float CreationTime;

    public float energy = 1f;

    Animator animator;

    int weaponsLayer = 11;

	// Use this for initialization
	void Start () {
        CreationTime = Time.time;
        animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == weaponsLayer && collision.collider.transform.position.y > 1)
        {
            energy = 0;
            if (energy < 0.05)
            {
                gameObject.layer = 0;
                GetComponent<Unit>().Stop();
                animator.SetFloat("Energy", energy);
                DeleteMonster(GetAnimationTime("ghost_die"));
                GetComponents<AudioSource>()[0].volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.gameSoundLevel;
                GetComponents<AudioSource>()[0].Play();
            }
        }
    }

    public void DeleteMonster(float delay)
    {
        DataStorage.dataStorage.monstersDictionary.Remove(ID);
        Destroy(gameObject, delay);
    }

   float GetAnimationTime(string animationName)
    {
        float time = 0;
        RuntimeAnimatorController ac = animator.runtimeAnimatorController;
        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == animationName)
                time = ac.animationClips[i].length;
        return time;
    }

    public MonsterInfo GiveSaveInfo()
    {
        Vector3 pos = transform.position;
        return new MonsterInfo(pos.x, pos.y, pos.z, energy);
    }
}
