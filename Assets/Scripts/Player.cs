using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Player : MonoBehaviour
{
    private int speed = 25;
    private int turnSpeed = 60;
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;
    private bool canMove = true;
    
    private bool collided = false;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Init();
    }

    public void Init()
    {
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        canMove = true;

    }

    void Update()
    {
        if (transform.position.y < -10)
        {
            Init();
        }

        if (canMove)
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.forward * (Time.deltaTime * verticalInput * speed));
            transform.Rotate(Vector3.up, Time.deltaTime * horizontalInput * turnSpeed);
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (!collided  &&(collision.gameObject.CompareTag("Bus") ||  collision.gameObject.CompareTag("Obstacle"))) // 
        {
            Debug.Log("Collision Detected with " + collision.gameObject.name);
            canMove = false;
            collided = true;

            GameSceneManager.Instance.MakeApiCall();
        }
    }
}
