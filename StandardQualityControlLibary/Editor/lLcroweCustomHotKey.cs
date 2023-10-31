using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace lLCroweTool.QC.EditorOnly
{
    public class lLcroweCustomHotKey : Editor
    {
        //Ŀ���� ����Ű
        //�޴������ۿ� �ִ� �������� �����ͼ� ������ _ ���� ���ϴ� ����Ű�� ���� ��
        //[MenuItem("GameObject/ActiveToggle _a")]
        [MenuItem("GameObject/ActiveToggle _`")]
        private static void SelectGameObjectActiveAndDeActive()
        {
            foreach (GameObject go in Selection.gameObjects)
            {
                go.SetActive(!go.activeSelf);
            }   
        }

        [MenuItem("Tools/Toggle Inspector _1")]
        private static void ToggleLockInspector()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}
