using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPandaStateController : MonoBehaviour
{
    private NPCPanda _currentState = NPCPanda.Idle;
    [SerializeField] private Animator _animator;
    [SerializeField] private float acceleration = 1f; //naikin kalo mau animasi rest & sit lebih cepet
    private float velocity = 0.0f;

    [SerializeField] private float _idleTime = 6f; //naikin kalo mau animasi idle lebih lama
    private float _timeBeforeIdle = 0; 
    private bool _isIdle = false;


    void Update()
    {
        switch (_currentState)
        {
            case NPCPanda.Idle:
                _animator.SetBool("isIdle", false);
                _timeBeforeIdle += Time.deltaTime;

                if(_timeBeforeIdle > _idleTime)
                {
                    _animator.SetBool("isIdle", true);
                    RandomIdles();
                    Debug.Log("Idle"); 
                    _timeBeforeIdle = 0;
                }
                break;
            case NPCPanda.Sit:
                _animator.SetFloat("Idle", 1.1f);
                VelocityCounting();

                if (velocity > 5)
                {
                    _currentState = NPCPanda.UpSit;
                }
                break;
            case NPCPanda.UpSit:
                _animator.SetFloat("Idle", 5.1f);
                _currentState = NPCPanda.Idle;
                VelocityReset();
                break;
            case NPCPanda.Rest:
                _animator.SetFloat("Idle", 0.1f);

                VelocityCounting();
                if (velocity > 5)
                {
                    _currentState = NPCPanda.UpRest;
                }
                break;
            case NPCPanda.UpRest:
                _animator.SetFloat("Idle", 5.1f);
                _currentState = NPCPanda.Idle;
                VelocityReset();
                break;
        }

        Debug.Log(_timeBeforeIdle);
        Debug.Log(_currentState);
        Debug.Log(velocity);
    }

    private void RandomIdles()
    {
        float rd = Random.Range(0f, 2f);

        if(rd < 1)
        {
            _currentState = NPCPanda.Rest;
        } 
        else 
        {
            _currentState = NPCPanda.Sit;
        }
    }

    private float VelocityReset()
    {
        return velocity = 0.0f;
    }

    private void VelocityCounting()
    {
        velocity += Time.deltaTime * acceleration;
    }

    private void ResetAnimator()
    {
        _animator.SetFloat("Idle", 0f);
    }
}


public enum NPCPanda
{
    Idle,
    Sit,
    UpSit,
    Rest,
    UpRest 
}