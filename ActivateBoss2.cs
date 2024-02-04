using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBoss2 : MonoBehaviour
{
	public GameObject Boss2;

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player"){
			Boss2.gameObject.SetActive(true);
            Destroy(gameObject);
		}
    }
}
