using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottomPositioner : MonoBehaviour
{
    [SerializeField]
    private GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        body = transform.parent.Find("Body").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateBottom();
    }

    private void CalculateBottom()
    {
        //transform.position = [body get bound sprite and find bottom]
    }
}
