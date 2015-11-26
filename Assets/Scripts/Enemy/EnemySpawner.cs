using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float width = 10f;
    [SerializeField] private float heigth = 5f;
    [SerializeField] private float paddind = 1f;
    [SerializeField] private float speed = 5f;

    [SerializeField] private int direction = 1;
    [SerializeField] private float boundaryRightEdge;
    [SerializeField] private float boundaryLeftEdge;

    private void Start()
    {
        Camera camera = Camera.main;
        float distance = transform.position.z - camera.transform.position.z;
        boundaryLeftEdge = camera.ViewportToWorldPoint(new Vector3(0, 0, distance)).x + paddind;
        boundaryRightEdge = camera.ViewportToWorldPoint(new Vector3(1, 1, distance)).x - paddind;

        foreach (Transform child in transform)
        {
            GameObject enemy = Instantiate(enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
            enemy.transform.parent = child;
        }
    }

    void OnDrawGizmos()
    {
        float xmin, xmax, ymin, ymax;
        xmin = transform.position.x - 0.5f * width;
        xmax = transform.position.x + 0.5f * width;

        ymin = transform.position.y - 0.5f * heigth;
        ymax = transform.position.y + 0.5f * heigth;

        Gizmos.DrawLine(new Vector3(xmin, ymin, 0), new Vector3(xmin, ymax, 0));
        Gizmos.DrawLine(new Vector3(xmin, ymax, 0), new Vector3(xmax, ymax, 0));
        Gizmos.DrawLine(new Vector3(xmax, ymax, 0), new Vector3(xmax, ymin, 0));
        Gizmos.DrawLine(new Vector3(xmax, ymin, 0), new Vector3(xmin, ymin, 0));
    }

    private void Update()
    {
        float formationRightEdge = transform.position.x + 0.5f * width;
        float formationLeftEdge = transform.position.x - 0.5f * width;

        if (formationRightEdge > boundaryRightEdge)
        {
            direction = -1;
        } 
        if (formationLeftEdge < boundaryLeftEdge)
        {
            direction = 1;
        }

        transform.position += new Vector3(direction * speed * Time.deltaTime, 0,0);
    }
}
