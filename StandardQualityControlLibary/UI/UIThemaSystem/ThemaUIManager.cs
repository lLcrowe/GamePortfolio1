using lLCroweTool.DataBase;
using lLCroweTool.Dictionary;
using lLCroweTool.LogSystem;
using lLCroweTool.Singleton;
using System.Collections.Generic;
using UnityEngine;

namespace lLCroweTool.UI.UIThema
{
    public class ThemaUIManager : MonoBehaviourSingleton<ThemaUIManager>
    {
        //����Ÿ���õ� �׸�����
        [SerializeField] 
        private UIThemaInfo currentUIThemaInfo;

        //����Ÿ���õ� ����������
        [SerializeField]
        private IconPresetInfo currentTargetIconPresetInfo;

        [System.Serializable]
        public class UIThemaBible : CustomDictionary<string, UIThemaInfo> { }
        public UIThemaBible uIThemaInfoBible = new UIThemaBible();

        //�׸�Ű//������Ű,������
        [System.Serializable]
        public class IconBible : CustomDictionary<string, Dictionary<string, Sprite>> { }
        public IconBible iconBible = new IconBible();

        public static string logKey = "UIThemaKey";

        protected override void Awake()
        {
            base.Awake();
            //���� ����Ʈ
            InitThemaUIManager(DataBaseManager.Instance.dataBaseInfo);
        }

        /// <summary>
        /// UI�׸��Ŵ��� �ʱ�ȭ
        /// </summary>
        /// <param name="dataBaseInfo">�����ͺ��̽� ����</param>
        public void InitThemaUIManager(DataBaseInfo dataBaseInfo)
        {
            //�׸��� ����� ù���� �׸��� �⺻�׸��� ó��
            //�״��� �����ܹ��̺��� �ʱ�ȭó��

            //UI�׸����
            uIThemaInfoBible.Clear();
            foreach (var item in dataBaseInfo.uIThemaInfoList)
            {
                uIThemaInfoBible.TryAdd(item.labelID, item);
            }

            //�����������µ��
            iconBible.Clear();
            foreach (var item in dataBaseInfo.iconPresetInfoList)
            {
                var dataList = item.iconDataList;
                var bible = new Dictionary<string, Sprite>();

                foreach (var data in dataList)
                {
                    bible.TryAdd(data.iconName,data.iconSprite);
                }

                iconBible.TryAdd(item.labelID, bible);
            }
        }

        /// <summary>
        /// ��� UI�׸��� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="uIThemaInfo"></param>
        public void InitAllThemaUI(UIThemaInfo uIThemaInfo = null)
        {
            if (uIThemaInfo == null)
            {
                uIThemaInfo = currentUIThemaInfo;
            }

            var array = FindObjectsOfType<ThemaUI>();


            for (int i = 0; i < array.Length; i++)
            {
                if (!LogManager.CheckRegister(logKey))
                {
                    LogManager.Register(logKey, "UIThema",true, true);
                }
                LogManager.Log(logKey, $"{i} {array[i].name}");
                array[i].InitThemaUI(uIThemaInfo);
            }
        }

        /// <summary>
        /// ��� �������׸��� �ʱ�ȭ �ϴ� �Լ�
        /// </summary>
        /// <param name="iconPresetInfo">����������������</param>
        public void InitAllThemaIcon(IconPresetInfo iconPresetInfo)
        {
            if (iconPresetInfo == null)
            {
                iconPresetInfo = currentTargetIconPresetInfo;
            }

            var array = FindObjectsOfType<ThemaIcon>();


            for (int i = 0; i < array.Length; i++)
            {
                if (!LogManager.CheckRegister(logKey))
                {
                    LogManager.Register(logKey, "ThemaIcon", true, true);
                }
                LogManager.Log(logKey, $"{i} {array[i].name}");

                var temp = array[i];
                string id =  array[i].IconID;
                var sprite = RequestIcon(id);
                temp.SetImage(sprite);
            }
        }

        /// <summary>
        /// UI�׸��� �´� �������� ��û�ϴ� �Լ�
        /// </summary>
        /// <param name="iconID">������ID</param>
        /// <returns>������ ��������Ʈ</returns>
        public Sprite RequestIcon(string iconID)
        {
            //UI�׸��� ���̵� ���ӵ�����
            iconBible.TryGetValue(currentTargetIconPresetInfo.labelID, out var data);
            data.TryGetValue(iconID, out var sprite);
            return sprite;
        }

        /// <summary>
        /// Ÿ������ �׸�UI�� �ʱ�ȭ�ϴ� �Լ�
        /// </summary>
        /// <param name="themaUI">UI�׸�</param>
        public void InitTargetThemaUI(ThemaUI themaUI)
        {
            themaUI.InitThemaUI(currentUIThemaInfo);
        }
    }
}


