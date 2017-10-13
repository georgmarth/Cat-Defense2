using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat1Attack : CatAttack {

    public Transform attackTrans;
    public float burstRadius = 0.2f;
    public AudioClip hitClip;

    private Animator animator;
    public ParticleSystem particles;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();;
    }

    protected override void Attack()
    {
        animator.SetTrigger("Attack");
    }

    public void Damage()
    {
        particles.Play();
        SoundManager.soundManager.sfx.clip = hitClip;
        SoundManager.soundManager.sfx.Play();
        Collider[] colliders = Physics.OverlapSphere(attackTrans.position, burstRadius, layerMask);
        foreach (Collider col in colliders)
        {
            EnemyHealth enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(damage);
            }
        }
    }
}
