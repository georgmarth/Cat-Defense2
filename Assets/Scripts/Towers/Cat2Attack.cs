using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat2Attack : CatAttack {

    private TurnCat turnCat;
    private LineRenderer lineRenderer;

    public Transform attackTransform;
    public float laserVisibility = 0.1f;

    public AudioClip hitClip;

	// Use this for initialization
    protected override void Start()
    {
        base.Start();
        turnCat = GetComponent<TurnCat>();
        lineRenderer = GetComponent<LineRenderer>();
    }
	
    protected override void Attack()
    {
        Transform enemy = turnCat.enemy;
        RaycastHit hit;
        Physics.Raycast(attackTransform.position, (enemy.position - attackTransform.position).normalized, out hit);
        if (hit.transform != null)
        {
            SoundManager.soundManager.sfx.clip = hitClip;
            SoundManager.soundManager.sfx.Play();

            Vector3[] positions = new Vector3[] { attackTransform.position, enemy.position };
            lineRenderer.SetPositions(positions);
            lineRenderer.enabled = true;
            Invoke("DisableLineRenderer", laserVisibility);
            if (hit.transform.gameObject.tag == "Enemy")
            {
                EnemyHealth enemyHealth = hit.transform.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.Damage(damage);
                }
            }
        }
    }

    void DisableLineRenderer()
    {
        lineRenderer.enabled = false;
    }
}
