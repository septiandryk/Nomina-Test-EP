using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolaTanah : MonoBehaviour
{
    private Vector3 mousePos;
    private Camera mainCam;
    private Rigidbody2D rb;
    public float force;
    private int hit;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        hit = 0;
    }

    void Update()
    {
        CheckHit();
    }

    //Ini untuk cek berapa kali bola sudah memantul
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            hit += 1;
            rb.velocity /= 2f;
        }
    }

    // Ini untuk menghancurkan bola 
    void CheckHit()
    {
        if (hit >= 4)
        {
            Destroy(gameObject);
        }
    }
}
