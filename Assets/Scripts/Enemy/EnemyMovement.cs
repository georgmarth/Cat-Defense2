using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour 
{

    public float speed = 2f;
    public float turnSpeed = 180f;
    public float distanceToTurn = 0.2f;
    public float turnJitter = 20f;
    public GameObject path;
    public int damage = 1;

    private Transform[] nodes;
    private int nextNode = 1;
    private Transform currentNode;
    private Rigidbody rb;

	void Start () 
    {
        rb = GetComponent<Rigidbody>();
        nodes = path.gameObject.GetComponentsInChildren<Transform>();
        FindNextNode();
	}
	
	void FixedUpdate () 
    {
        Turn();
        MoveForward();
	}

    private void FindNextNode()
    {
        currentNode = transform;
        // if the next node exceeds the number of nodes, reach end of route.
        if (nodes.Length <= nextNode)
        {
            ReachEnd();
            return;
        }
        currentNode = nodes[nextNode];
        nextNode++;
    }

    private void MoveForward()
    {
        
        // Vector3 direction = (currentNode.position - rb.position).normalized;

        float distanceToNode = Vector3.Distance(rb.transform.position, currentNode.position);
        if (distanceToNode < distanceToTurn)
        {
            FindNextNode();
        }

        rb.position = rb.position + (rb.transform.forward * speed * Time.deltaTime);
        //rb.velocity = rb.transform.forward * speed;
    }

    private void ReachEnd()
    {
        Destroy(gameObject);
        HealthManager.healthManager.Damage(damage);
        GameManager.gameManager.enemies.Remove(gameObject);
    }

    private void Turn()
    {
        float turnRadius = turnSpeed * Time.deltaTime;

        Vector3 direction = currentNode.position - rb.position;

        float jitter = Random.Range(-turnJitter, turnJitter);
        Quaternion lookRotation = Quaternion.LookRotation(direction);

        Vector3 eulerAngles = new Vector3(lookRotation.eulerAngles.x, lookRotation.eulerAngles.y + jitter, lookRotation.eulerAngles.z);

        lookRotation.eulerAngles = eulerAngles;
        rb.rotation = Quaternion.RotateTowards(rb.rotation, lookRotation, turnRadius);
    }
}
