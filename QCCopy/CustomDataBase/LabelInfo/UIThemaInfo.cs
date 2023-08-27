using lLCroweTool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIThemaInfo : LabelBase
{
    //UI���� �׸��� ���ϱ� ���� ó��
    //����� �̹����� ���� ���븸 ��Ƶѿ���

    //CSVó���� ���̹��� ��ġȭ�ؼ� ó������


    //�̸��� �����غ��߰���
    //background => bg => panel


    //�⺻����
    //CustomImage//�׵θ��̹���//�̹���//��ư
    //  Panel//�г��̹���//�׵θ����//�̹���
    //-----Ȯ�屸��(������,�ؽ�Ʈ)
    //      Icon
    //      �ؽ�Ʈ

    //�����ܵ�, ��ư��׶��� �׵θ� UI��ó��
    //�̰����� ���۷������� ��Ģã�� ���� ó��


    public class SpritePreset
    {
        //�ϳ��� �̹����� �÷��� ��������Ʈ�� �۵�
        public Color color = Color.white;
        public Sprite sprite;
    }

    public class ButtonColorPreset
    {
        public Color highLightColor;
        public Color pressedColor;
        public Color selectedColor;
        public Color disabledColor;
        public float fadeDuration;
    }

    public class ButtonSpriteSwapPreset
    {
        public Sprite highLightSprite;
        public Sprite pressedSprite;
        public Sprite selectedSprite;
        public Sprite disabledSprite;
    }

    //��������� ���� Booló���� üũ
    //CSV ����Ʈ�Ҷ� ���ҽý�üũ�ϴµ� �ű⼭ null�� ��������� nullSprite�� üũ�Ǵ� �ƿ� �۵����ϰ� ó��

    //�޹��//�г�
    //�׵θ�//=> ó������� ��ŷ
    public bool isUseBorder;
    public float borderValue;


    //������
    









    //��̹����� ����Ұ��ΰ�
    
    

    public override LabelBaseDataType GetLabelDataType()
    {
        return LabelBaseDataType.Nothing;
    }
}
