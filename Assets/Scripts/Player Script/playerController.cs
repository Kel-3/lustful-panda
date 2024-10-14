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

    private bool _isRun;


    public CharacterAnimatorController CharacterAnimatorController;
    [SerializeField] private float _animeSmoothSpeed = 2;
    [SerializeField] private float _animHorizontal, _animVertical;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        CharacterAnimatorController = GetComponent<CharacterAnimatorController>();
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
    
            _isRun = true;
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

        AnimateWalkRun(new Vector3(hInput, vInput, 0));
        AnimateJump();

        if(Input.GetKey(KeyCode.E))
        {
            ySpeed = 0;
            vInput = 0;
            hInput = 0;
            speed = 0;
        }

        //transform.Rotate(rotation * Time.deltaTime * _rotationSpeed);
    }

    private void jump()
    {
        ySpeed = 0f;

        if (Input.GetKey(KeyCode.Space))
        {
            ySpeed = _jumpSpeed;
            _isJump = true;
        }
    }

#region animation
    private JumpState _currentState = JumpState.Grounded;
    private bool _isJump = false;

    private void AnimateJump()
    {
        switch (_currentState)
        {
            case JumpState.Grounded:
                if(_isJump == true)
                {
                    _currentState = JumpState.Jump;
                }
                break;
            case JumpState.Jump:
                CharacterAnimatorController.Jump();
                _currentState = JumpState.Falling;
                Debug.Log("jump");
                break;
            case JumpState.Falling:
                CharacterAnimatorController.Land();
                _isJump = false;
                _currentState = JumpState.Grounded;
                Debug.Log("tidak jump");
                break;
        }
    }

    private void AnimateWalkRun(Vector3 input) 
    {
        float multiplier = Input.GetKey(KeyCode.LeftShift) ? 3 : 1.5f;
        float targetHorizontal = input.x * multiplier;
        float targetVertical = input.y * multiplier;

        _animHorizontal = Mathf.Lerp(_animHorizontal, targetHorizontal, Time.deltaTime * _animeSmoothSpeed);
        _animVertical = Mathf.Lerp(_animVertical, targetVertical, Time.deltaTime * _animeSmoothSpeed);

        CharacterAnimatorController.WalkSpeed(_animHorizontal, _animVertical);
    }
#endregion
}

public enum JumpState
{
    Grounded,
    Jump,
    Falling
}
