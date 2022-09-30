using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitGenerator : MonoBehaviour
{
    [SerializeField]
    private OrbitController orbitPrefab;

    private Queue<OrbitController> orbitQueue;

    private OrbitController lastOrbit;

    [SerializeField]
    private Vector3 spaceBetweenOrbits;

    [SerializeField]
    private int loopLimit = 5;

    [SerializeField]
    private OrbitController firstOrbit;

    [SerializeField]
    private int orbitCount = 10;

    [SerializeField]
    private int maxSpeed = 700;

    [SerializeField]
    private int minSpeed = 300;

    private int speed;

    [SerializeField]
    private bool random;

    [SerializeField]
    private int speedUp = 5;

    void Awake(){
        orbitQueue = new Queue<OrbitController>();
        random = PlayerPrefs.GetInt("random", 0) == 1;
        createOrbits();
    }

    public void createOrbits(){
        lastOrbit = firstOrbit;
        if(!lastOrbit.getOrbitGenerator()){
            lastOrbit.setOrbitGenerator(this);
        }
        speed = minSpeed;
        lastOrbit.refreshOrbit(speed, findPoint(minSpeed));
        addToQueue(lastOrbit);
        for(int i = 0; i < orbitCount; i++){
            lastOrbit = Instantiate(orbitPrefab, lastOrbit.transform.position + spaceBetweenOrbits, new Quaternion(0, 0 ,0 ,0));
            if(!lastOrbit.getOrbitGenerator()){
                lastOrbit.setOrbitGenerator(this);
            }
            if(random){
                speed = getRandomSpeed();
                lastOrbit.refreshOrbit(speed, findPoint(speed));
            }else{
                speed += speedUp;
                lastOrbit.refreshOrbit(speed, findPoint(speed));
            }
            
        }
    }

    public void addToQueue(OrbitController newOrbit){
        orbitQueue.Enqueue(newOrbit);
        if(orbitQueue.Count > loopLimit){
            var tempOrbit = orbitQueue.Dequeue();
            if(random){
                speed = getRandomSpeed();
                lastOrbit.refreshOrbit(speed, findPoint(speed));
            }else{
                speed += speedUp;
                var tempSpeed = speed;
                if(Random.Range(0, 2) == 1){
                   tempSpeed = -speed;
                }
                lastOrbit.refreshOrbit(tempSpeed, findPoint(speed));
            }
            tempOrbit.transform.position = lastOrbit.transform.position + spaceBetweenOrbits;
            lastOrbit = tempOrbit;
        }
    }

    private int getRandomSpeed(){
        if(Random.Range(0, 2) == 1){
            return Random.Range(-maxSpeed, -minSpeed);
        }else{
            return Random.Range(minSpeed, maxSpeed);
        }
        
    }

    private int findPoint(int speed){
        if(speed > maxSpeed){
            return (int)(Mathf.InverseLerp(minSpeed, maxSpeed, Mathf.Abs(speed)) * 10) + Mathf.Abs(speed) - maxSpeed;
        }else{
            return (int)(Mathf.InverseLerp(minSpeed, maxSpeed, Mathf.Abs(speed)) * 10);
        }
        
    }
}
