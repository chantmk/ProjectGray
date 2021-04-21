using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TempSpriteUpdateOrder : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform bottom;
    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bottom = transform.parent.Find("Bottom");
    }

    private void Update()
    {
        spriteRenderer.sortingOrder = -Mathf.RoundToInt(bottom.position.y * 100);
    }

}