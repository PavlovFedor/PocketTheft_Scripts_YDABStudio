using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLocActivate : MonoBehaviour
{
	public GameObject barrierPrefab;
	public GameObject cameraGO;
	public float xxx = 0f;
	public float yyy = 0f;
    public CameraFollow cameraFollow;

    private void OnTriggerStay2D(Collider2D collision){
		if(collision.gameObject.tag == "Player"){
			
			Vector3 spawnPos = transform.position;
            spawnPos = new Vector3(xxx, yyy, 0f);
            cameraGO.GetComponent<CameraFollow>().FinalBoss();
			Instantiate(barrierPrefab, spawnPos, Quaternion.identity);
            cameraFollow.AudioLoc3();
			Destroy(gameObject);
		}
	}
}
