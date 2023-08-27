using System.Collections.Generic;
using UnityEngine;
using lLCroweTool.Dictionary;
using lLCroweTool.TimerSystem;
using lLCroweTool.AstarPath;
using DG.Tweening;
using System.Collections;
using Doozy.Engine.Extensions;

namespace lLCroweTool.TileMap.HexTileMap
{
    public class HexBasementTileMap : MonoBehaviour
    {
        //�����̽���ƮŸ�ϸ�
        //Ư��Ÿ�ϸ��� �̿��Ͽ� ���ο��Ŀ������ �۵���Ű�����ִ� ����� ����


        //��Ÿ�ϸ�(��Ŀ�������)
        //Ÿ�Ͽ� �������ִ���üũ
        //BFS�� ���� ¥��//��ã��
        //����? �װ͵� ������
        //Ÿ�ϸʿ� ����


        public LayerMask obstacleLayer;
        public int batchLimitPos = 4;
        [SerializeField] protected Custom3DHexTileMap tilemap;

        [System.Serializable] public class HexAreaBible : CustomDictionary<Vector3Int, HexTileData> { }
        [Header("������ ��ųʸ�")]
        //��ġ���� �ߺ���������
        public HexAreaBible hexAreaBible = new HexAreaBible();
        public IAstarNodeBible astarNodeBible = new IAstarNodeBible();

        public List<HexTileObject> tileObjectList = new List<HexTileObject>();
        public Vector3 tileMapCenterPos;//�߾���ġ
        public TimerModule_Element timerModule;

        /// <summary>
        /// ���� ��Ÿ�� üũ�뵵
        /// </summary>
        private Dictionary<HexTileData, bool> calHexAeaInfoBible = new Dictionary<HexTileData, bool>();
        private Dictionary<Vector3Int, bool> calCashGasBible = new Dictionary<Vector3Int, bool>();//����� ĳ���Ұ���

        public int oxygenTileCount;//Ÿ�ϰ���

        [Header("�ִϸ��̼ǰ���")]
        public float waitFallTimer = 0.01f;
        public float fallSpeed = 0.5f;
        public float punchSpeed = 0.3f;

        //private TilemapCollider2D tilemapCol2D;//����üũ��//����������ֵ� �ִ°� ������ üũ�ϱ�
        //�̰Կ켱������ �� ������ ����
        public Vector3Int[] vector3IntArray;
        //[SerializeField] protected Tilemap tilemap;

        protected void Awake()
        {
            if (tilemap == null)
            {
                tilemap = GetComponent<Custom3DHexTileMap>();
            }
            
            timerModule.SetTimer(1f);
            InitHexBasementTileMap();
            //StartCoroutine(ActionFallHexTile());
        }

        public void InitHexBasementTileMap()
        {
            //������ Ÿ�ϵ��� �����ͼ� ó��
            int i = 0;
            vector3IntArray = new Vector3Int[tilemap.hexTileObjectBible.Count];
            tileObjectList.Clear();
            Vector3 total = Vector3.zero;
            foreach (var item in tilemap.hexTileObjectBible)
            {
                Vector3Int tilePos = item.Key;
                HexTileObject hexTileObject = item.Value;
                HexTileData hexTileData = hexTileObject.GetHexTileData;
                hexTileData.InitHexTileData(tilePos, hexTileObject, tilemap);
                if (tilePos.x < batchLimitPos)
                {
                    //���� ¦���϶� ���ϴ°� üũ
                    hexTileData.IsBatchTile = true;//��ġ��ġ��
                    float a = hexTileObject.TileColor.a;
                    hexTileObject.TileColor = new Color(0.5f, 0, 0, a);
                }


                hexAreaBible.Add(tilePos, hexTileData);
                astarNodeBible.Add(tilePos, hexTileData);
                tileObjectList.Add(hexTileObject);

                total += hexTileData.GetWorldPos();
                vector3IntArray[i] = tilePos;
                i++;
            }

            //�߾���ġ
            tileMapCenterPos = total / vector3IntArray.Length;
            oxygenTileCount = vector3IntArray.Length;
        }

        /// <summary>
        /// ������Ʈó���ؼ� ��Ÿ�ϵ��� ���¸� ������Ʈ�����ִ� �Լ�
        /// </summary>
        public void InitHexTileObjectList()
        {
            //������Ʈó���ؼ� ��Ÿ�ϵ��� ���¸� ������Ʈ���Ѽ� 
            //��ã�Ⱑ �������� üũ��
            foreach (var item in tileObjectList)
            {
                item.UpdateHexTileData();
            }
        }

        [ButtonMethod]
        public void Test()
        {
            StartCoroutine(ActionFallHexTile());
        }


        private IEnumerator ActionFallHexTile()
        {
            for (int i = 0; i < tileObjectList.Count; i++)
            {
                tileObjectList[i].transform.localPosition += Vector3.up * 5;
            }

            var tempWait = new WaitForSeconds(waitFallTimer);
            for (int i = 0; i < tileObjectList.Count; i++)
            {   
                tileObjectList[i].transform.DOLocalMoveY(0, fallSpeed);
                yield return tempWait;
            }

            if (punchSpeed > 0.001f)
            {

                Vector3 punch = Vector3.one * 1.2f;
                for (int i = 0; i < tileObjectList.Count; i++)
                {
                    tileObjectList[i].transform.DOPunchScale(punch, punchSpeed);
                }
            }            
        }

        /// <summary>
        /// ������ �ʱ�ȭ
        /// </summary>
        public void ResetHexArea()
        {
            var HexAreaInfoList = hexAreaBible.GetValueList();
            for (int i = 0; i < HexAreaInfoList.Count; i++)
            {
                HexTileData hexAreaInfo = HexAreaInfoList[i];
                hexAreaInfo.ResetHexTileArea();
            }
        }

        [ButtonMethod]
        public void GetTilePos()
        {
            if (tilemap == null)
            {
                tilemap = GetComponent<Custom3DHexTileMap>();
            }
            vector3IntArray = lLcroweUtil.GetAllTilePos(tilemap);//����ġŸ�� ��������
        }

        public void ClearTileColor()
        {
            for (int i = 0; i < tileObjectList.Count; i++)
            {
                tileObjectList[i].TileColor = new Color(0.45f,0.45f,0.45f);
            }
        }

        private void Update()
        {
            if (!timerModule.CheckTimer())
            {
                return;
            }

            //Profiler.BeginSample("-=OxygenUpdateLoop!=-");
            HexTileData voidRoom = null;

            for (int i = 0; i < vector3IntArray.Length; i++)
            {
                if (hexAreaBible.ContainsKey(vector3IntArray[i]))
                {
                    //Profiler.BeginSample("-=OxygenUpdate!=-");
                    voidRoom = hexAreaBible[vector3IntArray[i]];
                    UpdateHexAreaInfo(ref voidRoom, this);
                    //Profiler.EndSample();
                }

            }
            //Profiler.EndSample();

            calHexAeaInfoBible.Clear();

            //��Ƶа����� �������氪���� �����Ŵ
            foreach (var item in calCashGasBible)
            {
                Vector3Int pos = item.Key;
                hexAreaBible[pos].isGasArea = true;
                lLcroweUtil.SetTile(pos, Color.red, tilemap);
            }
            calCashGasBible.Clear();
        }

        /// <summary>
        /// ���� ������Ʈ
        /// </summary>
        public static void UpdateHexAreaInfo(ref HexTileData hexAreaInfo, HexBasementTileMap targetBasementTilemap)
        {
            //�������� ������Ʈ//���߿� �������� ���������͵� �����ҵ���

            //ž��
            //�����ǥ�� �������� üũ
            var tempBible = targetBasementTilemap.calHexAeaInfoBible;
            //�̹̰�������� �Ѿ��
            if (tempBible.ContainsKey(hexAreaInfo))
            {
                return;
            }
            tempBible.Add(hexAreaInfo, false);

            //���๰(���ع�)�̸� ������
            if (hexAreaInfo.GetHexTileObject.BatchUnitObject != null)
            {
                var structUnit = hexAreaInfo.GetHexTileObject.BatchUnitObject as StructureUnitObject;
                if (structUnit != null)
                {
                    return;
                }
            }

            //�������°� �ƴϸ� �Ѿ��
            if (!hexAreaInfo.isGasArea)
            {
                return;
            }

            HexTileData tempHexTileData = null;
            HexBasementTileMap tempBasementTilemap = targetBasementTilemap;
            var calCashGasBible = tempBasementTilemap.calCashGasBible;

            //��óŸ�ϵ��� �� üũ��
            for (int i = 0; i < hexAreaInfo.NearCheckAreaArray.Length; i++)
            {
                int index = i;
                if (hexAreaInfo.NearCheckAreaArray[index])
                {
                    //������ ��Ŀ���� �۵�
                    //�ֺ� ��Ÿ��

                    //���翩��üũ
                    if (!tempBasementTilemap.GetHexAreaInfo(hexAreaInfo.NearAreaArray[index], out tempHexTileData))
                    {
                        continue;
                    }

                    //������ Ÿ�ϵ��� �������
                    if (tempHexTileData.isGasArea)
                    {
                        //���Ÿ���� ���������̸� �Ѿ��
                        continue;
                    }

                    //���Ÿ���� ���ع��̸� �Ѿ��
                    if (tempHexTileData.GetHexTileObject.BatchUnitObject != null)
                    {
                        var structUnit = tempHexTileData.GetHexTileObject.BatchUnitObject as StructureUnitObject;
                        if (structUnit != null)
                        {
                            continue;
                        }
                    }

                    //��꿡 ����ֱ�
                    if (!calCashGasBible.ContainsKey(tempHexTileData.GetTilePos()))
                    {
                        calCashGasBible.Add(tempHexTileData.GetTilePos(), false);
                    }

                }
            }
        }

        public void SetGas(Vector3Int pos)
        {
            if (!hexAreaBible.ContainsKey(pos))
            {
                return;
            }
            hexAreaBible[pos].isGasArea = true;
            lLcroweUtil.SetTile(pos, Color.red, tilemap);
        }

        /// <summary>
        /// �ش���ġ�� ��Ÿ�ϵ����͸� ������
        /// </summary>
        /// <param name="pos">Ÿ����ġ</param>
        /// <param name="hexTileData">��Ÿ�ϵ�����</param>
        /// <returns></returns>
        public bool GetHexAreaInfo(Vector3Int pos, out HexTileData hexTileData)
        {
            return hexAreaBible.TryGetValue(pos, out hexTileData);
        }

        /// <summary>
        /// Ÿ�ϸ� ���������Լ�
        /// </summary>
        /// <returns>Ÿ�ϸ�</returns>
        public Custom3DHexTileMap GetTileMap()
        {
            return tilemap;
        }
    }
}
