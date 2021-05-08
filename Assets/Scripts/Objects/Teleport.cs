using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Teleport : MonoBehaviour
{

    private bool isEnable = false;
    // Start is called before the first frame update
    void Start()
    {
        EventPublisher.DecisionMake += EnablePortal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        EventPublisher.DecisionMake -= EnablePortal;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isEnable && other.gameObject.name == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    public void EnablePortal(DecisionEnum decision)
    {
        isEnable = true;
    }
}
