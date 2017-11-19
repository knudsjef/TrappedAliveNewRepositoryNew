using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Window : Tile
{

#if UNITY_EDITOR

    [MenuItem("Assets/Create/Tiles/Window")]
    public static void CreateWindowTile()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save Window Tile", "New Window Tile", "asset", "Save Window Tile", "Assets");

        if(path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Window>(), path);
    }

#endif

    [SerializeField]
    private Sprite[] windowSprites;

    [SerializeField]
    private Sprite preview;

    private bool IsWindow(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for(int y = -1; y <= 1; y++)
        {
            for(int x = -1; x <= 1; x++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if(IsWindow(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        } 
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        string composition = string.Empty;

        for(int y = -1; y <= 1; y++)
        {
            for(int x = -1; x <= 1; x++)
            {
                if(x != 0 || y != 0)
                {
                    if(IsWindow(tilemap, new Vector3Int(position.x + x, position.y + y, position.z)))
                    {
                        composition += 'W';
                    }
                    else
                    {
                        composition += "N";
                    }
                }
            }
        }

        tileData.sprite = windowSprites[0];

        if(composition == "WWWWWWWW")
        {
            tileData.sprite = windowSprites[0];
        }
        else if (composition[3] == 'W' && composition[4] == 'W' && composition[5] == 'W' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = windowSprites[4];
        }
        else if (composition[0] == 'W' && composition[1] == 'W' && composition[2] == 'W' && composition[3] == 'W' && composition[4] == 'W')
        {
            tileData.sprite = windowSprites[3];
        }
        else if (composition[0] == 'W' && composition[1] == 'W' && composition[3] == 'W' && composition[5] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = windowSprites[2];
        }
        else if (composition[1] == 'W' && composition[2] == 'W' && composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = windowSprites[1];
        }
        else if(composition[4] == 'W' && composition[6] == 'W' && composition[7] == 'W')
        {
            tileData.sprite = windowSprites[5];
        }
        else if (composition[1] == 'W' && composition[2] == 'W' && composition[4] == 'W')
        {
            tileData.sprite = windowSprites[7];
        }
        else if(composition[3] == 'W' && composition[5] == 'W' && composition[6] == 'W')
        {
            tileData.sprite = windowSprites[6];
        }
        else if(composition[0] == 'W' && composition[1] == 'W' && composition[3] == 'W')
        {
            tileData.sprite = windowSprites[8];
        }
    }

}
