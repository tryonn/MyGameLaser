using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private float health = 150;

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
