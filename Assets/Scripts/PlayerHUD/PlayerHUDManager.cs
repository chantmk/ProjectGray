using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("BossHealthContainer").gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
