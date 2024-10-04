using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    public PhotonView pv;
    private Camera camera;
    public GameObject bulletPrefab; // Prefab de la bala
    public Transform firePoint;      // Punto desde donde se dispara la bala
    public float moveSpeed = 5f;     // Velocidad de movimiento del jugador
    public float fireRate = 3f;      // Intervalo de disparo en segundos
    public int health = 100;         // Salud del jugador
    public int damagePerSecond = 10;
    private Rigidbody2D rb;
    private float nextFireTime = 0f;
    private HashSet<Collider2D> enemiesInContact = new HashSet<Collider2D>();
    private float damageTimeAccumulator = 0f;
    public bool isDead = false;
    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        camera = GetComponentInChildren<Camera>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
    }

    private void Start()
    {
        camera.gameObject.SetActive(pv.IsMine);
    }

    private void Update()
    {
        Debug.Log(enemiesInContact.Count);
        if (pv.IsMine)
        {
            Move();
            CanShoot();
            ApplyDamage();
            if(isDead)
            {
                pv.RPC("HandleDeath", RpcTarget.AllBuffered);
            }
        }
    }
    public void Move()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, moveVertical, 0.0F);
        transform.Translate(movement * moveSpeed * Time.deltaTime);
    }
    public void CanShoot()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }
    [PunRPC]
    void HandleDeath()
    {
        gameObject.SetActive(false);

        GameManager.instance.OnPlayerDeath(this);
    }
    public void Shoot()
    {
        if (bulletPrefab && firePoint)
        {
            // Instanciar la bala
            GameObject bullet = PhotonNetwork.Instantiate(bulletPrefab.name, firePoint.position, Quaternion.identity);
            
            // Calcular la dirección hacia el ratón
            Vector2 direction = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

            // Configurar la dirección de la bala
            BulletController bulletController = bullet.GetComponent<BulletController>();
            if (bulletController)
            {
                bulletController.SetDirection(direction);
            }
        }
    }
    void ApplyDamage()
    {
        
            // Acumulador de tiempo para el cálculo del daño
        damageTimeAccumulator += Time.fixedDeltaTime;

        // Si hay enemigos en contacto y el acumulador ha alcanzado 1 segundo, aplicar daño
        if (enemiesInContact.Count > 0 && damageTimeAccumulator >= 1f)
        {
            int totalDamage = damagePerSecond * enemiesInContact.Count;
            health -= totalDamage;

            // Restablecer el acumulador
            damageTimeAccumulator = 0f;

            if (health <= 0)
            {
                Die();
            }
        }
        
    }

    void Die()
    {
        // Aquí puedes añadir efectos de muerte, animaciones, etc.
        isDead = true; // Destruye el jugador
    }
   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (pv.IsMine && other.CompareTag("Enemy"))
        {
            enemiesInContact.Add(other);
            Debug.Log(enemiesInContact.Count); // Imprime solo el número de enemigos en contacto
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (pv.IsMine && other.CompareTag("Enemy"))
        {
            enemiesInContact.Remove(other);
            Debug.Log(enemiesInContact.Count); // Imprime solo el número de enemigos en contacto
        }
    }

   
}
