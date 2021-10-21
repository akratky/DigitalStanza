using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 1;
    public float gravity = -10;

    public Transform groundCheck;
    public float groundDistance = 0.1f;
    public LayerMask groundMask;

    private CharacterController character;
    private Vector3 velocity;
    private bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // apply gravity
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -0.2f;
        }
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        character.Move(move * speed * Time.deltaTime);


        // now apply gravity
        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
    }
}
