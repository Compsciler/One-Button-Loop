using UnityEngine;

public class OrbitCenter : MonoBehaviour
{
    [SerializeField] Color activeColor = Color.white;
    [SerializeField] Color inactiveColor = Color.gray;

    SpriteRenderer spriteRenderer;
    
    bool isActive = true;
    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        SetIsActive(isActive);
    }
    
    public void SetIsActive(bool active)
    {
        isActive = active;
        spriteRenderer.color = isActive ? activeColor : inactiveColor;
    }
}
