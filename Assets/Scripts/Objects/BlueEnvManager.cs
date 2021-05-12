﻿using System;
using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEngine;
using UnityEngine.Tilemaps;
// [ExecuteAlways]
public class BlueEnvManager : MonoBehaviour
{
    [SerializeField] private Sprite[] puddleSprites = new Sprite[6];
    [SerializeField] private GameObject puddlePrefabs;
    private Dictionary<Vector2Int, BlueEnvPuddleManager> puddleDict;
    private Transform playerTransform;
    private PlayerMovementManager playerMovementManager;
    
    private float footOffset;
    private readonly float scale = 0.5f;
    private readonly int invScale = 2;
    
    public Vector2Int playerPuddleCoordCache;
    private bool isCreateOnPlayerFire;


    public void RemovePuddle(Vector2Int key)
    {
        puddleDict.Remove(key);
    }
    void Start()
    {
        puddleDict = new Dictionary<Vector2Int, BlueEnvPuddleManager>();

        var playerGameObject = GameObject.FindGameObjectsWithTag("Player")[0];
        playerTransform = playerGameObject.transform;
        playerMovementManager = playerGameObject.GetComponent<PlayerMovementManager>();
        footOffset = playerGameObject.GetComponent<SpriteUpdateOrder>().footOffset;
        
        
        EventPublisher.PlayerFire += OnPlayerFire;
        EventPublisher.BlueBubbleDestroy += OnBlueBubbleDestroy;
    }

    private void OnDestroy()
    {
        EventPublisher.PlayerFire -= OnPlayerFire;
        EventPublisher.BlueBubbleDestroy -= OnBlueBubbleDestroy;
    }

    private void OnBlueBubbleDestroy(Vector3 position)
    {
        CreateSmallPuddle(Coord2PuddleCoord(QuantizePosition(position)));
    }

    private void CreateSmallPuddle(Vector2Int centerCoord)
    {
        createPuddle(centerCoord + new Vector2Int(0,0));
        createPuddle(centerCoord + new Vector2Int(-1,0));
        createPuddle(centerCoord + new Vector2Int(1,0));
        createPuddle(centerCoord + new Vector2Int(0,1));
        createPuddle(centerCoord + new Vector2Int(0,-1));
    }

    private void OnPlayerFire(WeaponIDEnum weaponID)
    {
        if (weaponID == WeaponIDEnum.Blue)
        {
            isCreateOnPlayerFire = true;
        }
    }

    public void CreateBigPuddle(Vector2Int centerCoord)
    {
        
        createPuddleStrong(centerCoord);
        createPuddleStrong(centerCoord + new Vector2Int(-1,0));
        createPuddleStrong(centerCoord + new Vector2Int(1,0));
        createPuddleStrong(centerCoord + new Vector2Int(0,-1));
        createPuddleStrong(centerCoord + new Vector2Int(0,1));
        
        createPuddle(centerCoord + new Vector2Int(-1,-1));
        createPuddle(centerCoord + new Vector2Int(1,-1));
        createPuddle(centerCoord + new Vector2Int(-1,1));
        createPuddle(centerCoord + new Vector2Int(1,1));
        
        
    }
    private void createPuddle(Vector2Int puddleCoord)
    {
        if (puddleDict.ContainsKey(puddleCoord))
        {
            puddleDict[puddleCoord].AddChargeOneStep();
        }
        else
        {
            var puddle = Instantiate(puddlePrefabs,
                PuddleCoord2Coord(puddleCoord),
                Quaternion.Euler(Vector3.zero));
            var puddleManager = puddle.GetComponent<BlueEnvPuddleManager>();
            
            puddleDict[puddleCoord] = puddleManager;
        }
        
    }
    
    private void createPuddleStrong(Vector2Int puddleCoord)
    {
        if (puddleDict.ContainsKey(puddleCoord))
        {
            puddleDict[puddleCoord].AddChargeOneStep();
        }
        else
        {
            var puddle = Instantiate(puddlePrefabs,
                PuddleCoord2Coord(puddleCoord),
                Quaternion.Euler(Vector3.zero));
            var puddleManager = puddle.GetComponent<BlueEnvPuddleManager>();
            puddleManager.AddChargeOneStep();
            puddleDict[puddleCoord] = puddleManager;
        }
        
    }

    private Vector3 QuantizePosition(Vector3 position)
    {
        return new Vector3(
            Mathf.Round((position.x - 0.25f) * invScale) * scale,
            Mathf.Round((position.y - 0.25f) * invScale) * scale,
            0f
        );
    }

    private Vector2Int Coord2PuddleCoord(Vector3 coord)
    {
        return new Vector2Int(Mathf.RoundToInt(coord.x*invScale), Mathf.RoundToInt(coord.y*invScale));
    }
    
    private Vector3 PuddleCoord2Coord(Vector2Int coord)
    {
        return new Vector3(coord.x * scale + 0.25f, coord.y * scale + 0.25f, 0f);
    }

    void FixedUpdate()
    {   
        playerPuddleCoordCache = Coord2PuddleCoord(QuantizePosition(playerTransform.position + Vector3.up*footOffset));

        // if (puddleDict.ContainsKey(playerPuddleCoordCache))
        // {
        //     playerMovementManager.MoveSpeedFactor = 0.2f;
        // }
        // else
        // {
        //     playerMovementManager.MoveSpeedFactor = 1f;
        // }
        
        foreach(var item in puddleDict.Where(kvp => kvp.Value.shouldRemove).ToList())
        {
            Destroy(item.Value.gameObject);
            puddleDict.Remove(item.Key);
        }
        
        if (isCreateOnPlayerFire)
        {
            CreateBigPuddle(playerPuddleCoordCache);
        }

        isCreateOnPlayerFire = false;
    }
}
