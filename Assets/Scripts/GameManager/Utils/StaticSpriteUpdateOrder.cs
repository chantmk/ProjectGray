using UnityEngine;

public class SpriteUpdateOrderStatic : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public float footOffset;
 
    private void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingLayerName = "Entity";
        spriteRenderer.sortingOrder = -Mathf.RoundToInt((transform.position.y + footOffset )* 100);
    }
    
    void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * footOffset);
        Gizmos.DrawWireCube(transform.position + Vector3.up * footOffset, new Vector3(0.5f,0.2f,0.2f));
    }
}