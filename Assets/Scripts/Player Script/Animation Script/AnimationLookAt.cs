using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationLookAt : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] public Rig rigWeight ;

    [Header("Turn Animation")]
    public Transform lookPoint, rightPoint, leftPoint, Character;
    public float duration = 1f;
    private float elapsedTime = 0f;
    private Vector3 startPosition;
    private Vector3 startRotation = new Vector3(0, 0, 0);
    private Vector3 newPosition;
    private Vector3 anim_rotation;
    private Vector3 rightPosition, leftPosition;
    private Turn _currentState = Turn.Idle;
    private float positionTolerance = 0.1f;
    private float t;

    [SerializeField] private bool isRotate = false;

    private void Start()
    {
        startPosition = lookPoint.transform.localPosition; 
        rightPosition = rightPoint.transform.localPosition;
        leftPosition = leftPoint.transform.localPosition;

        // rigWeight.weight = 0;
    }

    private void Update()
    {
        float yRotation = Character.eulerAngles.y;

        Debug.Log(yRotation);

        switch (_currentState)
        {
            case Turn.Idle:

                if(isRotate == false)
                {
                    if(yRotation >= 25f && yRotation <= 65f || yRotation <= 330f && yRotation >= 300f)
                    {
                        if(Input.GetKey("d"))
                        {
                            _currentState = Turn.Right;
                        }

                        if(Input.GetKey("a"))
                        {
                            _currentState = Turn.Left;
                        }
                    }

                    if( yRotation >= 30f && yRotation <= 60f || yRotation <= 150f && yRotation >= 120f)
                    {
                        if(Input.GetKey("w"))
                        {
                            _currentState = Turn.Left;
                        }

                        if(Input.GetKey("s"))
                        {
                            _currentState = Turn.Right;
                        }
                    }

                    if(yRotation <= 150f && yRotation >= 120f || yRotation >= 210f && yRotation <= 240f)
                    {
                        if(Input.GetKey("d"))
                        {
                            _currentState = Turn.Left;
                        }

                        if(Input.GetKey("a"))
                        {
                            _currentState = Turn.Right;
                        }
                    }

                    if(yRotation <= 330f && yRotation >= 300f || yRotation >= 210f && yRotation <= 240f)
                    {
                        if(Input.GetKey("w"))
                        {
                            _currentState = Turn.Right;
                        }

                        if(Input.GetKey("s"))
                        {
                            _currentState = Turn.Left;
                        }
                    }
                }
                

                if(Input.GetKey(KeyCode.Q))
                {
                    rigWeight.weight = 0;
                }
                else
                {
                    rigWeight.weight = 1;
                }

                if(Input.GetKey(KeyCode.Space))
                {
                    rigWeight.weight = 0;
                }
                else
                {
                    rigWeight.weight = 1;
                }

                break;
            case Turn.Right:
                        
                elapsedTime += Time.fixedDeltaTime;
                t = Mathf.Clamp01(elapsedTime / duration);

                newPosition = Vector3.Lerp(startPosition, rightPosition, t);

                transform.localPosition = newPosition;
                if(Vector3.Distance(transform.localPosition, rightPosition) < positionTolerance)
                {
                    _currentState = Turn.StopTurn;
                    StopTurn();
                }
                break;
            case Turn.Left:
                elapsedTime += Time.fixedDeltaTime;
                t = Mathf.Clamp01(elapsedTime / duration); 
            
                newPosition = Vector3.Lerp(startPosition, leftPosition, t);

                transform.localPosition = newPosition;

                if(Vector3.Distance(transform.localPosition, leftPosition) < positionTolerance)
                {
                    _currentState = Turn.StopTurn;
                    StopTurn();
                }
                break;
            case Turn.StopTurn:
                elapsedTime += Time.deltaTime;
                t = Mathf.Clamp01(elapsedTime / duration); 
            
                newPosition = Vector3.Lerp(newPosition, startPosition, t);

                transform.localPosition = newPosition;

                if(Vector3.Distance(transform.localPosition, startPosition) < positionTolerance)
                {
                    _currentState = Turn.Idle;
                    StopTurn();
                }
                break;
        }  
        Debug.Log(_currentState);
    }

    public void StopTurn()
    {
        elapsedTime = 0;
        isRotate = false;
    }

}
public enum Turn
    {
        Idle,
        Right,
        Left,
        StopTurn
    }