using UnityEngine;
using UnityEngine.InputSystem;

public class NumpadHand : MonoBehaviour
{
    public Vector3 offset;
    
    // Update is called once per frame
    void Update()
    {
        // 1. Convert Screen Space (Mouse) to World Space
        var mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
        
        // 2. Apply position to the hand
        transform.position = worldPos + offset;
    }
    private void OnDrawGizmosSelected()
    {
        // Draw at the object's current world position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position - offset, 1);
    }
}
