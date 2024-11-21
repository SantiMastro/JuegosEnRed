using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{
    public Transform player;
    public Transform pointShoot;

    private Camera cam;

    void Start()
    {
        cam = player.GetComponentInChildren<Camera>();
    }

    void Update()
    {
        PointShoot();
    }

    private void PointShoot()
    {
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        Vector3 direction = mousePosition - pointShoot.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pointShoot.rotation = Quaternion.Euler(0, 0, angle);
    }
}
