using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CatAttack : MonoBehaviour {

    public float damage = 1f;
    public float range = 1f;
    public float coolDown = 0.5f;
    public LayerMask layerMask;

    protected TurnCat turnScript;

    protected abstract void Attack();

    private float coolDownTimer = 0f;

    virtual protected void Start()
    {
        turnScript = GetComponent<TurnCat>();
    }

    private void FixedUpdate()
    {
        coolDownTimer = Mathf.Clamp(coolDownTimer - Time.deltaTime, 0, Mathf.Infinity);
        if (coolDownTimer == 0f && turnScript.inRange())
        {
            if ((turnScript.enemy.transform.position - transform.position).magnitude <= range)
            {
                Attack();
                coolDownTimer = coolDown;
            }
        }
    }

}
