using System;
using System.Collections.Generic;
using System.Linq;
using Objects;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.SocialPlatforms;
using UnityEngine.Tilemaps;
using Utils;
using Random = UnityEngine.Random;

// [ExecuteAlways]
public class EnvManager : MonoBehaviour
{
    public GameObject blackTilePrefabs;
    public GameObject blueTilePrefabs;
    public GameObject yellowTilePrefabs;

    private Dictionary<ColorEnum, GameObject> tilePrefabDict = new Dictionary<ColorEnum, GameObject>();

    private Dictionary<Vector2Int, TileManager> tileDict;
    private Transform playerTransform;
    private PlayerMovementManager playerMovementManager;
    
    private float footOffset;
    private readonly float scale = 0.5f;
    private readonly int invScale = 2;
    
    [FormerlySerializedAs("playerTileCoordCache")] [HideInInspector] public Vector2Int playerTileXYCache;
    private bool isCreateOnPlayerFire;


    public void RemovePuddle(Vector2Int key)
    {
        tileDict.Remove(key);
    }
    void Start()
    {
        tileDict = new Dictionary<Vector2Int, TileManager>();
        tilePrefabDict[ColorEnum.Black] = blackTilePrefabs;
        tilePrefabDict[ColorEnum.Blue] = blueTilePrefabs;
        tilePrefabDict[ColorEnum.Yellow] = yellowTilePrefabs;
        
        
        var playerGameObject = GameObject.FindGameObjectsWithTag("Player")[0];
        playerTransform = playerGameObject.transform;
        playerMovementManager = playerGameObject.GetComponent<PlayerMovementManager>();
        footOffset = playerGameObject.GetComponent<SpriteUpdateOrder>().footOffset;
        
        EventPublisher.PlayerFire += OnPlayerFire;
        EventPublisher.MindBreak += OnMindBreak;
        // EventPublisher.BlueBubbleDestroy += OnBlueBubbleDestroy;
    }

    private void OnMindBreak(ColorEnum color
    )
    {
        CreateCircle(playerTileXYCache, color);
    }

    // private void OnBlueBubbleDestroy(Vector3 position)
    // {
    //     CreateSmallPuddle(Coord2PuddleCoord(QuantizePosition(position)));
    // }
    //
    // private void CreateSmallPuddle(Vector2Int centerCoord)
    // {
    //     CreateTile(centerCoord + new Vector2Int(0,0));
    //     CreateTile(centerCoord + new Vector2Int(-1,0));
    //     CreateTile(centerCoord + new Vector2Int(1,0));
    //     CreateTile(centerCoord + new Vector2Int(0,1));
    //     CreateTile(centerCoord + new Vector2Int(0,-1));
    // }

    private void OnPlayerFire(WeaponIDEnum weaponID)
    {
        if (weaponID == WeaponIDEnum.Blue)
        {
            isCreateOnPlayerFire = true;
        }
    }

    // public void CreateBigPuddle(Vector2Int centerCoord)
    // {
    //     
    //     createPuddleStrong(centerCoord);
    //     createPuddleStrong(centerCoord + new Vector2Int(-1,0));
    //     createPuddleStrong(centerCoord + new Vector2Int(1,0));
    //     createPuddleStrong(centerCoord + new Vector2Int(0,-1));
    //     createPuddleStrong(centerCoord + new Vector2Int(0,1));
    //     
    //     CreateTile(centerCoord + new Vector2Int(-1,-1));
    //     CreateTile(centerCoord + new Vector2Int(1,-1));
    //     CreateTile(centerCoord + new Vector2Int(-1,1));
    //     CreateTile(centerCoord + new Vector2Int(1,1));
    //     
    //     
    // }

    public void CreateRandomTile(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            
        }
    }

    private void CreateCircle(Vector2Int center, ColorEnum color)
    {
        for (int x = -12; x <= 11; x++)
        {
            for (int y = -8; y <= 1; y++)
            {
                var sqrmag = (new Vector2Int(x, y) - center).sqrMagnitude;
                float prob = Mathf.Exp(-sqrmag/7.2f);
                float randomnum = Random.value;
                if (prob > randomnum && sqrmag >= 2)
                    CreateTile(new Vector2Int(x,y), color);
            }
        }
    }
    
    private void CreateTile(Vector2Int xy, ColorEnum color)
    {
        if (tileDict.ContainsKey(xy))
        {
            Destroy(tileDict[xy].gameObject);
            tileDict.Remove(xy);
        }
        
        if (xy.x >= -12 && xy.x <= 11 && xy.y >= -8 && xy.y <= 1)
        {
            var tile = Instantiate(tilePrefabDict[color],
                (Vector3Int)xy + new Vector3(0.5f,0.5f,0),
                Quaternion.Euler(Vector3.zero));
            var tileManager = tile.GetComponent<TileManager>();
            tileDict[xy] = tileManager;
        }
        
    }
    private Vector2Int Quantize1Unit(Vector3 position)
    {
        return new Vector2Int(
            (int) (Mathf.Floor(position.x)),
            (int) (Mathf.Floor(position.y))
        );
    }

    void FixedUpdate()
    {   
        // playerPuddleCoordCache = Coord2PuddleCoord(QuantizePosition(playerTransform.position + Vector3.up*footOffset));
        playerTileXYCache = Quantize1Unit(playerTransform.position + Vector3.up * footOffset);

        if (Input.GetKeyDown(KeyCode.T))
        {
            // CreateTile(playerTileXYCache, ColorEnum.Black);
            CreateCircle(playerTileXYCache, ColorEnum.Blue);
        }
        // CreateTile(playerTileXYCache, ColorEnum.Black);
        // if (puddleDict.ContainsKey(playerPuddleCoordCache))
        // {
        //     playerMovementManager.MoveSpeedFactor = 0.2f;
        // }
        // else
        // {
        //     playerMovementManager.MoveSpeedFactor = 1f;
        // }
        
        foreach(var item in tileDict.Where(kvp => kvp.Value.shouldRemove).ToList())
        {
            Destroy(item.Value.gameObject);
            tileDict.Remove(item.Key);
        }
        
        // if (isCreateOnPlayerFire)
        // {
        //     CreateBigPuddle(playerPuddleCoordCache);
        // }

        isCreateOnPlayerFire = false;
    }
}
