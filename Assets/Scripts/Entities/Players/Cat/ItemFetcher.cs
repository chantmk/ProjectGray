using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemFetcher : MonoBehaviour
{
    public Action<Collider2D> OnFetcherTriggerEnter;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnFetcherTriggerEnter?.Invoke(collision);
    }
}
