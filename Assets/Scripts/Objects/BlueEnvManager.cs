using System;
using System.Collections.Generic;
using Objects;
using UnityEngine;
using UnityEngine.Tilemaps;
// [ExecuteAlways]
public class BlueEnvManager : MonoBehaviour
{
    [SerializeField] private GameObject puddlePrefabs;
    private Dictionary<Tuple<int, int>, BlueEnvPuddleManager> puddleDict;

    public TileBase tileA;
    // Start is called before the first frame update 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
