using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RandomTile : MonoBehaviour
{
    public Tilemap tilemap;
    private List<Vector3> tileWorldLocations;
    private float[,,] dangerTile = new float[20, 20, 3];
    BoundsInt bounds;
    private GameObject playerObj = null;
    private GameObject weaponObj = null;
    private PlayerWeaponManager weaponManager = null;
    int objX;
    int objY;
    //private int currentTileX = 0;
    //private int currentTileY = 0;
    //private float outSpeed;
    private int updateIdx;
    private Color updateColor;

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        if (!playerObj) playerObj = GameObject.FindGameObjectWithTag("Player");
        if (!weaponManager) weaponManager = GameObject.FindGameObjectWithTag("Weapon").GetComponent< PlayerWeaponManager>();
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
        switch (weaponManager.CurrentWeaponID)
        {
            case 0:
                updateColor = Color.green;
                break;
            case 1:
                updateColor = Color.black;
                break;
            case 2:
                updateColor = Color.blue;
                break;
            default:
                break;
        }
        Debug.Log(message: "Weapon ID:" + weaponManager.CurrentWeaponID);
        if (dangerTile[objX + 5, objY + 7, weaponManager.CurrentWeaponID] != 5)
        {
            dangerTile[objX + 5, objY + 7, weaponManager.CurrentWeaponID] += 1;
        }
        //Color color = Color.green;
        updateColor.a = dangerTile[objX + 5, objY + 7, weaponManager.CurrentWeaponID] * 0.2f;
        tilemap.SetColor(new Vector3Int(objX, objY, 0), updateColor);
        /*playerObj.GetComponent<PlayerMovementManager>().setSpeed(dangerTile[objX + 5, objY + 7]);*/
        //Debug.Log(dangerTile[objX + 5, objY + 7]);



    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Player Position: X = " + Math.Floor(playerObj.transform.position.x) + " --- Y = " + Math.Floor(playerObj.transform.position.y));\

        // Update player position every frame
        objX = (int)Math.Floor(playerObj.transform.position.x);
        objY = (int)Math.Floor(playerObj.transform.position.y);
        ////////////////////////////////////////////////////////////////

        /*if (objX != currentTileX || objY != currentTileY)
        {
            playerObj.GetComponent<PlayerMovementManager>().setSpeed(dangerTile[objX + 5, objY + 7]);
            currentTileX = objX;
            currentTileY = objY;
        }*/

    }

    public float getCurrentTileSpeed()
    {
        try
        {
            /* return  outSpeed = dangerTile[objX + 5, objY + 7, 0];*/
            return dangerTile[objX + 5, objY + 7, 0];
        }
        catch (IndexOutOfRangeException e)  // CS0168
        {
            Console.WriteLine(e.Message);
            throw new ArgumentOutOfRangeException("index parameter is out of range.", e);
        }
        //return outSpeed;
    }
}
