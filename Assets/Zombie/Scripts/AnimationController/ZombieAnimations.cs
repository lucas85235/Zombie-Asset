using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAnimations : MonoBehaviour, IZombieAnimations
{
    private Animator _anim;
    public AnimatorStateInfo info { get; private set; }

    void Awake()
    {
        _anim = GetComponent<Animator>();
        info = _anim.GetCurrentAnimatorStateInfo(0);
    }

    public void Idle()
    {
        _anim.SetBool("CanMove", false);
    }

    public void Walk(float blendSpeed = 0)
    {
        _anim.SetBool("CanMove", true);
        _anim.SetFloat("BlendMove", blendSpeed);
    }

    public void Attack()
    {
        var randA = Random.Range(0, 4);

        _anim.SetInteger("ChoiceAttack", randA);
        _anim.SetTrigger("Attack");
    }

    public void HitReaction()
    {
        var randH = Random.Range(0, 2);

        _anim.SetInteger("RandHit", randH);
        _anim.SetTrigger("Hit");
    }

    public void Death()
    {
        _anim.StopPlayback();

        if (_anim.GetCurrentAnimatorStateInfo(0).IsTag("Death"))
        {
            _anim.SetBool("CanMove", false);
            _anim.Play("Idle", 0);
        }
        else
        {
            var randD = Random.Range(0, 3);

            _anim.SetInteger("RandDeath", randD);
            _anim.SetTrigger("Death");
        }
    }
}
