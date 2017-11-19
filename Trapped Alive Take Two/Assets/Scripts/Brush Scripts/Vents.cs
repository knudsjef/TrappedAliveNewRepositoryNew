using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Vents : Tile
{

    [SerializeField]
    private Sprite[] ventSprites;

    [SerializeField]
    private Sprite preview;

    public override void RefreshTile(Vector3Int position, ITilemap tilemap)
    {
        for (int y = -1; y <= 1; y++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3Int nPos = new Vector3Int(position.x + x, position.y + y, position.z);

                if(IsVent(tilemap, nPos))
                {
                    tilemap.RefreshTile(nPos);
                }
            }
        } 
    }

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {

        string composition = string.Empty;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {

                if (x != 0 || y != 0)
                {

                    if (IsVent(tilemap, new Vector3Int(position.x + x, position.y + y, position.z)))
                    {
                        composition += 'V';
                    }
                    else
                    {
                        composition += 'W';
                    }
                }
            }
        }

        tileData.sprite = ventSprites[3];
        
        if(composition[1] == 'V' && composition[3] == 'V' && composition[6] == 'V')
        {
            tileData.sprite = IntersectionBottomRandom();
        }
        else if(composition[1] == 'V' && composition[4] == 'V' && composition[6] == 'V')
        {
            tileData.sprite = IntersectionTopRandom();
        }
        else if(composition[1] == 'V' && composition[3] == 'V' && composition[4] == 'V')
        {
            tileData.sprite = IntersectionLeftRandom();
        }
        else if(composition[3] == 'V' && composition[4] == 'V' && composition[6] == 'V')
        {
            tileData.sprite = IntersectionRightRandom();
        }
        else if(composition[1] == 'V' && composition[3] == 'V')
        {
            tileData.sprite = CornerURRandom();
        }
        else if(composition[1] == 'V' && composition[4] == 'V')
        {
            tileData.sprite = CornerBRRandom();
        }
        else if(composition[3] == 'V' && composition[6] == 'V')
        {
            tileData.sprite = CornerULRandom();
        }
        else if(composition[4] == 'V' && composition[6] == 'V')
        {
            tileData.sprite = CornerBLRandom();
        }
        else if(composition[1] == 'V')
        {
            tileData.sprite = HorizontalRandom();
        }
        else if(composition[3] == 'V')
        {
            tileData.sprite = VerticalRandom();
        }
        else if(composition[4] == 'V')
        {
            tileData.sprite = VerticalRandom();
        }
        else if(composition[6] == 'V')
        {
            tileData.sprite = HorizontalRandom();
        }
    }

    private bool IsVent(ITilemap tilemap, Vector3Int position)
    {
        return tilemap.GetTile(position) == this;
    }

    private Sprite IntersectionBottomRandom()
    {

        int RaNum = Random.Range(3, 1000);

        if (RaNum % 3 == 0)
        {
            return ventSprites[18];
        }
        else if(RaNum % 2 == 0)
        {
            return ventSprites[11];
        }
        else
        {
            return ventSprites[13];
        }

    }

    private Sprite IntersectionTopRandom()
    {

        return ventSprites[15];

    }

    private Sprite IntersectionRightRandom()
    {

        int RaNum = Random.Range(3, 1000);

        if (RaNum % 3 == 0)
        {
            return ventSprites[10];
        }
        else
        {
            return ventSprites[14];
        }
    }

    private Sprite IntersectionLeftRandom()
    {

        int RaNum = Random.Range(3, 1000);

        if (RaNum % 3 == 0)
        {
            return ventSprites[20];
        }
        else
        {
            return ventSprites[12];
        }
    }

    private Sprite CornerURRandom()
    {

        int RaNum = Random.Range(3, 1000);

        if (RaNum % 3 == 0)
        {
            return ventSprites[17];
        }
        else
        {
            return ventSprites[6];
        }
    }

    private Sprite CornerBRRandom()
    {

        return ventSprites[7];

    }

    private Sprite CornerBLRandom()
    {

        return ventSprites[8];

    }

    private Sprite CornerULRandom()
    {

        int RaNum = Random.Range(3, 1000);

        if (RaNum % 3 == 0)
        {
            return ventSprites[16];
        }
        else
        {
            return ventSprites[9];
        }
    }

    private Sprite HorizontalRandom()
    {

        int RaNum = Random.Range(3, 1000);

        if (RaNum % 3 == 0)
        {
            return ventSprites[0];
        }
        else if(RaNum % 2 == 0)
        {
            return ventSprites[2];
        }
        else
        {
            return ventSprites[1];
        }
    }

    private Sprite VerticalRandom()
    {

        int RaNum = Random.Range(3, 1000);

        if (RaNum % 3 == 0)
        {
            return ventSprites[4];
        }
        else if (RaNum % 2 == 0)
        {
            return ventSprites[5];
        }
        else
        {
            return ventSprites[3];
        }
    }

#if UNITY_EDITOR

    [MenuItem("Assets/Create/Tiles/VentTile")]
    public static void CreateVentTile()
    {

        string path = EditorUtility.SaveFilePanelInProject("Save Vent Tile", "New Vent Tile", "asset", "Save Vent Tile", "Assets");
        if(path == "")
        {
            return;
        }
        AssetDatabase.CreateAsset(ScriptableObject.CreateInstance<Vents>(), path);
    }
#endif

}
