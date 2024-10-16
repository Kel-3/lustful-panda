using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCPandaAnimatorController : StateMachineBehaviour
{
    [SerializeField] private float _timeUntilIdle;
    [SerializeField] private float _idleChangeTime = 5f;

    [SerializeField] private int _numberIdleAnimations;
    private bool _isIdle;
    private float _idleTime;
    private int _idleAnimation;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_isIdle == false)
        {
            _idleTime += Time.deltaTime;

            if(_idleTime > _timeUntilIdle && stateInfo.normalizedTime % 1 < 0.02f)
            {
                _idleAnimation = Random.Range(1, _numberIdleAnimations + 1);
                _isIdle = true;

                if(_idleAnimation == 2)
                {
                    animator.SetFloat("IdleRest", 0);
                } 
                else if(_idleAnimation == 3)
                {
                    animator.SetFloat("IdleSit", 0);
                }

                animator.SetFloat("Idle", _idleAnimation);
            }
        }
        else if (stateInfo.normalizedTime % 1 > 0.98)
        {
            //_idleChangeTime 
            // if(_idleAnimation == 2)
            // {
            //     animator.SetFloat("IdleRest", 1);
            // } 
            // else if(_idleAnimation == 3)
            // {
            //     animator.SetFloat("IdleSit", 1);
            // }


            // Debug.Log("reset");
            //ResetIdle();
        }

        if(Input.GetKey(KeyCode.I))
        {
            ResetIdle();
        }

        //animator.SetFloat("Idle", _idleAnimation, 0.2f, Time.deltaTime);
       
       Debug.Log(_idleTime);
       Debug.Log(_idleAnimation);

    }

    private void ResetIdle()
    {
        _isIdle = false;
        _idleTime = 0;
        _idleAnimation = 0; 
    }
}
