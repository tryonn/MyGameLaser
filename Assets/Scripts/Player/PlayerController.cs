using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    [SerializeField] private float speed;
    [SerializeField] private float maxPositionX = -10f;
    [SerializeField] private float minPositionX = 10f;
    [SerializeField] private float paddind = 1f;
    [SerializeField] private GameObject laserPrefabs;
    [SerializeField] private float projetilSpeed = 10f;

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

    private void CheckShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject myLaser = Instantiate(laserPrefabs, transform.position, Quaternion.identity) as GameObject;
            myLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projetilSpeed);
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
}
