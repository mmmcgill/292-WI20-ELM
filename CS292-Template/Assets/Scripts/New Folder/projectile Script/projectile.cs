﻿    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class projectile : MonoBehaviour
{   
    private float minSpeed = 1;
    private float maxSpeed = 3;
    public GameObject slowdownIcon;
    private float slowDuration;
    public GameObject Particle;

    float speed;
    float TempSpeed;
    //gameObject to get Ruby position and current score (functionality on homing projectile)
    public Transform BlazeFox;
    private float changeDirectionTime; //change Direction Condition
    private Rigidbody2D rb;
    private Vector2 movement;
    private float angle;
  

    void Start(){
        speed = Random.Range(minSpeed, maxSpeed);
        TempSpeed = speed;
        rb = this.GetComponent<Rigidbody2D >();
        Vector2 direction = BlazeFox.position - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x)*Mathf.Rad2Deg;  
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
    
    }
    void OnTriggerEnter2D(Collider2D other){ //TODO
        BlazeFoxController controller = other.GetComponent<BlazeFoxController >();
        if(controller != null){
            controller.changeHealth(1);
        } 
        projectile noTouch = other.GetComponent<projectile >();
        if(noTouch != null){
            return; 
        }
        
        if(other.tag == "projectile platform"){
            speed = 0;
            TempSpeed = 0;
            return;
        }
        if(other.tag == "ground"){
           
            HighScore.instance.changeScore(1);
        }
        Instantiate(Particle, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()//TODO: redo code plz, this looks so ugly
    {
        //transform.Translate(Vector2.down*speed*Time.deltaTime);
        rb.MovePosition((Vector2)transform.position + (movement * speed* Time.deltaTime));
        
        if(slowDuration > 0 && slowdownIcon.activeSelf){
            slowDuration -= Time.deltaTime;
        }

        if(slowDuration <= 0 && slowdownIcon.activeSelf){
            speed = speed/5;
            slowDuration = 5;
        } 
        if(!slowdownIcon.activeSelf){
            speed = TempSpeed;
        }
    }
    
    void slowDown(){
        if(slowDuration < 0 && slowdownIcon.activeSelf){
            speed = speed/5;
            slowDuration = 5;
        }
    }
}
