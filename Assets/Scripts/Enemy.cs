using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    [SerializeField] float destroyDistance = 20f;

    Vector3 direction;

    public static event Action OnEnemyHitPlayer;
    
    void SetDirection(Vector2 direction)
    {
        direction.Normalize();
        transform.up = direction;
    }

    void Start()
    {
        direction = UnityEngine.Random.insideUnitCircle;
        SetDirection(direction);
    }

    void Update()
    {
        transform.position += (Vector3)direction * speed * Time.deltaTime;
        
        if (Vector2.Distance(transform.position, Vector2.zero) > destroyDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out Player player))
        {
            Destroy(player.gameObject);
            OnEnemyHitPlayer?.Invoke();
        }
    }
}
