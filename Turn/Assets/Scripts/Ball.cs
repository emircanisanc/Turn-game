using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Follower))]
public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speed = 7;

    private Rigidbody2D rg2D;
    private Follower follower;

    void Awake(){
        rg2D = GetComponent<Rigidbody2D>();
        follower = GetComponent<Follower>(); 
    }

    public void shootBall(){
        if(transform.parent){
            var direction = transform.position - transform.parent.position;
            direction.Normalize();
            transform.SetParent(null);
            StartCoroutine(protectVelocity(new Vector2(direction.x, direction.y)));
        }
    }

    IEnumerator protectVelocity(Vector2 direction){
        while(transform.parent == null){
            rg2D.velocity = direction * speed;
            yield return new WaitForSeconds(0);
        }
        follower.setFollowerPosition(transform.parent.position);
    }
}
