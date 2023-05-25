using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    private Transform target;
    private int wavepointIndex = 0;

    private Enemy enemy;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.points[0];
    }
    
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * (enemy.speed * Time.deltaTime), Space.World);
        
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Euler(lookRotation.eulerAngles);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }

        enemy.speed = enemy.startSpeed;
    }

    void GetNextWaypoint()
    {
        if (wavepointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }

        wavepointIndex++;
        target = Waypoints.points[wavepointIndex];
    }

    void EndPath()
    {
        Destroy(gameObject);
        PlayerStats.Lives--;
        if (PlayerStats.Lives <= 0)
            return;
        WaveSpawner.EnemiesAlive--;
    }
}
