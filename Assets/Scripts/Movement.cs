using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento2D : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Captura las entradas de movimiento (horizontal y vertical)
        float movimientoHorizontal = Input.GetAxis("Horizontal");
        float movimientoVertical = Input.GetAxis("Vertical");

        // Calcula la dirección del movimiento
        Vector2 direccion = new Vector2(movimientoHorizontal, movimientoVertical).normalized;

        // Aplica el movimiento
        rb.velocity = direccion * velocidad;
    }
}
