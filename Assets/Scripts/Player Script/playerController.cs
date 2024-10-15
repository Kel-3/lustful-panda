using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
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
    [SerializeField]
    AnimationCurve _rollCurve;

    private CharacterController characterController;
    private float ySpeed;
    private bool _isRun;

    private bool isRooling;
    private float _rollTimer;
    private float _speed;
    private float hInput;
    private float vInput;
    private Vector3 move;
    private Vector3 velocity;


    public CharacterAnimatorController CharacterAnimatorController;
    [SerializeField] private float _animeSmoothSpeed = 2;
    [SerializeField] private float _animHorizontal, _animVertical;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        CharacterAnimatorController = GetComponent<CharacterAnimatorController>();

        Keyframe roll_lastFrame = _rollCurve[_rollCurve.length - 1];
        _rollTimer = roll_lastFrame.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRooling)
        {
        gameObject.tag = "PandaMC";
        hInput = Input.GetAxis("Horizontal");
        vInput = Input.GetAxis("Vertical");

        _speed = _walkSpeed;

        move = new Vector3(hInput, 0, vInput);

        //Run
        Running();

        float magnitude = Mathf.Clamp01(move.magnitude) * _speed;
        move.Normalize();

        ySpeed += Physics.gravity.y * Time.deltaTime;


        velocity = move * magnitude;
        velocity.y = ySpeed;

            characterController.Move(velocity * Time.deltaTime);
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, _rotationSpeed * Time.deltaTime);
        }

        //Jump
        Jump();

        //Rolling
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (velocity.magnitude != 0) StartCoroutine(Rolling());
            }
        }


        AnimateWalkRun(new Vector3(hInput, vInput, 0));
        AnimateJump();


        //transform.Rotate(rotation * Time.deltaTime * _rotationSpeed);
    }

    IEnumerator Rolling()
    {
        isRooling = true;
        gameObject.tag = "PandaRolling";
        float timer = 0;
        while (timer < _rollTimer) {
            float _rollSpeed = _rollCurve.Evaluate(timer);
            Vector3 dir = (transform.forward * _rollSpeed);
            characterController.Move(dir * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
        isRooling = false;
    }

    private void Running()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            _speed = _runSpeed;
            _isRun = true;
        }
    }

    private void Jump()
    {
        if (characterController.isGrounded)
        {
        ySpeed = 0f;
            if (Input.GetKey(KeyCode.Space))
            {
                ySpeed = _jumpSpeed;
                _isJump = true;
            }
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
