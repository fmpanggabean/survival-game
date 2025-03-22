using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovement : MonoBehaviour
{
    public float movespeed;
    float speedx, speedy;
    Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        speedx = Input.GetAxisRaw("Horizontal") * movespeed;
        speedy = Input.GetAxisRaw("Vertical") * movespeed;
        rb.velocity = new Vector2 (speedx, speedy);
    }
}
