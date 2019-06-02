using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour {

    public Rigidbody rb;

    public float fowardForce = 2000f;
    public float movementForce = 500f;
    static public float moltiplicator = 0.03f;

    private void Start()
    {
        
    }

    void FixedUpdate () {
        if (fowardForce >= 5000)
        {
            moltiplicator = 0.001f;
        }

        movementForce += movementForce * Time.deltaTime * (moltiplicator * 1.2f);
        fowardForce += fowardForce * Time.deltaTime * moltiplicator;
        rb.AddForce(0, 0, fowardForce*Time.deltaTime);

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetTouch(0).position.x >= Screen.width / 2)
        {   
            rb.AddForce(movementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.GetTouch(0).position.x < Screen.width / 2)
        {
            rb.AddForce(-movementForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        if(rb.position.y < 0.5f)
        {
            FindObjectOfType<GameManager>().PlayerDead();
        }
    }

}
