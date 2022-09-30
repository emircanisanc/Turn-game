using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Ball ball;

    [SerializeField]
    private OrbitPlayerListener playerListener;

    [SerializeField]
    private TMP_Text tmpTextScore;

    private int playerPoint;

    private int lastPoint;

    [SerializeField]
    private float killYMax;

    [SerializeField]
    private float killYMin;

    [SerializeField]
    private float killXMin;

    private bool isDead;

    public void addPoint(int point){
        playerPoint += lastPoint;
        lastPoint = point;
        tmpTextScore.text = playerPoint.ToString();
    }

    void Update(){
        if(playerListener.isPressingShoot()){
            ball.shootBall();
        }
        if(transform.position.y >= killYMax){
            killPlayer();
        }else if(transform.position.y <= killYMin){
            killPlayer();
        }else if(transform.position.x <= killXMin){
            killPlayer();
        }
    }

    private void killPlayer(){
        if(!isDead){
            isDead = true;
            FindObjectOfType<GameManager>().endGame(playerPoint);
        }
    }

}
