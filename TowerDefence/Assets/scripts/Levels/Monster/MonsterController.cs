using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

    public int ID;
    public float CreationTime;

    public float maxEnergy = 1f;
    public float energy = 1f;

    public Slider healthSlider;

    Animator animator;

    int weaponsLayer = 11;

    public bool invinsible = false;

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
        if (collision.collider.gameObject.layer == weaponsLayer && collision.collider.transform.position.y > 1
            && !collision.collider.GetComponent<Launcher>().ifHadHit && energy >= 0.05)
        {
            energy -= collision.collider.GetComponent<Launcher>().damage;
            collision.collider.GetComponent<Launcher>().ifHadHit = true;
            healthSlider.value = energy / maxEnergy;
            if (energy < 0.05)
            {
                gameObject.layer = 0;
                GetComponent<Unit>().Stop();
                animator.SetFloat("Energy", energy);
                DeleteMonster(GetAnimationTime("ghost_die"));
                GetComponents<AudioSource>()[0].volume = SceneInfoCarrier.sceneInfoCarrier.gameInfo.profilesList[SceneInfoCarrier.sceneInfoCarrier.gameInfo.userNo].settings.gameSoundLevel;
                GetComponents<AudioSource>()[0].Play();                
            }
            else
                animator.SetTrigger("Damaged");
        }
    }

    IEnumerator UpdateAfterMonsterDeath(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameObject.Find("Coins Text").GetComponent<CoinsController>().AddCoinsForMonster();
        DataStorage.dataStorage.mobsKilled++;
    }

    public void DeleteMonster(float delay)
    {
        DataStorage.dataStorage.monstersDictionary.Remove(ID);
        StartCoroutine(UpdateAfterMonsterDeath(0.9f*delay));
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
        return new MonsterInfo(pos.x, pos.y, pos.z, maxEnergy, energy);
    }
}
