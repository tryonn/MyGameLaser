using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float projetilSpeedRate = 5f;
    [SerializeField] private float maxPositionX = -10f;
    [SerializeField] private float minPositionX = 10f;
    [SerializeField] private float paddind = 1f;
    [SerializeField] private GameObject laserPrefabs;
    [SerializeField] private float projetilSpeed = 10f;
    [SerializeField] private float health = 150f;
    [SerializeField] private AudioClip audioFire;

    private void Start()
    {
        Camera camera = Camera.main;
        float distance = transform.position.z - camera.transform.position.z;
        minPositionX = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + paddind;
        maxPositionX = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - paddind;
    }

	void Update () {
        CheckShoot();
        CheckMovement();

	}

    private void Fire()
    {
        Vector3 offset = new Vector3(0,1,0);
        GameObject myLaser = Instantiate(laserPrefabs, transform.position + offset, Quaternion.identity) as GameObject;
        myLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projetilSpeed);
        AudioSource.PlayClipAtPoint(audioFire, transform.position);
    }

    private void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            //InvokeRepeating("Fire", 0.0001f, projetilSpeedRate);
        }
    }

    private void CheckMovement()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x-speed * Time.deltaTime, minPositionX, maxPositionX), transform.position.y, transform.position.z);
        } else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x + speed * Time.deltaTime, minPositionX, maxPositionX), transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile laser = collider.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            Debug.Log("player colidiu com laser");
            health -= laser.GetDamage();
            laser.Hit();

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
