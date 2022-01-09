using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController playerController;
    private float gravity = -9.81f;

    public float playerSpeed = 3.0f;

    private void Start()
    {
        playerController = GetComponent<CharacterController>();    
    }
    private void Update()
    {
        if (GameManager.Instance.GameState != GameState.Pause)
        { 
            float x = Input.GetAxis("Horizontal") * playerSpeed;
            float z = Input.GetAxis("Vertical") * playerSpeed;

            Vector3 playerMovement = new Vector3(x, gravity, z);
            playerMovement *= Time.deltaTime;
            playerMovement = transform.TransformDirection(playerMovement);
            playerController.Move(playerMovement);
        }
    }
}
