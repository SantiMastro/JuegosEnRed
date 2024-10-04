using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f; // Velocidad de la bala
    public float lifetime = 5f; // Tiempo de vida de la bala

    private Vector2 direction;

    void Start()
    {
        Destroy(gameObject, lifetime); // Destruye la bala después de un tiempo
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyController enemy = other.GetComponent<EnemyController>();
            if (enemy != null)
            {
                enemy.TakeDamage(50); // Inflige 50 de daño al enemigo
                Destroy(gameObject); // Destruye la bala al colisionar
            }
        }
    }
}
