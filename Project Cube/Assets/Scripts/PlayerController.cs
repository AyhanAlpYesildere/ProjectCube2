using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;
    Vector3 direction;
    public float forwardSpeed;
    public float maxSpeed;
    int desiredLane = 1; // Left: 0 Middle: 1 Right: 2
    public float laneDistance = 4;
    public float jumpForce;
    public float Gravity = -20;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        direction.z = forwardSpeed;
        direction.y += Gravity * Time.deltaTime;

        if(PlayerManager.isGameStarted == true)
        {

            if (forwardSpeed < maxSpeed)
            {
            forwardSpeed += 0.5f * Time.deltaTime;
            }
            
        }

        if(controller.isGrounded)
        {
            //if(Input.GetKeyDown(KeyCode.UpArrow))
            if(SwipeManager.swipeUp)
            {
                Jump();
            }
        }
        else 
        {
            direction.y += Gravity * Time.deltaTime;
        }

        //if(Input.GetKeyDown(KeyCode.RightArrow))
        if(SwipeManager.swipeRight)
        {
            desiredLane++;
            if(desiredLane == 3)
            {
                desiredLane = 2;
            }
        }

        //if(Input.GetKeyDown(KeyCode.LeftArrow))
        if(SwipeManager.swipeLeft)
        {
            desiredLane--;
            if(desiredLane == -1)
            {
                desiredLane = 0;
            }
        }

        //if(Input.GetKeyDown(KeyCode.DownArrow))
        if(SwipeManager.swipeDown)
        {
            direction.y -= jumpForce;
        }

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;

        if(desiredLane == 0)
        {
            targetPosition += Vector3.left * laneDistance;
        }
        else if(desiredLane == 2)
        {
            targetPosition += Vector3.right * laneDistance;
        }

        //transform.position = Vector3.Lerp(targetPosition, targetPosition, 80 * Time.deltaTime);
        if (transform.position == targetPosition)
        {
            return;
        }
            Vector3 diff = targetPosition - transform.position;
            Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
            if(moveDir.sqrMagnitude < diff.sqrMagnitude)
            {
                controller.Move(moveDir);
            }
            else
            {
                controller.Move(diff);
            }
        
    }

    void FixedUpdate()
    {
        if(!PlayerManager.isGameStarted)
        {
            return;
        }
        controller.Move(direction * Time.fixedDeltaTime);
    }

    void Jump()
    {
        direction.y = jumpForce;
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.tag == "Obstacle")
        {
            PlayerManager.gameOver = true;
        }
        if(hit.transform.tag == "UzunObstacle")
        {
            PlayerManager.gameOver = true;
        }
    }
}
