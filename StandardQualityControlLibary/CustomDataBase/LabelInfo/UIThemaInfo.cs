using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace lLCroweTool.UI.UIThema
{
    /// <summary>
    /// UI�׸��� ���� ����
    /// </summary>
    [CreateAssetMenu(fileName = "New UIThemaInfo", menuName = "lLcroweTool/New UIThemaInfo")]
    public class UIThemaInfo : LabelBase
    {
        //UI���� �׸��� ���ϱ� ���� ó��
        //����� �̹����� ���� ���븸 ��Ƶѿ���
        //CSVó���� ���̹��� ��ġȭ�ؼ� ó������4
        //���� �ʹ�������
        //�׸��鸸 ���� CSV �����
        //�������� ���� CSV���� ó������

        //��¥�� ����//��¥�� ¥��
        //�����ұ�//����ƮŸ���� ��������

        //-=����
        //�⺻������
        public UIThemaPreset panelUIThemaPreset = new();

        //������������//�����ǿ�����Ʈ�� ����        
        public UIThemaPreset iconUIThemaPreset = new();
        //public IconPresetInfo iconPresetInfo;//�������ܼ�//���� ó����

        //��ư������
        public UIThemaPreset buttonUIThemaPreset = new();
        public ButtonColorPreset buttonColorPreset = new();
        public ButtonSpriteSwapPreset buttonSpriteSwapPreset = new();

        //�ؽ�Ʈ������//�����ǿ�����Ʈ�� ����
        public UIThemaPreset textUIThemaPreset = new();
        public FontPreset textFontPreset = new();

        public override LabelBaseDataType GetLabelDataType()
        {
            return LabelBaseDataType.Nothing;
        }
    }

    /// <summary>
    /// UI�׸��� �����ų �̹���UIŸ��
    /// </summary>
    public enum ThemaUIType
    {
        Panel,
        Icon,
        Button,
        Text,
    }

    /// <summary>
    /// UI�׸� ������
    /// </summary>
    [System.Serializable]
    public class UIThemaPreset
    {
        //�׵θ�//�޹��//ó������� ��ŷ -1~0���� ó��//0�� ����Ʈ
        //��ư ó���� ������ �����Ƿ� ��� ����//20230904

        //�г�
        public SpritePreset panelSpritePreset = new();

        //�����г�
        public BorderPreset innerPanelBorderPreset = new();
        public SpritePreset innerPanelSpritePreset = new();

        /// <summary>
        /// UI�׸� �����¿� �°� �ʱ�ȭ �ϴ� �Լ�
        /// </summary>
        /// <param name="panelImage">�г��̹���</param>
        /// <param name="innerPanelImage">�����г��̹���</param>
        public void InitImage(Image panelImage, Image innerPanelImage)
        {
            panelSpritePreset.InitImage(panelImage);

            innerPanelBorderPreset.InitImage(innerPanelImage);
            innerPanelSpritePreset.InitImage(innerPanelImage);
        }

        public void SetImageData(Image panelImage, Image innerPanelImage)
        {
            panelSpritePreset.SetImageData(panelImage);

            innerPanelBorderPreset.SetImageData(innerPanelImage);
            innerPanelSpritePreset.SetImageData(innerPanelImage);
        }
    }

    /// <summary>
    /// ��������Ʈ������
    /// </summary>
    [System.Serializable]
    public class SpritePreset
    {
        //�ϳ��� �̹����� �ϳ��� �÷�, ��������Ʈ, ���͸���� �۵�
        //���͸����� ������ ������ ���� ������ ���͸����� ���̴�ó���� ���� ��������
        //�����ؽ��ĸ� �޾ƿͼ� ó���Ҽ� �ְ� �����Ұ�
        public Color color = Color.white;
        public Sprite sprite;
        public Material uiMaterial;//������ �⺻���� �۵��ǰ�
        public bool isUseRaycastTarget = true;

        /// <summary>
        /// ��������Ʈ �����¿� �°� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="image">�̹���</param>
        public void InitImage(Image image)
        {
            image.color = color;
            image.enabled = sprite == null ? false : true;
            image.sprite = sprite;
            image.material = uiMaterial == null ? Canvas.GetDefaultCanvasMaterial() : uiMaterial;//UIDefault ���͸���
            image.raycastTarget = isUseRaycastTarget;
            //Graphic.defaultGraphicMaterial;
        }

        public void SetImageData(Image image)
        {
            color = image.color;
            sprite = image.sprite;
            uiMaterial = image.material;
            isUseRaycastTarget = image.raycastTarget;
        }
    }

    /// <summary>
    /// ��ư�� �÷� ����������
    /// </summary>
    [System.Serializable]
    public class ButtonColorPreset
    {
        //��ư �÷��⺻������        
        public Color highLightColor = lLcroweUtil.GetColor256(246, 246, 246, 256);
        public Color pressedColor = lLcroweUtil.GetColor256(201, 201, 201, 256);
        public Color selectedColor = lLcroweUtil.GetColor256(246, 246, 246, 256);
        public Color disabledColor = lLcroweUtil.GetColor256(201, 201, 201, 128);

        //public Color highLightColor = lLcroweUtil.GetHSVAColor(0, 0, 95, 100);
        //public Color pressedColor = lLcroweUtil.GetHSVAColor(0, 0, 78, 100);
        //public Color selectedColor = lLcroweUtil.GetHSVAColor(0, 0, 95, 100);
        //public Color disabledColor = lLcroweUtil.GetHSVAColor(0, 0, 78, 50);
        [Range(1, 5)]
        public float colorMultiplier = 1f;
        public float fadeDuration = 0.1f;

        /// <summary>
        /// ��ư �÷������¿� �°� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="button">��ư</param>
        public void InitButton(UnityEngine.UI.Button button)
        {
            ColorBlock colorBlock = new ColorBlock();
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = highLightColor;
            colorBlock.pressedColor = pressedColor;
            colorBlock.selectedColor = selectedColor;
            colorBlock.disabledColor = disabledColor;
            colorBlock.colorMultiplier = colorMultiplier;
            colorBlock.fadeDuration = fadeDuration;

            button.colors = colorBlock;
        }

        public void SetButtonData(UnityEngine.UI.Button button)
        {
            var colorBlock = button.colors;
            highLightColor = colorBlock.highlightedColor;
            pressedColor = colorBlock.pressedColor; 
            selectedColor = colorBlock.selectedColor;
            disabledColor = colorBlock.disabledColor;
            colorMultiplier = colorBlock.colorMultiplier;
            fadeDuration = colorBlock.fadeDuration;
        }
    }   


    /// <summary>
    /// ��ư�� ��������Ʈ ����������
    /// </summary>
    [System.Serializable]
    public class ButtonSpriteSwapPreset
    {
        public Sprite highLightSprite;
        public Sprite pressedSprite;
        public Sprite selectedSprite;
        public Sprite disabledSprite;

        /// <summary>
        /// ��ư��������Ʈ ���������¿� �°� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="button">��ư</param>
        public void InitButton(UnityEngine.UI.Button button)
        {
            SpriteState spriteState = new SpriteState();
            spriteState.highlightedSprite = highLightSprite;
            spriteState.pressedSprite = pressedSprite;
            spriteState.selectedSprite = selectedSprite;
            spriteState.disabledSprite = disabledSprite;

            button.spriteState = spriteState;
        }

        public void SetButtonData(UnityEngine.UI.Button button)
        {
            SpriteState spriteState = button.spriteState;
            highLightSprite = spriteState.highlightedSprite;
            pressedSprite = spriteState.pressedSprite;
            selectedSprite = spriteState.selectedSprite;
            disabledSprite = spriteState.disabledSprite;
        }
    }

    /// <summary>
    /// �׵θ� ������
    /// </summary>
    [System.Serializable]
    public class BorderPreset
    {
        //������ ó��
        public float borderTopValue;
        public float borderBottomValue;
        public float borderLeftValue;
        public float borderRightValue;

        /// <summary>
        /// �׵θ������¿� �°� �̹����� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="image">�̹���������Ʈ</param>
        public void InitImage(Image panel)
        {
            //if (!panel.TryGetComponent(out Canvas canvas))
            //{
            //     canvas = panel.GetAddComponent<Canvas>();
            //    //�׷��Ƚ��� ������ �ƴϰ� ���Ͼ��
            //    canvas.vertexColorAlwaysGammaSpace = true;//�ؼ� ��� ����//���߿� �ڼ�������
            //}
            //canvas.overrideSorting = isUseBorderImage;
            var rect = panel.rectTransform;
            rect.SetAnchorPreset(lLcroweUtil.RectAnchorPreset.StretchBoth);

            //Left,Bottom
            rect.offsetMin = new Vector2(borderLeftValue, borderBottomValue);
            //Right,Top//�ִ� ���̿� �������� ����ߵ�
            rect.offsetMax = -new Vector2(borderRightValue, borderTopValue);
        }

        public void SetImageData(Image panel)
        {
            var rect = panel.rectTransform;
            borderRightValue = rect.offsetMax.x;
            borderTopValue = rect.offsetMax.y;
            borderLeftValue = rect.offsetMin.x;
            borderBottomValue = rect.offsetMin.y;
        }



        public enum BorderDrawType
        {
            All,//�����¿�
            TopBottom,//����
            LeftRight,//�¿�
        }
    }


    /// <summary>
    /// ��Ʈ������
    /// </summary>
    [System.Serializable]
    public class FontPreset
    {
        public TMP_FontAsset fontAsset;
        public Color fontColor = Color.black;

        /// <summary>
        /// ��Ʈ�����¿� �°� �ؽ�Ʈ�� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="text">�ؽ�Ʈ</param>
        public void InitText(TextMeshProUGUI text)
        {
            text.rectTransform.SetAnchorPreset(lLcroweUtil.RectAnchorPreset.StretchBoth);
            text.font = fontAsset == null ? TMP_Settings.defaultFontAsset : fontAsset;
            text.color = fontColor;
        }

        public void SetFontData(TextMeshProUGUI text)
        {
            fontAsset = text.font;
            fontColor = text.color;
        }
    }

    
}