using lLCroweTool;
using lLCroweTool.TileMap.HexTileMap;
using UnityEngine;
using static lLCroweTool.lLcroweUtil.HexTileMatrix;
using static lLCroweTool.TileMap.HexTileMap.Custom3DHexTileMap;

public class TestTileSpacing : MonoBehaviour
{
    public int widthAmount = 7;
    public int heightAmount = 5;

    public float widthSpacingValue = 1;
    public float heightSpacingValue = 1;

    public float tileSize = 1f;
    public float tileDistance = 2f;//Ÿ�ϳ����� ����

    public HexTileType hexTileType;
    public CreateTileAxisType createTileAxisType;

    public Material material;
    public HexTileObject hexTilePrefab;
    public HexTileObjectBible hexTileObjectBible = new HexTileObjectBible();

    public Transform target;

    [ButtonMethod]
    public void Init()
    {
        //�����
        foreach (var item in hexTileObjectBible)
        {
            var hexTile = item.Value;
            DestroyImmediate(hexTile.gameObject);
        }
        hexTileObjectBible.Clear();

        //���� ����
        switch (hexTileType)
        {
            case HexTileType.FlatTop:
                for (int x = 0; x < widthAmount; x++)
                {
                    int decreaseValue = x % 2 == 1 ? 1 : 0;
                    for (int y = 0; y < heightAmount - decreaseValue; y++)
                    {
                        var hexTile = Instantiate(hexTilePrefab);
                        Vector3Int tilePos = new Vector3Int(x, y);//����
                        hexTile.name = tilePos.ToString();
                        hexTile.InitTrObjPrefab(GetTileLocalPos(tilePos), Quaternion.identity, transform, false);
                        hexTile.InitHexTileRenderer(tileSize, hexTileType, createTileAxisType, material);
                        hexTile.hexTileData.InitHexTileData(tilePos, hexTile, null);
                        hexTile.CreateMesh();

                        hexTileObjectBible.Add(tilePos, hexTile);
                    }
                }
                break;
            case HexTileType.PointyTop:
                for (int y = 0; y < heightAmount; y++)
                {
                    int decreaseValue = y % 2 == 1 ? 1 : 0;
                    for (int x = 0; x < widthAmount - decreaseValue; x++)
                    {
                        var hexTile = Instantiate(hexTilePrefab);
                        Vector3Int tilePos = new Vector3Int(x, y);//����
                        hexTile.name = tilePos.ToString();
                        hexTile.InitTrObjPrefab(GetTileLocalPos(tilePos), Quaternion.identity, transform, false);
                        hexTile.InitHexTileRenderer(tileSize, hexTileType, createTileAxisType, material);
                        hexTile.hexTileData.InitHexTileData(tilePos,hexTile,null);
                        hexTile.CreateMesh();

                        hexTileObjectBible.Add(tilePos, hexTile);
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Ÿ����ġ�� ���� ������������ �������� �Լ�
    /// </summary>
    /// <param name="tilePos">Ÿ����ġ</param>
    /// <returns></returns>
    private Vector3 GetTileLocalPos(Vector3Int tilePos)
    {
        //�� Ÿ�Կ� ���� ��ġ ����//������ ����
        Vector2 addPos = Vector2.zero;
        switch (hexTileType)
        {
            case HexTileType.FlatTop:
                float addYPos = tilePos.x % 2 == 1 ? (tileDistance + heightSpacingValue) * 0.5f : 0;
                addPos = new Vector2(0, addYPos);
                break;
            case HexTileType.PointyTop:
                float addXPos = tilePos.y % 2 == 1 ? (tileDistance + widthSpacingValue) * 0.5f : 0;
                addPos = new Vector2(addXPos, 0);
                break;
        }

        //Ÿ�ϰ��� �Ÿ� == ���ϱ�
        //��ǥ�� ���� spacing == ���ϱ�
        //spacing == ���ϱ�
        //��ġ�߰� == ���ϱ�

        //�������
        //float xPos = (tilePos.x * tileDistance) + addPos.x + (widthSpacingValue * tilePos.x);
        //float yPos = (tilePos.y * tileDistance) + addPos.y + (heightSpacingValue * tilePos.y);
        //����ȭ
        float xPos = (tilePos.x * (tileDistance + widthSpacingValue)) + addPos.x;
        float yPos = (tilePos.y * (tileDistance + heightSpacingValue)) + addPos.y;

        //�ຯ��
        Vector3 newPos = Vector3.zero;
        switch (createTileAxisType)
        {
            case CreateTileAxisType.XY:
                newPos = new Vector3(xPos, yPos);
                break;
            case CreateTileAxisType.XZ:
                newPos = new Vector3(xPos, 0, yPos);
                break;
        }
        return newPos;
    }

    private void OnValidate()
    {
        foreach (var item in hexTileObjectBible)
        {
            var hexTileTr = item.Value.transform;
            var tilePos = item.Key;
            hexTileTr.localPosition = GetTileLocalPos(tilePos);
            item.Value.InitHexTileRenderer(tileSize, hexTileType, createTileAxisType, material);
        }
    }

    private void OnDrawGizmos()
    {
        if (target == null)
        {
            return;
        }

        //��������
        //������ġ��

        var pos = ConvertWorldPosForTilePos(target.position);
        Debug.Log(pos);
    }

    
    /// <summary>
    /// ������ġ�� Ÿ����ġ�� ��ȯ�ϴ� �Լ�
    /// </summary>
    /// <param name="worldPos">������ġ</param>
    /// <returns>Ÿ����ġ</returns>
    private Vector3Int ConvertWorldPosForTilePos(Vector3 worldPos)
    {
        Vector3 targetPos = transform.InverseTransformPoint(worldPos);
        targetPos = ConventAxis(targetPos);//�ຯ��

        ////������ ����
        float xSpacing = tileDistance + widthSpacingValue;
        float xHalfSpacing = xSpacing * 0.5f;
        float ySpacing = tileDistance + heightSpacingValue;
        float yHalfSpacing = ySpacing * 0.5f;

        ////������ ��ġ//������ ������ ������ġ�� ����
        //float xPos = targetPos.x;
        //float yPos = targetPos.y;

        //float xTileMousePos = targetPos.x / xSpacing;
        //float yTileMousePos = targetPos.y / ySpacing;

        ////Ÿ����ġ//����ġ�� ã�ư��ߵ�
        //int xTilePos = Mathf.RoundToInt(xTileMousePos);
        //int yTilePos = Mathf.RoundToInt(yTileMousePos);

        //float xTilePosF = xTilePos;
        //float yTilePosF = yTilePos;

        ////�����µ�
        //Vector2 posOffSet = Vector2.zero;        
        //Vector3Int tileOffset = Vector3Int.zero;//Ÿ����ġ ������ó��


        //1. Ȧ������ ������ �߻� => MathF.RoundToInt ����
        //Ư����ġ�� Ȧ���Ͻ� ó��
        //switch (hexTileType)
        //{
        //    case HexTileType.FlatTop:
        //        //float addYPos = tilePos.x % 2 == 1 ? (tileDistance + heightSpacingValue) * 0.5f : 0;
        //        //addPos = new Vector2(0, addYPos);
        //        if (xTilePos % 2 == 1)
        //        {
        //            posOffSet = new Vector2(0, ySpacing * 0.5f);
        //            //offset = new Vector3Int(0, -Mathf.RoundToInt(spacing));
        //        }
        //        break;
        //    case HexTileType.PointyTop:

        //        //Ȧ���Ͻ� ó��


        //        if (yTilePos % 2 == 1)
        //        {
        //            //�������ó��
        //            int mulValue = (int)Mathf.Sign(xPos);
        //            mulValue = mulValue >= 0 ? 1 : mulValue;

        //            //������
        //            //xTilePos = Mathf.RoundToInt(xPos + xHalfSpacing);

        //            ////���������� +-1//0�� �ƴϰ� ����
        //            ////xTilePosF = 1 * mulValue;

        //            //posOffSet.x = RoundToNear(xTileMousePos, xTilePosF, xHalfSpacing);

        //            //xTilePosF�̰ɷ� �۾��ؾߵ�//�̰ɷ� �۾��ҷ��� xTileMousePos �̰ɷ� ��ȣ�ۿ��ؾߵǰ�
        //            //���� 0 �� ����ó���ϰ�
        //            //xTilePos = Mathf.RoundToInt(xTileMousePos - xHalfSpacing);
        //            //tilePosXF += tilePosX * xSpacing;

        //            Debug.Log($"{xPos}\n{(xTilePos * xSpacing) + xHalfSpacing}");

        //            //posOffSet = new Vector2(tilePosXF, 0) * mulValue;
        //            //offset = new Vector3Int(-Mathf.RoundToInt(spacing), 0);
        //            //Ÿ�������� �����ִ°� ��������
        //        }
        //        break;
        //}


        //��ȸ�ؼ� ó��
        Vector3Int targetTilePos = Vector3Int.zero;
        foreach (var item in hexTileObjectBible)
        {
            var hexTileObject = item.Value;
            var hexTileData = hexTileObject.GetHexTileData;
            Vector3 hexTileWorldPos = ConventAxis(hexTileObject.transform.localPosition);//�ຯ��            

            if (!CheckRoundToNear(targetPos.y, hexTileWorldPos.y, yHalfSpacing))
            {
                continue;
            }


            if (!CheckRoundToNear(targetPos.x, hexTileWorldPos.x, xHalfSpacing))
            {

                continue;
                
            }

            targetTilePos = hexTileData.GetTilePos();
            //Debug.Log(targetTilePos);
            break;
        }


        //tilePosX = RoundToNear(xPos / xSpacing,, xHalfSpacing);
        //tilePosY = RoundToNear(yPos / ySpacing,, yHalfSpacing);







        //float positionValue = (xSpacing * 0.5f) + xSpacing / 2;
        //positionValue += (tilePosX - 1) * xSpacing;

        //���� 2
        //Ȧ���Ͻô� ���ݰ����� �߰� == 5 * 0.5f;
        //Ȧ��

        //Ȧ��//1 3 5 => ����� ���ؼ� 2 4 
        //float resultX = RoundToNear(xPos, (tilePosX * xSpacing) +  addPos.x, xSpacing * 0.5f);

        //¦��//0 2 4 => ����� ���ؼ� 1 3//�۵��Ϸ�
        //float resultX = RoundToNear(xPos, tilePosX, xHalfSpacing);
        //float resultY = RoundToNear(yPos,tilePosY + posOffSet.y, yHalfSpacing);
        //Debug.Log($"{resultX},{resultY}");


        //Debug.Log($"{xPos}=>{tilePosX}\n {posOffSet.x},{RoundToNear(posOffSet.x, posOffSet.x, xHalfSpacing)}");
        //Debug.Log($"{xPos}\n{tilePosX}\n{posOffSet}\n{resultX}");
        //Debug.Log($"{xPos},{yPos}\n{tilePosX},{tilePosY}\n{resultX},{resultY}");


        //Debug.Log($"{xPos},{yPos}+{addPos.x},{addPos.y}\n=>{addPos.x + xPos},{addPos.y + addPos.y}");
        //Debug.Log($"{xPos},{yPos}\n{addPos.x},{addPos.y}=>{addPos.x + xPos},{addPos.y + addPos.y}\n{x1IntPos},{y1IntPos}");

        //if (cashVec != newIntPos)
        //{
        //    cashVec = newIntPos;
        //    Debug.Log($"{newPos}\n {newIntPos}");
        //} 

        return targetTilePos;
    }

    /// <summary>
    /// �������� Ư�������� �ݿø������ִ� �Լ�.
    /// RoundToInt�� int�� ���Ǵ� ��Ÿ���°�
    /// </summary>
    /// <param name="value">���� ��</param>
    /// <param name="checkValue">������ �� ��ġ��</param>
    /// <param name="range">����</param>
    /// <returns>����� ���ȿ� ���Դ��� ����</returns>
    private bool CheckRoundToNear(float value, float checkValue, float range)
    {
        float lowerBound = checkValue - range;
        float upperBound = checkValue + range;

        //���� ����üũ
        //0 ~ 0.4, 0.5~0.9
        //0<=0.5, 0.5 < (1 == 0.9999..)
        return lowerBound <= value && value < upperBound;
    }

    /// <summary>
    /// �������� Ư�������� �ݿø������ִ� �Լ�.
    /// RoundToInt�� int�� ���Ǵ� ��Ÿ���°�
    /// </summary>
    /// <param name="value">���� ��</param>
    /// <param name="checkValue">������ �� ��ġ��</param>
    /// <param name="range">����</param>
    /// <returns>����� ������</returns>
    private float RoundToNear(float value, float checkValue, float range)
    {
        //����üũ
        if (CheckRoundToNear(value, checkValue, range))
        {
            //���̸� üũ�ҹ���� �ѱ�
            return checkValue;
        }
        else
        {
            //���̸� �״�� �ѱ�
            return value;
        }
    }

    private Vector3 ConventAxis(Vector3 worldPos)
    {
        switch (createTileAxisType)
        {
            case CreateTileAxisType.XY:
                worldPos = new Vector3(worldPos.x, worldPos.y);
                break;
            case CreateTileAxisType.XZ:
                worldPos = new Vector3(worldPos.x, worldPos.z);
                break;
        }
        return worldPos;
    }
}
