using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField]
    private Transform follower;

    private Vector3 target;
    private Vector3 startPosition;
    private float lerpTime = 0.5f;
    private float timePass;

    [SerializeField]
    private Vector3 distanceFromTarget;

    public void setFollowerPosition(Vector3 targetPosition){
        startPosition = follower.position;
        target = new Vector3(targetPosition.x, targetPosition.y, -10) +  distanceFromTarget;
        if(timePass == 0){
            StartCoroutine(positionLerper());
        }
        timePass = 0;
    }

    IEnumerator positionLerper(){
        while(follower.position != target){
            follower.position = Vector3.Lerp(startPosition, target, timePass / lerpTime);
            timePass += Time.deltaTime;
            yield return new WaitForSeconds(0);
        }
        timePass = 0;
    }

}
