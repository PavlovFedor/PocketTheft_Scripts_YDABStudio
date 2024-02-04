using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	Vector3 offset = new Vector3(0f,0f,0f);
	public Rigidbody2D player;
	public float xOffset = -5f;
	public float yyy = 0f;
	public float zzz = 0f;
	public bool isFinalBoss = false;
	public bool isExit = false;
	public float yyyUP = 0f;
	public float yyyDown = 0f;
	public float xxxBoss = 0f;
    public AudioSource audioSource;
	public AudioClip audioLoc2;
    public AudioClip audioLoc3;

	void Start()
	{

	}

	//Late update runs after all of the normal updates
	void LateUpdate()
	{
		if(!isExit)
		{
			if(isFinalBoss){
				if(player.position.y >= yyyUP){
					transform.position = new Vector3(xxxBoss, yyyUP, zzz);
				}
				else if(player.position.y <= yyyDown){
					transform.position = new Vector3(xxxBoss, yyyDown, zzz);	
				}
				else{
					transform.position = new Vector3(xxxBoss, player.position.y, zzz);
				}
			}
			else{
				transform.position = new Vector3(player.position.x - xOffset, yyy, zzz);
			}
		}
		else{
			transform.position = new Vector3(570.1f, -44.8f, -5f);
		}

	}

	public void FinalBoss(){
		isFinalBoss = true;
	}

	public void AudioLoc2(){
		audioSource.Stop();
        audioSource.clip = audioLoc2;
		audioSource.Play();
	}

	public void AudioLoc3(){
	audioSource.Stop();
    audioSource.clip = audioLoc3;
	audioSource.Play();
	}
}
