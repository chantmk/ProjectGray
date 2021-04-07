using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class HealthPackManager : InteractableBase
    {
        public override void OnInteract()
        {
            var g = GameObject.FindGameObjectsWithTag("Player")[0];
            g.GetComponent<PlayerInventory>().AddHealthPack();
            Destroy(gameObject);
        }
    }

    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractableBase: MonoBehaviour
    {
        private bool interactable = false;
        private Color color;

        private void Start()
        {
            color = transform.GetComponent<SpriteRenderer>().color;
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
                transform.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.tag == "Player")
            {
                interactable = true;
                transform.GetComponent<SpriteRenderer>().color = color;
            }
        }
    }
}