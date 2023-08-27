using UnityEngine;
using UnityEditor;

namespace lLCroweTool.QC.EditorOnly
{
    //20230609//��¥���� �䱸���� üũ�� ����
    public class LockTheDragEditor : EditorWindow
    {
        public static bool checkHierarchy = false;
        public static bool checkProject = false;

        //������â ����
        [MenuItem("lLcroweTool/LockTheDragEditor")]
        public static void ShowWindow()
        {
            EditorWindow editorWindow = GetWindow(typeof(LockTheDragEditor));
            editorWindow.titleContent.text = "�� ������";
            editorWindow.minSize = new Vector2(200, 105);
            editorWindow.maxSize = new Vector2(200, 105);
        }

        private void OnGUI()
        {
            string content = checkProject ? "-=������Ʈâ ��� Ȱ��ȭ=-" : "-=������Ʈâ ��� ��Ȱ��ȭ=-";
            EditorGUILayout.LabelField(content);

            if (GUILayout.Button("��ݺ���"))
            {
                if (checkProject)
                {
                    DragAndDrop.RemoveDropHandler(ProjectHandler);
                    checkProject = false;
                }
                else
                {
                    DragAndDrop.AddDropHandler(ProjectHandler);
                    checkProject = true;
                }
            }

            content = checkHierarchy ? "-=���̾��Űâ ��� Ȱ��ȭ=-" : "-=���̾��Űâ ��� ��Ȱ��ȭ=-";
            EditorGUILayout.LabelField(content);
            if (GUILayout.Button("��ݺ���"))
            {
                if (checkHierarchy)
                {
                    DragAndDrop.RemoveDropHandler(HierarchyHandler);
                    checkHierarchy = false;
                }
                else
                {
                    DragAndDrop.AddDropHandler(HierarchyHandler);
                    checkHierarchy = true;
                }
            }

            EditorGUILayout.LabelField("Creat by lLcrowe");
            //HierarchyDropFlags.
            //DragAndDrop.AddDropHandler();
            //EditorApplication.hierarchyChanged += Func;
            //EditorApplication.projectChanged += Func;
            //DragAndDrop.AddDropHandler(SceneHandler);
            //DragAndDrop.AddDropHandler(InspectorHandler);
        }

        private static DragAndDropVisualMode GetSettingMode(bool checkValue)
        {
            //var data = DragAndDropVisualMode.None;
            var data = DragAndDropVisualMode.Generic;
            if (checkValue)
            {
                data = DragAndDropVisualMode.Rejected;
            }
            return data;
        }

        public static DragAndDropVisualMode HierarchyHandler(int dropTargetInstanceID, HierarchyDropFlags dropMode, Transform parentForDraggedObjects, bool perform)
        {
            //false//�巡��
            //true//���
            //Debug.Log(perform);

            return GetSettingMode(checkHierarchy);
        }

        public static DragAndDropVisualMode ProjectHandler(int id, string path, bool perform)
        {
            return GetSettingMode(checkProject);
        }


        //public static DragAndDropVisualMode InspectorHandler(UnityEngine.Object[] targets, bool perform)
        //{
        //    //?
        //    return GetSettingMode();
        //}


        //public static DragAndDropVisualMode SceneHandler(UnityEngine.Object dropUpon, Vector3 worldPosition, Vector2 viewportPosition, Transform parentForDraggedObjects, bool perform)
        //{
        //    //?
        //    return GetSettingMode();
        //}


        //static void Func()
        //{
        //    Debug.Log($"������Ʈ Test! {Selection.count}");
        //}
    }

    ////���µ����ͺ��̽� ó���Ǳ� ��
    //public class TestAssetModificationProcessor : AssetModificationProcessor
    //{
    //    //https://docs.unity3d.com/ScriptReference/AssetModificationProcessor.html
    //    //failed�� �������ָ� ���̾�α� â�߰� �۵�������
    //    static void OnWillCreateAsset(string assetName)
    //    {
    //        Debug.Log("OnWillCreateAsset is being called with the following asset: " + assetName + ".");
    //    }

    //    static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions opt)
    //    {
    //        Debug.Log("OnWillDeleteAsset");
    //        Debug.Log(path);
    //        return AssetDeleteResult.DidDelete;
    //    }

    //    private static AssetMoveResult OnWillMoveAsset(string sourcePath, string destinationPath)
    //    {
    //        Debug.Log("Source path: " + sourcePath + ". Destination path: " + destinationPath + ".");
    //        AssetMoveResult assetMoveResult = AssetMoveResult.DidMove;

    //        // Perform operations on the asset and set the value of 'assetMoveResult' accordingly.

    //        return assetMoveResult;
    //    }
    //}

    ////���µ����ͺ��̽� ó�� ��
    //public class TestCustomPostprocessor : AssetPostprocessor
    //{
    //    private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    //    {
    //        //importedAssets//����Ʈ��
    //        //deletedAssets//����
    //        //movedAssets//������ ���
    //        //movedFromAssetPaths//������ ����� ���� ��ġ

    //        for (int i = 0; i < movedAssets.Length; i++)
    //        {
    //            Debug.Log($"{movedFromAssetPaths[i]} => ({movedAssets[i]}) Move");
    //        }
    //    }

    //    private void OnPreprocessAsset()
    //    {
    //        //ó���Ϸ�
    //    }
    //}
}

