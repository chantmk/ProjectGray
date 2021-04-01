using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteUpdateOrder: MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
 
    private void Start () {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
 
    private void Update () { 
        spriteRenderer.sortingOrder = -Mathf.RoundToInt(transform.position.y * 100);
    }
    
}