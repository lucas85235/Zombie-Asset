using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class ZombieIA : IZombieIA
{
    private NavMeshAgent _nav;
    private Transform _target;
    private IZombieAnimations _animations;
    
    public float currentLife { get; private set; }
    private bool canDamage = true;

    private bool isDeath = false;
    public override bool IsDeath { get => isDeath; }

    [Header("Life Settings")]
    public Slider lifeBar;
    public float maxLife = 100f;

    [Header("Behaviour Settings")]
    public float attackDistace = 1.25f;
    public float followDistance = 18;
    public bool asFollowDistance = true;

    [Header("Damage Settings")]
    public float damage = 5f;
    public float damageWait = 1f;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _animations = GetComponent<IZombieAnimations>();
        _nav = GetComponent<NavMeshAgent>();

        if (lifeBar != null) lifeBar.maxValue = maxLife;
        UpdateLife(maxLife);

        _nav.SetDestination(_target.position);
    }

    private void FixedUpdate()
    {
        if (isDeath) return;

        // can follow target
        if (Distance(_target) <= followDistance || !asFollowDistance) 
        {
            // can attack
            if (Distance(_target) <= attackDistace)  
            {
                if (canDamage) 
                {
                    canDamage = false;
                    _animations.Attack();

                    // Look at target
                    var lookPos = _target.position;
                    lookPos.y = 0;
                    transform.LookAt(lookPos);

                    StartCoroutine(DamageInPlayer());                
                }

                _nav.isStopped = true;
            }
            // can walk
            else if (Distance(_target) > attackDistace)
            {
                _nav.isStopped = false;
                _nav.SetDestination(_target.position);
                _animations.Walk();
            }            
        }
        // not moving
        else
        {
            _animations.Idle();
            _nav.isStopped = true; // Ativa comportamento sem target
        }
    }

    protected override void TakeDamage(float damage) 
    {
        currentLife -= damage;

        if (currentLife > maxLife) 
        {
            currentLife = maxLife;
        }
        else if (currentLife <= 0) 
        {
            isDeath = true;
            DeathBehaviout();
        }

        if (lifeBar != null) lifeBar.value = currentLife;
        if (isDeath == false) _animations.HitReaction();
    }
    
    protected override void UpdateLife(float newLife) 
    {
        currentLife = newLife;

        if (currentLife > maxLife) 
        {
            currentLife = maxLife;
        }
        else if (currentLife <= 0) 
        {
            isDeath = true;
            DeathBehaviout();
        }

        if (currentLife > 0 && isDeath) isDeath = false;
        if (lifeBar != null) lifeBar.value = currentLife;
    }

    public void DeathBehaviout() 
    {
        Debug.Log("Zombie Morreu");
        isDeath = true;
        _nav.isStopped = true;
        _animations.Death();
    }

    private float Distance(Transform point) 
    {
        return Vector3.Distance(transform.position, point.position);
    }

    private IEnumerator DamageInPlayer() 
    {
        Debug.Log("Dano no player");
        yield return new WaitForSeconds(0.2f);
        yield return new WaitUntil( () => !_animations.info.IsTag("Attack"));
        yield return new WaitForSeconds(damageWait);
        canDamage = true;
    }
}
