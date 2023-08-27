using Assets;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.TextCore.Text;
using UnityEngine.Tilemaps;
using Unity.VisualScripting;
using lLCroweTool.TimerSystem;
using lLCroweTool;
using static lLCroweTool.lLcroweUtil.HexTileMatrix;

public class HexTest : MonoBehaviour
{
    //�����Ӱ� �ʹ�ġ


    //ī�޶�Ŭ����ġ�� üũ�ؾߵ�//�ָ��ѵ� �����ߴ�

    Tilemap tilemap;


    [System.Serializable]
    public class HexTileInfoTest
    {
        public HexTest parentHexTileMap;

        public Vector3Int hexTilePos;//��Ÿ�Ϸ�����ġ
        public GameObject batchObject;//��ġ�� ������Ʈ üũ//��ֹ��̵�//�����̵�//
        //���߿� �ٸ�������Ʈ�� ��ü�ؾ� �ɵ�

        //�ֺ� ��Ÿ�� üũ
        //�������� ������� Ÿ�Ϻ��� �������Ÿ�ϱ��� �� 6���� �޴´�

        public bool[] isExistNearSideHexTileArray;
        public Vector3Int[] nearSideHexTileArray;//�̰� ��Ÿ�������� �ߴٰ� �������ܼ� ��ġ������ ������

        /// <summary>
        /// ��Ÿ�� �ʱ�ȭ
        /// </summary>
        /// <param name="hexTest">��Ÿ�ϸ� �θ�</param>
        /// <param name="pos">��Ÿ�� ������ġ ����</param>
        public HexTileInfoTest(HexTest hexTest, Vector3Int pos)
        {
            parentHexTileMap = hexTest;
            hexTilePos = pos;


        }

        /// <summary>
        /// ��ġ������Ʈ�� ����
        /// </summary>
        /// <param name="targetObject">Ÿ���� �� ������Ʈ</param>
        public void SetBatchObject(GameObject targetObject)
        {
            batchObject = targetObject;
        }

        //ã���͵�//������ܺ��� ������ܱ���
        private static Vector3Int[] checkPosArray =
        {
            new Vector3Int(1, 0, 1),
            new Vector3Int(1, 0, 0),
            new Vector3Int(1, 0, -1),
            new Vector3Int(0, 0, -1),
            new Vector3Int(-1, 0, 0),
            new Vector3Int(0, 0, 1),
        };

        public void FindNearSideHexTile()
        {
            //������ܺ��� �ð����(��������)���� ���ư��� 
            //������ܱ��� üũ


            isExistNearSideHexTileArray = new bool[6];
            nearSideHexTileArray = new Vector3Int[6];

            for (int i = 0; i < checkPosArray.Length; i++)
            {
                int index = i;
                //�ش���ġ�� Ÿ�ϸ��� �ִ���
                if (parentHexTileMap.hexTileInfoBible.TryGetValue(checkPosArray[index] + hexTilePos, out HexTileInfoTest hexTileInfo))
                {
                    isExistNearSideHexTileArray[index] = true;
                    nearSideHexTileArray[index] = hexTileInfo.hexTilePos;
                }
                else
                {
                    isExistNearSideHexTileArray[index] = false;
                    nearSideHexTileArray[index] = Vector3Int.zero;
                }
            }

        }

    }

    public float speed;
    public GameObject targetObject;
    public Vector3Int currentPos;
    public Vector3Int targetPos;


    //�� �������//������
    public List<HexTileInfoTest> hexInfoList = new List<HexTileInfoTest>();

    public Dictionary<Vector3Int, HexTileInfoTest> hexTileInfoBible = new Dictionary<Vector3Int, HexTileInfoTest>();


    //����Ÿ�ϸ�üũ
    public HexTileType hexType;
    public float size;

    public TimerModule_Element timer;    

    public int height = 5;
    public int width = 6;
    public GameObject prefab;

    public float heightInterval = 2f;
    public float widthInterval = 1.75f;

    public Vector3 p0;
    public Vector3 p1;
    public Vector3 p2;
    public Vector3 p3;

    private void Awake()
    {
        for (int i = 0; i < height; i++)
        //for (int i = 0; i < 2; i++)
        {
            int indexZ = i;
            //int indexXAddValue = 0;//¦�������� �ִ� �迭 ��ġ���� ó��

            //���̰� ¦���϶�
            int size = 0;
            if (indexZ % 2 != 0)
            {
                size--;
                //indexXAddValue += 1;
            }

            for (int j = 0; j < width + size; j++)
            //for (int j = 0; j < 7 + size; j++)
            {
                int indexX = j;


                //��ġ�����ϱ�
                //widthInterval

                Vector3Int curPos = new Vector3Int(indexX, 0, indexZ);

                HexTileInfoTest hexTileInfo = new HexTileInfoTest(this, curPos);

                hexTileInfoBible.Add(new Vector3Int(indexX, 0, indexZ), hexTileInfo);
                hexInfoList.Add(hexTileInfo);
            }
        }

        for (int i = 0; i < hexInfoList.Count; i++)
        {
            hexInfoList[i].FindNearSideHexTile();
        }
    }

    private void Update()
    {
        //if (timer.CheckTimer())
        //{
        //    Debug.Log("Tick");
        //}

        //��ã�⸦ �غ���
        //Camera.main.ScreenToWorldPoint();

        Vector3 originPos = transform.position;
        Vector3 lastPoint = Vector3.zero + originPos;
        if (hexTileInfoBible.ContainsKey(targetPos))
        {
            HexTileInfoTest hexTileInfoTest = hexTileInfoBible[targetPos];
            Vector3Int hexTilePos = hexTileInfoTest.hexTilePos;

            //z�� Ȧ�� �Ͻ�
            float startXPos = 0;

            if (hexTilePos.z % 2 != 0)
            {
                startXPos += size;
            }

            lastPoint = originPos + new Vector3((hexTilePos.x * heightInterval * size) + startXPos, 0, hexTilePos.z * widthInterval * size);
        }
        //�̵�
        targetObject.transform.position = Vector3.Lerp(targetObject.transform.position, lastPoint, speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Mouse0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Vector3 mousePoint = ray.origin;
            Debug.Log(mousePoint);
            mousePoint = new Vector3(mousePoint.x, 0, mousePoint.z);

            //z�� Ȧ�� �Ͻ�
            //float startXPos = 0;

            //if (targetPos.z % 2 != 0)
            //{
            //    startXPos += outerSize;
            //}

            //mousePoint = originPos + new Vector3((mousePos.x * heightInterval * outerSize) + startXPos, 0, mousePos.z * widthInterval * outerSize);
            //mousePos = Vector3Int.CeilToInt(mousePoint);
            //targetPos = new Vector3Int(mousePos.x, 0, mousePos.z);

            //���������δ� üũ�� �ݹ��




            //�پ������� �������
            //��������, �����������̶� üũ�� ��Ȯ�� �ض�




            for (int i = 0; i < hexInfoList.Count; i++)
            {
                int index = i;
                Vector3Int hexTilePos = hexInfoList[index].hexTilePos;
                //z�� Ȧ�� �Ͻ�
                float startXPos = 0;

                if (hexTilePos.z % 2 != 0)
                {
                    startXPos += size;
                }
                lastPoint = originPos + new Vector3((hexTilePos.x * heightInterval * size) + startXPos, 0, hexTilePos.z * widthInterval * size);


                //Debug.Log($"���콺{mousePoint} : ��Ÿ��{checkPos}");

                if (Vector3.Distance(mousePoint, lastPoint) < size)
                {
                    Debug.Log($"{hexInfoList[index].hexTilePos} : Find");
                    targetPos = hexInfoList[index].hexTilePos;
                    break;
                }

            }



            //targetObject.transform.position = mousePos;
        }




        //if (Check(targetPos, currentPos))
        //{
        //    Debug.Log($"�����Ҽ� �ֽ��ϴ�.");
        //}
        //else
        //{
        //    Debug.Log($"�����Ҽ� �����ϴ�.");
        //}




    }

    private static Queue<HexTileInfoTest> hexTileInfoQueue = new Queue<HexTileInfoTest>(100);

    private bool Check(Vector3Int currentPos, Vector3Int targetPos)
    {
        //���� ��ġ�� �����ϴ��� üũ
        if (!hexTileInfoBible.TryGetValue(currentPos, out HexTileInfoTest hexTileInfo))
        {
            return false;
        }

        //Ÿ�� ��ġ�� �����ϴ��� üũ
        if (!hexTileInfoBible.ContainsKey(targetPos))
        {
            return false;
        }

        bool isFind = false;
        hexTileInfoQueue.Clear();
        hexTileInfoQueue.Enqueue(hexTileInfo);

        //�̸�����ֱ�

        do
        {
            //�۵�
            //�ش�Ÿ�� �ֺ� ģ��Ÿ�ϵ��� üũ
            hexTileInfo = hexTileInfoQueue.Dequeue();
            for (int i = 0; i < 6; i++)
            {
                int index = i;
                //Ÿ���� �����ϴ���
                if (!hexTileInfo.isExistNearSideHexTileArray[index])
                {
                    continue;
                }

                //Ÿ����ġ���� üũ
                Vector3Int tempPos = hexTileInfo.nearSideHexTileArray[index];
                if (tempPos == targetPos)
                {
                    //Ÿ����ġ�� ����ֱ�
                    isFind = true;
                    break;
                }
                else
                {
                    //Ÿ����ġ�� �ƴϸ� ����ֱ�
                    hexTileInfoQueue.Enqueue(hexTileInfoBible[tempPos]);
                }
            }
        } while (hexTileInfoQueue.Count <= 0);

        return isFind;
    }


    private void OnDrawGizmos()
    {
        //������
        for (int i = 0; i < 10; i++)
        {
            int index = i;
            Gizmos.DrawWireSphere(new Vector3(lLcroweUtil.ThreePointBezier(p0.x, p1.x, p2.x, index), 0, lLcroweUtil.ThreePointBezier(p0.z, p1.z, p2.z, index)), 0.2f);
        }

        //��ó��

        //if (Application.isPlaying)
        //{
        //    if (Check(currentPos, targetPos)) 
        //    {
        //        Gizmos.color = Color.blue;
        //    }
        //    else
        //    {
        //        Gizmos.color = Color.red;
        //    }

        //    Gizmos.DrawLine(currentPos, new Vector3(targetPos.x * widthInterval, 0, targetPos.z * heightInterval));

        //}

        Gizmos.color = Color.white;

        Vector3 originPos = transform.position;

        switch (hexType)
        {
            case HexTileType.PointyTop:
                //outer�� ���Ϸ� ���ִ� ���
                //�¿���� �����ϰ� ���ϸ� �����ؾߵ�


                for (int i = 0; i < height; i++)
                //for (int i = 0; i < 2; i++)
                {
                    int indexZ = i;
                    float startXPos = 0;

                    //���̰� ¦���϶�
                    int size = 0;
                    if (indexZ % 2 != 0)
                    {
                        size--;
                        startXPos += this.size;
                    }

                    for (int j = 0; j < width + size; j++)
                    //for (int j = 0; j < 7 + size; j++)
                    {
                        int indexX = j;


                        //��ġ�����ϱ�
                        //widthInterval
                        Handles.Label(originPos + new Vector3((indexX * heightInterval * this.size) + startXPos, 0, indexZ * widthInterval * this.size), $"�迭:{indexX}, {indexZ}");


                        DrawHex(originPos + new Vector3((indexX * heightInterval * this.size) + startXPos, 0, indexZ * widthInterval * this.size));
                    }
                }


                break;
            case HexTileType.FlatTop:


                //outer�� �¿�� ���ִ� ���
                //���ϸ����۾��ϰ� ������ �����ؾߵ�

                for (int i = 0; i < width; i++)
                {
                    int indexX = i;
                    float startZPos = 0;

                    //width�� Ȧ���κ��̸� ���ݳ��̿��� +1 ũ��
                    int size = 0;
                    if (indexX % 2 != 0)
                    {
                        size++;
                        startZPos -= this.size;
                    }

                    for (int j = 0; j < height + size; j++)
                    {
                        int indexZ = j;

                        DrawHex(originPos + new Vector3(indexX * widthInterval * this.size, 0f, (indexZ * heightInterval * this.size) + startZPos));
                    }
                }
                break;
        }
    }

    private void DrawHex(Vector3 originPos)
    {
        Vector3[] tempArray = null;
        
        tempArray = lLcroweUtil.HexTileMatrix.GetHexTilePoint(originPos, size, hexType, CreateTileAxisType.XZ);

        Gizmos.DrawLine(tempArray[0], tempArray[1]);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(tempArray[1], tempArray[2]);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(tempArray[2], tempArray[3]);
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(tempArray[3], tempArray[4]);
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(tempArray[4], tempArray[5]);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(tempArray[5], tempArray[0]);
        Gizmos.color = Color.blue;
    }
}
