using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    // TODO: try constant angular speed
    [SerializeField] float speed = 5f;

    [SerializeField] float orbitRadiusStart = 1.5f;
    [SerializeField] float orbitRadiusIncreaseSpeed = 1.0f;
    
    [SerializeField] GameObject orbitCenter;
    [SerializeField] GameObject orbitCenterAlternate;

    float radius;
    bool isCounterClockwise = false;

    void Start()
    {
        radius = Vector2.Distance(transform.position, orbitCenter.transform.position);
    }

    void Update()
    {
        Move();
    }
    
    void Move()
    {
        float distance = speed * Time.deltaTime;
        float angularDistance = distance / (radius * 2 * Mathf.PI);

        Vector2 orbitCenterOffset = transform.position - orbitCenter.transform.position;
        float angle = Mathf.Atan2(orbitCenterOffset.y, orbitCenterOffset.x);
        float newAngle = angle + angularDistance * (isCounterClockwise ? 1 : -1);

        Vector2 newOrbitCenterOffset = new Vector2(Mathf.Cos(newAngle), Mathf.Sin(newAngle)) * radius;
        transform.position = orbitCenter.transform.position + new Vector3(newOrbitCenterOffset.x, newOrbitCenterOffset.y, 0f);
    }
}
