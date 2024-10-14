using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void WalkSpeed(float horizontal,float vertical)
    {
        _animator.SetFloat("horizontal", horizontal);
        _animator.SetFloat("vertical", vertical);
    }

    public void Jump()
    {
        _animator.SetBool("isJump", true);
    }

    public void Land()
    {
        _animator.SetBool("isJump", false);
    }

    public void Roll()
    {
        _animator.SetBool("isRolling", true);
    }

    public void StopRoll()
    {
        _animator.SetBool("isRolling", false);
    }

}
