using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]

    [SerializeField] private float playerSpeed = 2.0f;
    [SerializeField] private float jumpHeight = 1.0f;
    [SerializeField] private LayerMask playerMask;
    
    public Transform groundCheck;
    
    private Camera cam;
    private CharacterController controller;
    private Vector3 playerVelocity;
    
    private bool groundedPlayer;
    private float horizontalMove, verticalMove, gravityValue = -9.81f;
    
    // Start is called before the first frame update
    void Start()    
    {
        cam = gameObject.GetComponentInChildren<Camera>();
        controller = gameObject.GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        groundedPlayer = Physics.OverlapSphere(groundCheck.position, 0.25f, playerMask).Length == 1;

        if (!controller.enabled && !groundedPlayer)
        {
            controller.enabled = enabled;

            playerVelocity.y += gravityValue * Time.deltaTime;
            controller.Move(playerVelocity * Time.deltaTime);
            
            controller.enabled = !enabled;
        }
        
        else if (!controller.enabled)
        {
            return;
        }


        Move();

        Jump();
    }

    private void Move()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal");
        verticalMove = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(horizontalMove, 0, verticalMove);
        move *= playerSpeed;
        move = transform.rotation * move;
        controller.Move(move * Time.deltaTime);
    }

    private void Jump()
    {
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }     

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

}

