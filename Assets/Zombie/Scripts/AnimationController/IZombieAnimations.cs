using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IZombieAnimations
{
    AnimatorStateInfo info { get; }

    void Idle();
    void Walk(float blendSpeed = 0);
    void Attack();
    void HitReaction();
    void Death();
}
