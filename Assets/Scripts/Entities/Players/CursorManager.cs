using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;
    private SpriteRenderer spriteRenderer;

    public float offset;
    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.Find("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Cursor.visible = false;
        transform.localScale *= 1/(1+offset);
        spriteRenderer.color = new Color(1f, 1f, 1f, 1 / Mathf.Pow(1.1f + offset,2f));

        
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 playerPos = playerTransform.position;
        Vector2 cursorLine = Camera.main.ScreenToWorldPoint(Input.mousePosition) - playerTransform.position;
        transform.position = playerPos + (cursorLine / (1+offset));

    }
}
