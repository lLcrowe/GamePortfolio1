using lLCroweTool.DataBase;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace lLCroweTool.UnitBatch
{
    public class UnitBatchCardUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler ,IPointerDownHandler,IPointerUpHandler
    {
        //UI
        //����ī��
        public UnitObjectInfo targetUnitInfo;

        //�̹��� �����Ҽ� �ְ� ó�����ֱ�
        private Image targetImage;//ī���̹���
        public Image classImage;//Ŭ�����̹���
        public Image charecterImage;//ĳ�����̹���
        public TextMeshProUGUI unitNameText;//���ֳ���


        private void Awake()
        {
            targetImage = GetComponent<Image>();
            Image[] imageArray = GetComponentsInChildren<Image>();

            for (int i = 0; i < imageArray.Length; i++)
            {
                if (targetImage == imageArray[i])
                {
                    continue;
                }

                imageArray[i].raycastTarget = false;
            }
        }

        /// <summary>
        /// ���ֹ�ġī�� �ʱ�ȭ
        /// </summary>
        /// <param name="unitObjectInfo">��������</param>
        public void InitUnitBatchCardUI(UnitObjectInfo unitObjectInfo)
        {
            targetUnitInfo = unitObjectInfo;
            if (targetUnitInfo == null)
            {
                return;
            }

            classImage.sprite = targetUnitInfo.classIcon;
            charecterImage.sprite = targetUnitInfo.icon;
            unitNameText.text = targetUnitInfo.labelNameOrTitle;
        }


        public void OnPointerDown(PointerEventData eventData)
        {
            UnitBatchUIManager.Instance.SetUnitBatchUI(UnitBatchUIManager.UnitBatchStateType.SelectUnitUI, transform, transform.parent, targetUnitInfo);
            targetImage.raycastTarget = false;//�����ȵǰ��ؼ� �ؿ� ī�� �κ��� �˼� �ְ�
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            //���콺�����͸� ���� �ٽ� �����ǰ� ó��
            targetImage.raycastTarget = true;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            //���콺�����͸� �÷����� �ش�Ǵ� ������Ʈ�� ���οø������� ����
            UnitBatchUIManager.Instance.EnterTheOnCard(transform);
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            //����
            UnitBatchUIManager.Instance.EnterTheOnCard(null);
        }

    }
}



