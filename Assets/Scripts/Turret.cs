using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public GameObject txtApi;
    public GameObject txtAir;
    public GameObject txtTanah;

    private bool api;
    private bool air;
    private bool tanah;
    private bool canSwitch;


    void Start()
    {
        // Ini untuk mencari camera
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        api = true;
        txtApi.SetActive(true);
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
                api = false;
                air = true;
                tanah = false;
                canSwitch = false;
                txtApi.SetActive(false);
                txtAir.SetActive(true);
            }
        }

        if (air == true && canSwitch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                api = false;
                air = false;
                tanah = true;
                canSwitch = false;
                txtAir.SetActive(false);
                txtTanah.SetActive(true);
            }
        }

        if (tanah == true && canSwitch)
        {
            if (Input.GetMouseButtonDown(0))
            {
                api = true;
                air = false;
                tanah = false;
                canSwitch = false;
                txtTanah.SetActive(false);
                txtApi.SetActive(true);
            }
        }
    }
}
