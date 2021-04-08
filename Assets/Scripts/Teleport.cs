using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // public override void OnInteract()
    // {
    //     var g = GameObject.FindGameObjectsWithTag("Player")[0];
    //     g.GetComponent<PlayerInventory>().AddHealthPack();
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(1);
    }
}
