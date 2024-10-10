using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [SerializeField]
    private float _jumpSpeed = 3f;

    [SerializeField]
    private float _walkSpeed = 3f;

    [SerializeField]
    private float _runSpeed = 6f;

    [SerializeField]
    private float _rotationSpeed = 90f;

    private CharacterController characterController;

    private float ySpeed;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        float speed = _walkSpeed;

        //Vector3 rotation = new Vector3(0, hInput);

        //Run
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = _runSpeed;
        }

        Vector3 move = new Vector3(hInput, 0, vInput);
        float magnitude = Mathf.Clamp01(move.magnitude) * speed;
        move.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;

        //Jump
        if (characterController.isGrounded)
        {
            jump();
        }

        Vector3 velocity = move * magnitude;

        velocity.y = ySpeed;


        characterController.Move(velocity * Time.deltaTime);

        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }


        //transform.Rotate(rotation * Time.deltaTime * _rotationSpeed);
    }

    private void jump()
    {
        ySpeed = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            ySpeed = _jumpSpeed;
        }
    }
}
