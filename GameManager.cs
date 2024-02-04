using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public Text HPCount;
    public Image HPIcon;
    public Image background;
    public GameObject button;
    public GameObject boss1;
    public GameObject boss2;
    public GameObject player;
    public GameObject firstStageCompleted;
    public GameObject secondStageCompleted;
    public GameObject barrierBoss1;
    public GameObject barrierBoss2;
    public Animator animator;
    public CameraFollow cameraFollow;

    private int HPplayer;
    private Text test;
    private bool isHPfull;
    private bool singleUseAfterBoss1 = false;
    private bool singleUseAfterBoss2 = false;

    void Start()
    {
        Time.timeScale = 0;
        HPCount.enabled = false;
        HPIcon.enabled = false;
    }
    public void StartGame(){
    	background.enabled = false;
    	button.gameObject.SetActive(false);
        firstStageCompleted.gameObject.SetActive(false);
        secondStageCompleted.gameObject.SetActive(false);
    	HPCount.enabled = true;
        HPIcon.enabled = true;
    	Time.timeScale = 1;
    }
    // Update is called once per frame
    void Update()
    {
        HPplayer = player.GetComponent<PlayerMover>().health;
        HPCount.text = HPplayer.ToString();
    	if (boss1 == null && singleUseAfterBoss1 == false){     
            firstStageCompleted.gameObject.SetActive(true);
            Destroy(barrierBoss1,5f);
            cameraFollow.AudioLoc2();
            singleUseAfterBoss1 = true;
    	}
        if (boss2 == null && singleUseAfterBoss2 == false){     
            secondStageCompleted.gameObject.SetActive(true);
            Destroy(barrierBoss2,2f);
            singleUseAfterBoss2 = true;
        }
        isHPfull = HPplayer > 9 ? true : false;
        animator.SetBool("FullHP", isHPfull);
    }
}
