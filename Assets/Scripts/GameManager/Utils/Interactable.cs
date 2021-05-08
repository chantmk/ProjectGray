using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class Interactable : MonoBehaviour
{
    private bool interactable = false;
    private Color color;

    protected virtual void Start()
    {
        //color = transform.GetComponent<SpriteRenderer>().color;
    }

    protected virtual void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && interactable)
        {
            //Debug.Log("key F pressed");
            OnInteract();
        }
    }

    public abstract void OnInteract();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(other + " enter");
            interactable = true;
            //transform.GetComponent<SpriteRenderer>().color = Color.red;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log(other + " exit");
            interactable = false;
            //transform.GetComponent<SpriteRenderer>().color = color;
        }
    }
}
