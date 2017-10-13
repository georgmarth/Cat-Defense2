using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnCat : MonoBehaviour
{

    public float turnSpeed = 180f;
    public float range = 1f;
    public LayerMask layerMask;

    private Rigidbody rb;
    [HideInInspector]
    public Transform enemy;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        findEnemy();
        Turn();
    }

    private void findEnemy()
    {
        if (enemy != null)
        {
            if (Vector3.Distance(rb.position, enemy.transform.position) <= range)
                return;
            // if the enemy is not in range anymore, delete the reference
            enemy = null;
        }
        float distance = 0;

        Collider[] colliders = Physics.OverlapSphere(rb.position, range, layerMask);
        foreach (Collider col in colliders)
        {
            Transform colTran = col.gameObject.transform;
            if (enemy == null || Vector3.Distance(rb.position, colTran.position) < distance)
            {
                enemy = colTran;
                distance = Vector3.Distance(rb.position, colTran.position);
            }
        }
    }

    private void Turn()
    {
        if (enemy == null)
            return;
        float turnDistance = turnSpeed * Time.deltaTime;
        Quaternion desiredRotation = Quaternion.LookRotation(enemy.position - rb.position);
        rb.rotation = Quaternion.RotateTowards(rb.rotation, desiredRotation, turnDistance);
    }

    public bool inRange()
    {
        return (enemy != null);
    }
}
