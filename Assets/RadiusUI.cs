using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[ExecuteAlways]
public class RadiusUI : MonoBehaviour
{
    [Range(0,50)]
    public int segments = 50;
    [Range(0,5)]
    public float xradius = 1.5f;
    LineRenderer line;
    private float time;
    private float maxTime;

    void Start ()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.SetVertexCount (segments + 1);
        line.useWorldSpace = false;
        CreatePoints ();
        Hide();
    }

    public void Hide()
    {
        line.enabled = false;
    }

    public void Show(float maxtime)
    {
        this.time = maxtime;
        this.maxTime = maxtime;
        line.enabled = true;
        SetColorWithAlpha(0f);
    }

    private void SetColorWithAlpha(float alpha)
    {
        var color = Color.red;
        color.a = alpha;
        line.endColor = color;
        line.startColor = color;
        
    }

    void Update()
    {
        time -= Time.deltaTime;
        SetColorWithAlpha((maxTime-Mathf.Max(0f, time) )/ maxTime);
    }
    
    void CreatePoints ()
    {
        float x;
        float y;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin (Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos (Mathf.Deg2Rad * angle) * xradius;

            line.SetPosition (i,new Vector3(x,y,0) );

            angle += (360f / segments);
        }
    }
}
