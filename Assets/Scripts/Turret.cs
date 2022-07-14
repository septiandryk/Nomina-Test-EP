using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    private Camera mainCam;
    private Vector3 mousePos;
    public Transform moncong;
    private bool canFire;
    private float timer;
    public float timeBetweenFire;

    public GameObject bolaApi;
    public GameObject bolaAir;
    public GameObject bolaTanah;

    private bool api;
    private bool air;
    private bool tanah;
    private bool canSwitch;


    void Start()
    {
        // Ini untuk mencari camera
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        api = true;
    }

    void Update()
    {
        // Untuk gerakin turret agar mengikuti mouse
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 rotation = mousePos - transform.position;

        // Untuk merubah rotasi
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);

        // Ini agar turret tidak bisa menembak secara cepat
        if (!canFire)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFire)
            {
                canFire = true;
                timer = 0;
            }
        }

        // Ini agar mengganti elemen tidak berjalan sekaligus
        if (!canSwitch)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFire)
            {
                canSwitch = true;
                timer = 0;
            }
        }

        // Ini untuk menembak
        if (Input.GetKeyDown(KeyCode.Space) && canFire && api)
        {
            canFire = false;
            Instantiate(bolaApi, moncong.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canFire && air)
        {
            canFire = false;
            Instantiate(bolaAir, moncong.position, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.Space) && canFire && tanah)
        {
            canFire = false;
            Instantiate(bolaTanah, moncong.position, Quaternion.identity);
        }

        // Ini untuk mengganti elemen peluru
        if (api == true && canSwitch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Api!");
                api = false;
                air = true;
                tanah = false;
                canSwitch = false;
            }
        }

        if (air == true && canSwitch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Air!");
                api = false;
                air = false;
                tanah = true;
                canSwitch = false;
            }
        }

        if (tanah == true && canSwitch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Tanah!");
                api = true;
                air = false;
                tanah = false;
                canSwitch = false;
            }
        }
    }
}
