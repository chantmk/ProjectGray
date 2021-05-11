using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBarManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = FindObjectOfType<Camera>();
    }
}
