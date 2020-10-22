using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IZombieIA : MonoBehaviour
{
    public virtual bool IsDeath { get; }

    protected abstract void TakeDamage(float damage);
    protected abstract void UpdateLife(float damage);
}
