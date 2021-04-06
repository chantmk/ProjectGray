using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RandomTile : MonoBehaviour
{
    public Tilemap tilemap;
    private List<Vector3> tileWorldLocations;
    private float[,] dangerTile = new float[12, 6];
    BoundsInt bounds;
    private GameObject playerObj = null;
    int objX;
    int objY;
    private int currentTileX = 0;
    private int currentTileY = 0;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        if (playerObj == null)
            playerObj = GameObject.FindGameObjectWithTag("Player");
        EventPublisher.PlayerFire += processArrayColor;
        for (int x = bounds.xMin + 4; x < bounds.xMax - 1; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax - 4; y++)
            {
                //TileBase tile = allTiles[x + y * bounds.size.x];
                tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                /*tilemap.SetColor(new Vector3Int(x, y, 0), Color.green);*/

            }
        }

    }

    void processArrayColor()
    {
       /* objX = (int)Math.Floor(playerObj.transform.position.x);
        objY = (int)Math.Floor(playerObj.transform.position.y);*/
        if (dangerTile[objX + 5, objY + 7] != 5)
        {
            dangerTile[objX + 5, objY + 7] += 1;
        }
        
        Color color = Color.green;
        color.a = dangerTile[objX + 5, objY + 7] * 0.2f;
        tilemap.SetColor(new Vector3Int(objX, objY, 0), color);
        /*playerObj.GetComponent<PlayerMovementManager>().setSpeed(dangerTile[objX + 5, objY + 7]);*/
        //Debug.Log(dangerTile[objX + 5, objY + 7]);



    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player Position: X = " + Math.Floor(playerObj.transform.position.x) + " --- Y = " + Math.Floor(playerObj.transform.position.y));
        objX = (int)Math.Floor(playerObj.transform.position.x);
        objY = (int)Math.Floor(playerObj.transform.position.y);
        /*if (objX != currentTileX || objY != currentTileY)
        {
            playerObj.GetComponent<PlayerMovementManager>().setSpeed(dangerTile[objX + 5, objY + 7]);
            currentTileX = objX;
            currentTileY = objY;
        }*/

    }

    public float getCurrentTileData()
    {
        /*objX = (int)Math.Floor(playerObj.transform.position.x);
        objY = (int)Math.Floor(playerObj.transform.position.y);*/
        return dangerTile[objX + 5, objY + 7];
    }
}
