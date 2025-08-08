using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] bool usingConstantAngularSpeed = false;

    [SerializeField] float orbitRadiusStart = 1.5f;
    [SerializeField] float orbitRadiusIncreaseSpeed = 1.5f;

    [SerializeField] GameObject orbitCenter1;
    [SerializeField] GameObject orbitCenter2;

    [SerializeField] Color notUsingConstantAngularSpeedColor = Color.cyan;
    [SerializeField] Color usingConstantAngularSpeedColor = Color.red;
    
    float radius;
    bool isCounterClockwise = false;
    OrbitCenter orbitCenter;
    OrbitCenter inactiveOrbitCenter;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        orbitCenter = orbitCenter1.GetComponent<OrbitCenter>();
        inactiveOrbitCenter = orbitCenter2.GetComponent<OrbitCenter>();
        orbitCenter.SetIsActive(true);
        inactiveOrbitCenter.SetIsActive(false);
    }

    void Start()
    {
        radius = Vector2.Distance(transform.position, orbitCenter.transform.position);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            usingConstantAngularSpeed = !usingConstantAngularSpeed;
            spriteRenderer.color = usingConstantAngularSpeed ? usingConstantAngularSpeedColor : notUsingConstantAngularSpeedColor;
        }
        
        if (Input.GetMouseButtonDown((int)MouseButton.Left))
        {
            isCounterClockwise = !isCounterClockwise;
            radius = orbitRadiusStart;
            SwapOrbitCenters();
        }
        if (Input.GetMouseButton((int)MouseButton.Left))
        {
            radius += orbitRadiusIncreaseSpeed * Time.deltaTime;
            // RepositionActiveOrbitCenter();
        }
        
        Move();
        PositionInactiveOrbitCenter();
    }
    
    void SwapOrbitCenters()
    {
        orbitCenter.SetIsActive(false);
        inactiveOrbitCenter.SetIsActive(true);

        (orbitCenter, inactiveOrbitCenter) = (inactiveOrbitCenter, orbitCenter);
    }
    
    void Move()
    {
        float angularDistance;
        if (usingConstantAngularSpeed)
        {
            angularDistance = (speed * Time.deltaTime) / (orbitRadiusStart * 2 * Mathf.PI);
        }
        else
        {
            float distance = speed * Time.deltaTime;
            angularDistance = distance / (radius * 2 * Mathf.PI);
        }

        Vector3 orbitCenterOffset = transform.position - orbitCenter.transform.position;
        float angle = Mathf.Atan2(orbitCenterOffset.y, orbitCenterOffset.x);
        float newAngle = angle + angularDistance * (isCounterClockwise ? 1 : -1);

        Vector3 newOrbitCenterOffset = new Vector3(Mathf.Cos(newAngle), Mathf.Sin(newAngle), 0f) * radius;
        transform.position = orbitCenter.transform.position + newOrbitCenterOffset;
    }
    
    void RepositionActiveOrbitCenter()
    {
        Vector3 orbitCenterOffset = orbitCenter.transform.position - transform.position;
        orbitCenter.transform.position = transform.position + orbitCenterOffset.normalized * radius;
    }

    void PositionInactiveOrbitCenter()
    {
        Vector3 orbitCenterOffset = orbitCenter.transform.position - transform.position;
        inactiveOrbitCenter.transform.position = transform.position - orbitCenterOffset;
    }
}
