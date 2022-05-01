using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Marco : MonoBehaviour
{
    
    public Rigidbody2D rigid;
    public float moveSpeed;
    public float moveSpeedBase;
    public SpriteRenderer sprite;
    public bool indo;
    public Animator anime;
    public string animaIndo, animaVindo, animaHorizontal; 
    public bool isVertical;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    if(isVertical){
        if(indo){
            moveSpeed = moveSpeedBase;
            anime.Play(animaIndo);
        } else{
            moveSpeed = -moveSpeedBase;
            anime.Play(animaVindo);
            }   
            rigid.velocity = transform.TransformDirection(Vector2.up * moveSpeed);
        } 
        else{
            if(indo){
            moveSpeed = moveSpeedBase;
        } else{
            moveSpeed = -moveSpeedBase;
            }  
            sprite.flipX = !indo; 
            anime.Play(animaHorizontal);
            rigid.velocity = transform.TransformDirection(Vector2.right * moveSpeed);
        }
    }
     void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("vira")){
           indo = !indo;
        }
     }
}
