using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health = 1;
    public int money = 1;
    public ParticleSystem particles;

    public float animationSpeed = .2f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Damage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject, .5f);
        MoneyManager.moneyManager.GainMoney(money);

        GetComponent<EnemyMovement>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.None;
        StartCoroutine("dieAnimation");
        particles.Play();

        GameManager.gameManager.enemies.Remove(gameObject);
    }

    private IEnumerator dieAnimation()
    {
        while (true)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - (animationSpeed * Time.deltaTime), transform.position.z);
            yield return null;
        }
    }

}
