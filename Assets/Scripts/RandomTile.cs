using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class RandomTile : MonoBehaviour
{
    public Tilemap tilemap;
    private List<Vector3> tileWorldLocations;
    private int[,] dangerTile = new int[12, 6] { { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, { 1, 0, 1, 0, 0, 1 }, };
    // Start is called before the first frame update
    BoundsInt bounds;
    
    

    void Start()
    {
        tilemap = GetComponent<Tilemap>();
        bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);

        
        EventPublisher.PlayerFire += processArrayColor;
        
    }

    void processArrayColor()
    {
        for (int x = bounds.xMin + 4; x < bounds.xMax - 1; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax - 4; y++)
            {
                /*TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                }
                else
                {
                    Debug.Log("x:" + x + " y:" + y + " tile: (null)");
                }*/
                //Debug.Log("x:" + x + " y:" + y);
                if (dangerTile[x + 5, y + 7] == 1)
                {
                    tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                    tilemap.SetColor(new Vector3Int(x, y, 0), Color.green);
                }
                //tilemap.SetTileFlags(new Vector3Int(x, y, 0), TileFlags.None);
                //tilemap.SetColor(new Vector3Int(x, y, 0), Color.green);

            }
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
