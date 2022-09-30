using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rotator))]
public class OrbitController : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    private bool isDone = false;

    [SerializeField]
    private int point;

    private OrbitGenerator orbitGenerator;
    private Rotator rotator;

    [SerializeField]
    private TMP_Text tmpText;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private SpriteRenderer childsSpriteRenderer;

    [SerializeField]
    private Color[] colorsZeroToTen;
    
    [SerializeField]
    private AudioClip hitSound;

    public void setOrbitGenerator(OrbitGenerator orbitGenerator){
        this.orbitGenerator = orbitGenerator;
    }

    public bool getOrbitGenerator(){
        return orbitGenerator;
    }

    public void refreshOrbit(int speed, int newPoint){
        if(rotator){
            rotator.setSpeed((float)speed);
        }
        
        refreshColorBySpeed(newPoint);
        refreshPoint(newPoint+1);
        isDone = false;
    }

    private void refreshColorBySpeed(int newPoint){
        if(newPoint < colorsZeroToTen.Length){
            spriteRenderer.color = colorsZeroToTen[newPoint];
            childsSpriteRenderer.color = spriteRenderer.color - new Color(0, 0, 0, 0.8f);
        }else{
            spriteRenderer.color = colorsZeroToTen[Random.Range(0, colorsZeroToTen.Length)];
            childsSpriteRenderer.color = spriteRenderer.color - new Color(0, 0, 0, 0.8f);
        }
        
    }

    private void refreshPoint(int point){
        this.point = point;
        tmpText.text = this.point.ToString();
    }

    void Awake(){
        rotator = GetComponent<Rotator>();
    }
/*
    void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == playerTag){
            var rg2D = other.attachedRigidbody;
            rg2D.velocity = Vector2.zero;
            other.transform.SetParent(transform);
            if(!isDone){
                if(hitSound){
                    AudioSource.PlayClipAtPoint(hitSound, transform.position, 1);
                }
                other.GetComponent<PlayerManager>().addPoint(point);
                isDone = true;
                if(orbitGenerator){
                    orbitGenerator.addToQueue(this);
                }
            }
        }
    }
*/
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == playerTag){
            var rg2D = other.rigidbody;
            rg2D.velocity = Vector2.zero;
            other.transform.SetParent(transform);
            if(!isDone){
                if(hitSound){
                    AudioSource.PlayClipAtPoint(hitSound, transform.position, 1);
                }
                other.gameObject.GetComponent<PlayerManager>().addPoint(point);
                isDone = true;
                if(orbitGenerator){
                    orbitGenerator.addToQueue(this);
                }
            }
        }
    }

}
