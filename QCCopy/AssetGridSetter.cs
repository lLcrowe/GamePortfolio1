#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;



public class AssetGridSetter : MonoBehaviour
{
    //���¹�ġ�� ���� ����� ����
    //�������� ������
    //�ڽĿ� �ִ� ���������� ����

    public Transform[] childArray;
    public BatchType batchType;

    public float distance;
    public int rowCount;//��
    

    public enum BatchType
    {
        XY,//2D
        XZ,//3D
    }

    public void BatchObject()
    {
        childArray = GetComponentsInChildren<Transform>();
        List<Transform> tempTrList = new List<Transform>();
        for (int i = 0; i < childArray.Length; i++)
        {
            int index = i;
            Transform tr = childArray[index];
            if (tr.parent == transform)
            {
                tempTrList.Add(tr);
            }
        }
        childArray = tempTrList.ToArray();

        int count = childArray.Length;
        int tempCount = 0;
        float row = 0;
        float col = 0;

        for (int i = 0; i < count; i++)
        {
            row = 0;
            for (int j = 0; j < rowCount; j++)
            {
                //�ִ�ġüũ
                if (tempCount == count)
                {
                    return;
                }
                Transform tr = childArray[tempCount];

                switch (batchType)
                {
                    case BatchType.XY:
                        tr.SetLocalPositionAndRotation(new Vector3(row, col), Quaternion.identity);
                        break;
                    case BatchType.XZ:
                        tr.SetLocalPositionAndRotation(new Vector3(row, 0, col), Quaternion.identity);
                        break;
                }
                tempCount++;

                row += distance;
            }
            col += distance;
        }
    }
}
[CustomEditor(typeof(AssetGridSetter))]
public class AssetGridSetterInspectorEditor : Editor
{
    private AssetGridSetter targetAssetGridSetter;
    private void OnEnable()
    {
        targetAssetGridSetter = target as AssetGridSetter;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("����"))
        {
            targetAssetGridSetter.BatchObject();
        }
    }
}
#endif