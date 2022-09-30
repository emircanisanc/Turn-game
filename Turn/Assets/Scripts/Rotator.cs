using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField]
    private float speed = 200;

    [SerializeField]
    private Space space = Space.World;

    void Update()
    {
        transform.Rotate(Vector3.forward * speed * Time.deltaTime, space);
    }

    public void setSpeed(float speed){
        this.speed = speed;
    }
}
