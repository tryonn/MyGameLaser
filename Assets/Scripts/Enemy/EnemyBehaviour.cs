using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float health = 150;
    [SerializeField] private float speedProjetctile = 10f;
    private float shootPerSeconds = 0.5f;

    private void Update()
    {
        float probability = Time.deltaTime * shootPerSeconds;
        if (Random.value < probability)
            Fire();
    }

    private void Fire()
    {
        Vector3 startPosition = transform.position + new Vector3(0, -1, 0);
        GameObject laser = Instantiate(projectile, startPosition, Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speedProjetctile);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Projectile laser = collider.gameObject.GetComponent<Projectile>();
        if (laser)
        {
            health -= laser.GetDamage();
            laser.Hit();

            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
