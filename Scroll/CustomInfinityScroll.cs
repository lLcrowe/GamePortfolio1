using lLCroweTool.UI.SmoothScroll;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lLCroweTool.UI.InfinityScroll.Scroll
{
    [RequireComponent(typeof(ScrollSmoothMove))]
    public class CustomInfinityScroll : MonoBehaviour
    {
        //������Ʈ
        [Header("������Ʈ")]
        //����ư�� �������ʱ�ȭ�ؾߵ�
        public CellUI_Base cellPrefab;//������ó��
        public EScrollType scrollType;

        //���ʼ�������
        [Space]
        [Header("�����ִ°� ����")]
        [Min(1)]
        public int showCellAmount = 4;//�����ִ� CellAmount
        [Min(1)]
        public int showCellLineAmount = 1;//�ش������ ������ �����ִ� �뵵//����Ʈ1
        public float spacingValue = 20;//����ó��

        [Space]
        [Header("������ ����")]
        [Min(0)]
        public int cellCapacity;//���뷮��//�ܺο��� �޾ƿ��� �����͵�� ������//

        //���ذ�//���Ĵٵ��� ���ұ�
        public List<CellData> cellDataList = new List<CellData>();//��������

        [Header("UIó������")]
        //UIó���� ����
        private List<CellUI_Base> cellUIList = new List<CellUI_Base>();
        private int cellUIAmount;//����Ʈ�뷮ĳ��//����Ʈī��Ʈ

        //�ε��� �������� ��Ƴ����� �����Ͷ� �����Ҷ� ���Ұ� ������
        //�ϴ� ���� �Ǿ��� �ظ���
        private int currentIndex = 0;//ù��° ����
        private int lastIndex;//���������� ���� �ε���//�ٲ������ ���ſ�//Ʈ����
        private ScrollRect scrollRect;//��ũ�ѷ�Ʈ
        private RectTransform contentRect;//��ü���� ũ��

        /// <summary>
        /// ��ũ���� �۵����
        /// </summary>
        public enum EScrollType
        {
            Vertical,//����
            Horizontal,//����
        }

        private void Awake()
        {
            scrollRect = GetComponent<ScrollRect>();
            scrollRect.scrollSensitivity = 100;//�⺻ 100�� ������ ������
            scrollRect.onValueChanged.AddListener(UpdateScroll);
            contentRect = scrollRect.content;

            //Ȱ��ȭó��
            switch (scrollType)
            {
                case EScrollType.Vertical:
                    scrollRect.horizontal = false;
                    //contentRect.anchorMin = new Vector2(0, 1);
                    //contentRect.anchorMax = new Vector2(1, 1);
                    if (scrollRect.horizontalScrollbar != null)
                    {
                        DestroyImmediate(scrollRect.horizontalScrollbar.gameObject);
                    }
                    break;
                case EScrollType.Horizontal:
                    scrollRect.vertical = false;
                    //contentRect.anchorMin = new Vector2(0, 0);
                    //contentRect.anchorMax = new Vector2(0, 1);
                    if(scrollRect.verticalScrollbar != null)
                    {
                        DestroyImmediate(scrollRect.verticalScrollbar.gameObject);
                    }
                    break;
            }
        }

        /// <summary>
        /// �����ͼ���
        /// </summary>
        /// <param name="newCellDataList">�űԵ����͸�ϵ�</param>
        public void Init<T>(List<T> newCellDataList) where T: CellData
        {
            //������ ������ ��Ȱ��ȭ
            for (int i = 0; i < cellUIList.Count; i++)
            {
                cellUIList[i].gameObject.SetActive(false);
            }
            cellUIList.Clear();

            if (newCellDataList != null)
            {
                cellDataList.Clear();
                cellDataList.AddRange(newCellDataList);
            }
            RefreshContentSize();

            //������ ��ȯ
            if (showCellAmount != 0)
            {
                //��ȯ��//�����ִ� ���� * ���ο��� �����ִ� ��
                for (int i = 0; i < showCellAmount * showCellLineAmount; i++)
                {
                    CellUI_Base cellObject = ObjectPoolManager.Instance.RequestDynamicComponentObject(cellPrefab);
                    cellObject.transform.InitTrObjPrefab(Vector2.zero, Quaternion.identity, contentRect, false);
                    cellObject.InitCellUI(cellObject.GetHeight(), cellObject.GetWidth(), Vector2.zero);

                    cellUIList.Add(cellObject);
                }

                //RefreshContentSize();
            }


            //����//�ε���ó���� �״�� �����ؾߵȴٸ�?//���÷������� �ؾߵ�
            cellUIAmount = cellUIList.Count - 1;
            currentIndex = 0;
            lastIndex = currentIndex;
            contentRect.anchoredPosition = Vector2.zero;


            //�����͵���ȭ
            RefreshData();
        }

        public void AddData<T>(T newCellData) where T : CellData
        {
            cellDataList.Add(newCellData);
            RefreshContentSize();

            //��ġ �缱��//������ �ε�����//Ȯ���غ���=>//��������Ʈ�� ���������� �������� �ڵ������� ������
            CellUI_Base startCell = cellUIList[0];
            switch (scrollType)
            {
                case EScrollType.Vertical:
                    contentRect.anchoredPosition = new Vector2(0, startCell.GetHeight() * cellCapacity * showCellAmount);
                    break;
                case EScrollType.Horizontal:
                    contentRect.anchoredPosition = new Vector2(startCell.GetWidth() * cellCapacity * showCellAmount, 0);
                    break;
            }

            RefreshData();
        }

        public void ClearCellData()
        {
            //������ ������ ��Ȱ��ȭ
            for (int i = 0; i < cellUIList.Count; i++)
            {
                cellUIList[i].gameObject.SetActive(false);
            }
            cellUIList.Clear();
            cellDataList.Clear();
            RefreshContentSize();
        }
        
        /// <summary>
        /// ����������� �����ͷ��� �°� �����ϴ� �Լ�.
        /// </summary>
        private void RefreshContentSize()
        {
            cellCapacity = cellDataList.Count;

            //������ ���� ����//������´� ������ ����//���࿩������ �Ѵٸ�//�����͸����� �۾��ؾߵ�
            RectTransform cellRect = cellPrefab.transform as RectTransform;
            float cellHeight = cellRect.sizeDelta.y;
            float cellWidth = cellRect.sizeDelta.x;

            Vector2 size = Vector2.zero;
            bool isOdd = false;
            if (showCellLineAmount != 1)
            {
                isOdd = showCellLineAmount % 2 != 0 ? true : false;
            }
            float addValue = 0;

            switch (scrollType)
            {
                case EScrollType.Vertical:

                    float y = ((cellHeight + spacingValue) * cellCapacity) / showCellLineAmount;
                    //���⼭ ��F�� ó���ؼ� ���ľߵ� Ư�� Ȧ���϶� �����߻��Ǵ°�//���°� �ȸ���
                    addValue = isOdd ? /*(cellHeight + spacing) -*/ (cellHeight + spacingValue) : 0;

                    size = new Vector2(cellWidth * showCellLineAmount + spacingValue, y + addValue - spacingValue);
                    break;
                case EScrollType.Horizontal:
                    float x = ((cellWidth + spacingValue) * cellCapacity) / showCellLineAmount;
                    addValue = isOdd ?/* (cellWidth + spacing) -*/ (cellWidth + spacingValue / showCellLineAmount) : 0;

                    size = new Vector2(x + addValue - spacingValue, cellHeight * showCellLineAmount + spacingValue);
                    break;
            }
            contentRect.sizeDelta = size;
        }

        /// <summary>
        /// �� �������׿� ���� �� �ε����� �������� �Լ�
        /// </summary>
        /// <param name="scrollType">��ũ��Ÿ��</param>
        /// <param name="cellUI">�ش�Ǵ� cellUI</param>
        /// <returns>��ũ��</returns>
        private int GetCalCellIndex(CellUI_Base cellUI)
        {
            switch (scrollType)
            {
                case EScrollType.Vertical:
                    return (int)(contentRect.anchoredPosition.y / (cellUI.GetHeight() + spacingValue));
                case EScrollType.Horizontal:
                    return (int)(contentRect.anchoredPosition.x / (cellUI.GetWidth() + spacingValue)) * -1;//�ݴ����
            }
            return 0;
        } 

        /// <summary>
        /// ��ũ�� ������Ʈ//��ũ���̺�Ʈ�� ���
        /// </summary>
        /// <param name="temp">��ǥ</param>
        private void UpdateScroll(Vector2 temp)
        {
            if (cellUIList.Count == 0)
            {
                return;
            }

            //üũ�� ���ۼ��� ����
            CellUI_Base startCell = cellUIList[0];

            //�ε���üũ
            //���� ������ ��������Ŀ��ġ / ���⿡���� ���� or ���� + ����
            currentIndex = GetCalCellIndex(startCell) * showCellLineAmount;
            //print($"{currentIndex}");
            currentIndex = currentIndex < 0 ? 0 : currentIndex;//0����ó��

            //������ �ѱ�
            if (lastIndex == currentIndex)
            {
                return;
            }

            //���ΰ����� �Ʒ��� ������ üũ
            bool isUpScroll = lastIndex > currentIndex ? true : false;
            lastIndex = currentIndex;

            //�����͵���ȭ
            if (isUpScroll)
            {
                //���� �ö󰡸�
                //�ǹؿ� �ִ� ģ������ ���� �ø�
                //Debug.Log($"���� ������");

                //���θ�ŭ
                for (int i = 0; i < showCellLineAmount; i++)
                {
                    CellUI_Base endCell = cellUIList[cellUIAmount];

                    //����Ʈ�� �ε�������
                    //endCell.transform.SetAsFirstSibling();//�̰� �ʿ��ϴ�?
                    cellUIList.Remove(endCell);
                    cellUIList.Insert(0, endCell);
                }
            }
            else
            {
                //�Ʒ��� ��������
                //�� ���� �ִ� ģ���� ������ ����
                //Debug.Log($"������ ������");

                //���θ�ŭ
                for (int i = 0; i < showCellLineAmount; i++)
                {
                    startCell = cellUIList[0];

                    cellUIList.Remove(startCell);
                    cellUIList.Add(startCell);
                }
            }

            //��ġ����ȭ + ������ó��
            RefreshData();
        }


        /// <summary>
        ///UI��ġ����ȭ, UI������ó���ϴ� �Լ�
        /// </summary>
        private void RefreshData()
        {
            //�巡�� ����ϸ� ��ġ���� ������ �־ ���� ���� �̰ɷ� ó��
            //���Ʒ��� �̵���Ű�� ����ȭ ó����������
            
            int curLine = lastIndex / showCellLineAmount;//���� ���� �� ������ üũ//�߰��ȶ��ο��� ����
            int checkAddLine = 0;//�߰��Ǵ� ������ üũ
            //Ź ŸŸŹ ���� �����Ű��

            for (int i = 0; i < showCellAmount * showCellLineAmount; i++)
            {
                CellUI_Base cellUI = cellUIList[i];
                int dataIndex = i + lastIndex;//�������ε��� == �������ε��� + �ݺ� Ƚ��               
                
                //�� ������ �Ʒ����� * ���� + (���� or ����) * ������Ʈ�ε��� + �������ε���
                if (dataIndex >= cellDataList.Count)
                {
                    //�����Ͱ� ������//��������
                    if (cellUI.gameObject.activeSelf)
                    {
                        cellUI.gameObject.SetActive(false);
                    }
                    continue;
                }
                else
                {
                    //�����Ͱ� ������ �����ֱ�
                    if (!cellUI.gameObject.activeSelf)
                    {
                        cellUI.gameObject.SetActive(true);
                    }

                    //������ĳ��
                    float cellHeight = cellUI.GetHeight();
                    float cellWidth = cellUI.GetWidth();
                    CellData cellData = cellDataList[dataIndex];
                    Vector2 pos = Vector2.zero;

                    //��ġ����
                    switch (scrollType)
                    {
                        case EScrollType.Vertical:
                            //ù��ġ ����
                            pos = Vector2.down * (cellHeight + spacingValue) * curLine;


                            //�̰� Ȧ���϶� ó���ϴ°� ����� ���ߴ°�  �����ϱ��ѵ� �������ƴϴ�//���߿� �ð��Ǹ�
                            //1�Ͻø� �߰�
                            if (showCellLineAmount == 1)
                            {
                                pos += Vector2.right * (spacingValue * 0.5f);
                            }

                            //�������� �ø��°� üũ
                            if (checkAddLine != 0)
                            {
                                //0���� �ƴϸ� �������� �Ѱ���
                                pos += Vector2.right * (cellWidth + spacingValue) * checkAddLine;
                            }
                            checkAddLine++;

                            //���ο��� �����ִ� ���� ���������� 
                            if (checkAddLine >= showCellLineAmount)
                            {   
                                curLine++;//������������ ����
                                checkAddLine = 0;//���ο� �߰��Ǵ� �� üũ�ϴ°� ����
                            }
                            break;
                        case EScrollType.Horizontal:
                            //ù��ġ ����
                            pos = Vector2.right * (cellWidth + spacingValue) * curLine;

                            //�Ʒ��� �ø��°� üũ
                            if (checkAddLine != 0)
                            {
                                //0���� �ƴϸ� �Ʒ��� �Ѱ���
                                pos += Vector2.down * (cellHeight + spacingValue) * checkAddLine;
                            }
                            checkAddLine++;

                            //���ο��� �����ִ� ���� ���������� 
                            if (checkAddLine >= showCellLineAmount)
                            {
                                curLine++;//������������ ����
                                checkAddLine = 0;//���ο� �߰��Ǵ� �� üũ�ϴ°� ����
                            }
                            break;
                    }

                    //����,���� & ������ġ & ������ó��
                    cellUI.InitCellUI(cellHeight, cellWidth, pos);
                    cellUI.SetData(cellData);//������ó��
                }
            }
        }
    }
}


