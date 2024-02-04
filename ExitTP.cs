using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitTP : MonoBehaviour
{
	public Transform tpPlayer;
	public GameObject camera1;
	public Text HPCount;
    public Image HPIcon;

	public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
			CameraFollow cameraFollow = camera1.GetComponent<CameraFollow>();
			cameraFollow.isExit = true;
	    	HPCount.enabled = false;
	        HPIcon.enabled = false;
			collision.transform.position = tpPlayer.transform.position;
		}
    }
}
