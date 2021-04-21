using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    private bool interactable = false;
    private Color color;

    private void Start()
    {
        //color = transform.GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interactable)
        {
            OnInteract();
        }
    }

    public abstract void OnInteract();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactable = true;
            //transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            interactable = true;
            //transform.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
