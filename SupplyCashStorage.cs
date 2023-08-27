using lLCroweTool.Achievement;
using lLCroweTool.Effect.VFX;
using lLCroweTool.TimerSystem;
using TMPro;
using UnityEngine;
using System;
using static lLCroweTool.QC.Security.Security;

namespace lLCroweTool
{   
    public class SupplyCashStorage : MonoBehaviour
    {
        //�ð��� ���� �ڿ��� �����ϰ� �����ϴ� Ŭ����

        //�������� �ڿ�
        public int cashMoney;
        public int cashSupply;

        //�ִ�ġ
        public int maxMoney;
        public int maxSupply;

        //�ð�(��) ������ ���� ��·�
        public int addSecondTimeToMoney = 10;
        public int addSecondTimeToSupply = 10;

        public TextMeshProUGUI moneyTextObject;
        public TextMeshProUGUI supplyTextObject;

        //��ϼҾƾƵ�
        [RecordIDTagAttribute] public string moneyCollectID;
        
        //���������Ʈ
        public ResourceSpreadVFX[] resourceSpreadVFXArray = new ResourceSpreadVFX[0];

        //�ڿ� UI�� ��ġ�̵���
        public PlayerSupplyDataUI playerDataUI;
        public Transform originParent;

        //�ð�ó��
        private TimerModule_Element updateLimtTimerModule;//1��        

        //���α׷��� ���������� �ð�
        private static DateTime startDataTime;
        private static float startDataTimeF;

        

        //ó������������ �ð����� üũ
        //�ý������� �ð��� ���� üũ

        //Ŭ��κ��� Ŭ�󿡼� ó�����ֱ�
        //���ͳݿ��� ó���ϴ°� �ƴ�

        //Ȱ��Ǵ½ð���
        //�������� �Ǵ��ϴ� Ŭ���ʿ��� �Ǵ��� ������ ����
        //���̷��� �ߴ��� ���� ����


        private string key = "Harvest";
        private float limitButtonTime;

        [ButtonMethod]
        public void ResetDataBtn()
        {
            PlayerPrefs.SetString(key, "0");
        }



        private void Awake()
        {
            //�ð�����
            updateLimtTimerModule = new TimerModule_Element(1, true);            
            startDataTime = DateTime.Now;
            startDataTimeF = (float)GetSecond(startDataTime.Ticks);           
        }

        private bool CheckCurTime()
        {
            //�ð�����
            float time = Time.time;//���α׷��� �����ð��� ������
            time = startDataTimeF + time;//������ ������������ �ʸ� ������ + �����ð�
            //�������ð��� �����
                        
            DateTime curDateTime = DateTime.Now;//����ð�
            //DateTime prevDateTime = GetPrevDateTime();//���ð��� �����ͼ�

            //TimeSpan curSpan = curDateTime - prevDateTime;            
            //float spanSecond = (float)GetSecond(curSpan.Ticks);

            float spanSecond = (float)GetSecond(curDateTime.Ticks);

            //���̰��� üũ//0�� �����°� �����ϰ�
            float result = time - spanSecond;

            //���ǵ����ϸ� �������


            //�̻��ϸ� �α�
            if (result > 1 || result < -1)
            {
                print("����ð��� �̻��մϴ�");
            }

            return false;
        }
     
        private void OnEnable()
        {
            //������ �ð��� �����ͼ� ���

            //�ð�����//����, ����
            DateTime curDateTime = GetCurDateTime();
            DateTime prevDateTime = GetPrevDateTime();
            updateLimtTimerModule.ResetTime();

            //�ð��� ���� �����ʱ�ȭ
            TimeSpan span = curDateTime - prevDateTime;
            double second = GetSecond(span.Ticks);

            cashMoney += (int)(addSecondTimeToMoney * second);
            cashSupply += (int)(addSecondTimeToSupply * second);
            LimitCheckCashSupply();

            UpdateText();            
        }

        private void OnDisable()
        {
            //�ð�����
            SetPrevDataTime(GetCurDateTime());
        }

        /// <summary>
        /// ���� ������Ÿ���� �������� �Լ�
        /// </summary>
        /// <returns>������Ÿ��</returns>
        public DateTime GetPrevDateTime()
        {   
            string temp = PlayerPrefs.GetString(key);
            temp = DESEncryption.EncryptString(temp, key, ConvertEncryptType.Decrypt);
            double value = 0;
            double.TryParse(temp, out value);
            long tempTick = (long)(value * TimeSpan.TicksPerSecond);
            return new DateTime(tempTick);
        }

        /// <summary>
        /// ���� �����ͽð� �����Լ�
        /// </summary>
        /// <param name="dataTime">������Ÿ��</param>
        public void SetPrevDataTime(DateTime dataTime)
        {
            var seconds = GetSecond(dataTime.Ticks);
            string temp = DESEncryption.EncryptString(seconds.ToString(), key, ConvertEncryptType.Encrypt);
            PlayerPrefs.SetString(key, temp);
        }

        public DateTime GetCurDateTime()
        {
            DateTime dateTime = DateTime.Now;
            //�ð� ����            
            if (CheckCurTime())
            {
                dateTime = GetPrevDateTime();
            }

            return dateTime;
        }

        /// <summary>
        /// DateTime�� TimeSpan�� Ticks�� �ʴ����� ���氡������ �Լ�
        /// </summary>
        /// <param name="ticks">ƽ</param>
        /// <returns>��(�ð�)</returns>
        private double GetSecond(double ticks)
        {
            return ticks / TimeSpan.TicksPerSecond;
        }

        private void Update()
        {
            if (!updateLimtTimerModule.CheckTimer())
            {
                return;
            }

            //�߰��ع�����
            cashMoney += addSecondTimeToMoney;
            cashSupply += addSecondTimeToSupply;

            LimitCheckCashSupply();
            UpdateText();            
        }

        private void LimitCheckCashSupply()
        {
            cashMoney = Mathf.Clamp(cashMoney, 0, maxMoney);
            cashSupply = Mathf.Clamp(cashSupply, 0, maxSupply);
        }

        private void UpdateText()
        {
            moneyTextObject.text = $"{cashMoney}/{maxMoney}";
            supplyTextObject.text = $"{cashSupply}/{maxSupply}";
        }


        public void HarvestSupply()
        {
            if (cashMoney == 0 || cashSupply == 0)
            {
                return;
            }

            //��ưŬ������
            if (Time.time > limitButtonTime + 1 == false)
            {
                return;
            }

            //�����ֱ�
            DataBaseManager instacne = DataBaseManager.Instance;
            instacne.playerData.money += cashMoney;
            instacne.playerData.supply += cashSupply;

            //����ó��
            AchievementManager.Instance.UpdateRecordData(moneyCollectID, cashMoney);

            //�ʱ�ȭ
            cashMoney = 0;
            cashSupply = 0;
            limitButtonTime = Time.time;
            updateLimtTimerModule.ResetTime();

            //�ð�����
            DateTime curDateTime = GetCurDateTime();
            SetPrevDataTime(curDateTime);

            //�ڿ�UI
            UpdateText();
            FindObjectOfType<PlayerSupplyDataUI>(true)?.UpdateUI();

            //����Ʈó��
            for (int i = 0; i < resourceSpreadVFXArray.Length; i++)
            {
                resourceSpreadVFXArray[i].Action();
            }
        }
    }
}
