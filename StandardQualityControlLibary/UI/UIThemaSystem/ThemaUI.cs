using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace lLCroweTool.UI.UIThema
{
    /// <summary>
    /// UI�׸��� Ÿ�������ִ� ThemaUITarget
    /// </summary>
    public class ThemaUI : MonoBehaviour
    {
        //background => bg => panel

        //������ ���� �ܼ��� ����
        //themaUI
        //  panel1//=>image//���ʽ�Ʈ��ġ
        //  panel2//=>image//���ʽ�Ʈ��ġ


        //-=�⺻����
        //-Panel
        //panelBorderImage//�г��� ����//ThemaUITarget
        //  panelImage//�г��̹���//�׵θ����//Canvas

        //-Icon
        //panelBorderImage//ThemaUITarget//�׵θ��̹���
        //  panelImage//�г��̹���//�׵θ����//�̹���//ĵ����
        //      iconImageObject//�������̹���

        //-Button
        //panelBorderImage//ThemaUITarget//�׵θ��̹���
        //button//��ư
        //  panelImage//�г��̹���//�׵θ����//�̹���//ĵ����

        //-Text
        //text//�ؽ�Ʈ
        
        //�׸�UIŸ��
        public ThemaUIType themaUIType;
        public bool isOverrideUIThema;

        //Ÿ�Կ� ���� �ʿ��� ������Ʈ��
        public Image panelImage;//�ڱ��ڽ�
        public Image innerPanelImageObject;//���� �г�

        //�߰� ������Ʈ
        public Button button;//��ưó��//��ư���� ������ �̹����� ���� �־�ߵ�
        //��ȣ�ۿ� ��Ȱ��ȭ�ÿ� ����Ǵ� ������ �����ͼ� �ϴܿ� �ִ� ��� ������Ʈ���� �ٲ�����ҵ���
        //��ư�� ��Ȱ��ȭ �÷��� ��Ȱ��ȭ�ÿ� ���̴� �÷�
        //�ű⿡ ��Ȱ��ȭ�� �ѹ����ν��Ѽ� ó���ؾ��ҵ�
        public Image iconImageObject;//�����ܿ�����Ʈ
        public TextMeshProUGUI textObject;//�ؽ�Ʈ������Ʈ
        private Graphic[] childGraphicArray;//��ư�� ��Ȱ��ȭ�ɶ��� �����ϱ� ���� ó��

        private void Awake()
        {
            childGraphicArray = GetComponentsInChildren<Graphic>();
            //��F���߳��f���� ��ư�����س�����
        }

        private void OnEnable()
        {   
            ThemaUIManager.Instance.InitTargetThemaUI(this);
        }

        /// <summary>
        /// �Ŵ������� ��ü������ �׸��� �ʱ�ȭ�ϱ� ���� �Լ�
        /// </summary>
        /// <param name="themaInfo">UI�׸� ����</param>
        public void InitThemaUI(UIThemaInfo themaInfo)
        {
            if (isOverrideUIThema || themaInfo == null)
            {
                return;
            }

            switch (themaUIType)
            {
                case ThemaUIType.Panel:
                    themaInfo.panelUIThemaPreset.InitImage(panelImage, innerPanelImageObject);
                    break;
                case ThemaUIType.Icon:
                    themaInfo.iconUIThemaPreset.InitImage(panelImage, innerPanelImageObject);                    
                    break;
                case ThemaUIType.Button:
                    themaInfo.buttonUIThemaPreset.InitImage(panelImage, innerPanelImageObject);
                    themaInfo.buttonColorPreset.InitButton(button);
                    themaInfo.buttonSpriteSwapPreset.InitButton(button);
                    break;
                case ThemaUIType.Text:
                    themaInfo.textUIThemaPreset.InitImage(panelImage, innerPanelImageObject);
                    themaInfo.textFontPreset.InitText(textObject);
                    break;
            }
        }

        /// <summary>
        /// ��ưȰ��ȭ ��Ȱ��ȭ ���ִ� �Լ�
        /// </summary>
        /// <param name="value">Ȱ��ȭ ����</param>
        public void SetInteractable(bool value)
        {
            //��ư���Ͻø� �۵�����
            if (themaUIType != ThemaUIType.Button)
            {
                return;
            }
            button.interactable = value;

            for (int i = 0; i < childGraphicArray.Length; i++)
            {
                var temp = childGraphicArray[i];
                temp.color = value ? button.colors.normalColor : button.colors.disabledColor;
            }
        }


        //�� ������Ʈ�� �̾��ִ� �Լ����� ������ ��ŷ�ϱ� ���� �ʿ�����


    }
}

