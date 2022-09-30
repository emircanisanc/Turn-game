using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitPlayerListener : MonoBehaviour
{
    private int tapCount;

    public bool isPressingShoot(){
        if(Input.GetKeyDown(KeyCode.Space)){
            return true;
        }else if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began){
            tapCount++;
            return true;
        }else if(Input.GetMouseButtonDown(0)){
            return true;
        }
        return false;
    }    
}
