using lLCroweTool.Dictionary;
using lLCroweTool.ScoreSystem;
using lLCroweTool.ScoreSystem.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace lLCroweTool.ScoreSystem.UI
{
    public class MVPScoreBoard : MonoBehaviour
    {
        //���Ӹ��� �ִ� MVP ���� �ý����� ����

        //1. ���� �⿩�� ������ ���� �ջ�
        //2. �� ������ ���� ���� ����� MVP�� ����.
        //3. �⿩�� ������ ���� ���� 3���� �����ش�

        //������ ���� ���ʽ������� ����//Ŀ�����ν����ͷ� ó��
        //�̰� CSV�� ó��//���ھ�ʽ� ����
        [System.Serializable]
        public class ScoreBonusBible : CustomDictionary<ScoreType, int> { }        
        [SerializeField]private ScoreBonusBible scoreBonusBible = new ScoreBonusBible();
        
        public ScoreBonusBible ScoreBonusInfoBible { get => scoreBonusBible; }

        //�ְ������� ������� 
        //������ �����ٰ��� üũ
        //�������//����5//������//���� Ŭ����

        //�����̸� �ٲ�� �ҵ�

        //ĳ�����̸�
        //MVPŸ��Ʋ�̸�

        //�̰͵� ���߿� ó��
        //�������� �����ߵ�
        public struct ScoreTypeBonusInfo
        {
            public Color colorMVPBackGroundColor;//MVP��Ī���� ��׶��� �÷�//�ø������¡�ؼ� ���ϱ� RGBA�� ������
            public int bunusScale;//������ ���� ���ʽ� ����
            public string scoreMVPAlias;//�� ������ MVP�Ͻ� ����
            public string scoreMVPDescription;//�� ������ MVP�Ͻ� ���� ����
        }       
        

        public TextMeshProUGUI mvpAliasText;//MVP ����
        public TextMeshProUGUI mvpAliasReasonText;//MVP ���� ���� ����
        public TextMeshProUGUI mvpUnitNameText;//MVP�����̸�

        public Button scoreShowButton;//������Ȳ�����ֱ�
        public Transform rewordSlotPos;//���󽽷� ��ġ


        //���Ľ�ų ��������Ʈ
        private List<MVPScoreData> mVPScoreInfoList = new List<MVPScoreData>();

        /// <summary>
        /// Ŀ���ҵ�ųʸ��� ���� ����ϴ� ������ �����ϴ� ����ü
        /// </summary>
        public struct MVPScoreData
        {
            //����
            public UnitObject_Base unitObject;

            //�ش�Ÿ���� ���ս��ھ�//���ϱ� ��������
            public int totalScore;

            //�ش����ֿ� ���� ������ ���ϱ� ��������//����Ÿ�ٿ� �ִ� ��������� �������
            public List<ScoreTargetData> scoreTargetDataList;
        }

        /// <summary>
        /// ����Ÿ�Գ��� �񱳸� ���� ����ü
        /// </summary>
        public struct ScoreTargetData
        {
            public ScoreType scoreType;
            public int scoreTypeForScore;
        }


        /// <summary>
        /// ���������͵��� ����ϴ� �Լ�
        /// </summary>
        /// <param name="unitObject">���ֿ�����Ʈ</param>
        /// <param name="scoreTarget">���ھ�Ÿ��</param>
        public void RegisterScoreTarget(UnitObject_Base unitObject, ScoreTarget scoreTarget)
        {
            var scoreBible = scoreTarget.ScoreBible;

            //���
            int totalScore = 0;
            MVPScoreData mVPScoreInfo = new MVPScoreData();
            mVPScoreInfo.unitObject = unitObject;
            mVPScoreInfo.scoreTargetDataList = new List<ScoreTargetData>();

            foreach (var item in scoreBible)
            {
                ScoreTargetData scoreTargetInfo = new ScoreTargetData();
                var scoreType = item.Key;
                var score = item.Value;

                //���������� �״�� ���� ����ؼ� �����ִ� �뵵
                scoreTargetInfo.scoreType = scoreType;
                scoreTargetInfo.scoreTypeForScore = score;
                mVPScoreInfo.scoreTargetDataList.Add(scoreTargetInfo);

                //���������� ���ʽ������� �־� ��ü���� ���������� �ִ� �뵵
                totalScore += CalBonusScore(scoreType, score);
            }

            //���⼭ ���ھ�Ÿ�� ������ ���Ľ�����
            mVPScoreInfo.scoreTargetDataList.Sort(new ValueSort());
            mVPScoreInfo.totalScore = totalScore;
            mVPScoreInfoList.Add(mVPScoreInfo);//���
        }

        /// <summary>
        /// MVP�� �����ִ� �Լ�
        /// </summary>
        /// <returns>MVP�� �� ���ֿ�����Ʈ</returns>
        public UnitObject_Base ShowMVP()
        {
            //������ ����
            mVPScoreInfoList.Sort(new ValueSort());

            //UI�� ������ġ
            //MVP ��ġ


            //������� ������ߵ��� üũ�غ���

            foreach (var item in mVPScoreInfoList)
            {
                var unit = item.unitObject;
                
            }



            var highScoreUnit = mVPScoreInfoList[0].unitObject;
            //mvpAliasText.text = $"MVP {}";//MVP��Ī
            //mvpAliasReasonText//���� ���� ����
            mvpUnitNameText.text = $"{highScoreUnit.unitObjectInfo.labelNameOrTitle}";//�����̸�
            

            return highScoreUnit;
        }

        /// <summary>
        /// ������ ����//��ϵ� ����Ÿ�ٵ��� ����
        /// </summary>
        public void ResetScoreBoard()
        {
            mVPScoreInfoList.Clear();
        }

        /// <summary>
        /// ���� �������� ������ ���ʽ����� ���Ͽ� �����ִ� �Լ�
        /// </summary>
        /// <param name="scoreType">����Ÿ��</param>
        /// <param name="score">����</param>
        /// <returns>���� ���ʽ�����</returns>
        private int CalBonusScore(ScoreType scoreType, int score)
        {
            scoreBonusBible.TryGetValue(scoreType, out int bonusScoreScale);
            score *= bonusScoreScale;

            //print($"{scoreType} Value:{score} = {score / bonusScoreScale}x{bonusScoreScale}");
            return score;
        }

    }

}


#if UNITY_EDITOR

namespace lLCroweTool.QC.EditorOnly
{
    [UnityEditor.CustomEditor(typeof(MVPScoreBoard))]
    public class MVPScoreBoardEditor : UnityEditor.Editor
    {
        private MVPScoreBoard targetObject;
        private ScoreType[] scoreTypeArray = lLcroweUtil.GetEnumDefineData<ScoreType>().ToArray();

        private void OnEnable()
        {
            targetObject = target as MVPScoreBoard;
            var bible = targetObject.ScoreBonusInfoBible;
            if (bible.Count == scoreTypeArray.Length)
            {
                return;
            }

            //����ȭ//������ ����ü�� ��ġ�� �����Ͽ� �ֱ�
            Dictionary<ScoreType, int> newBible = new Dictionary<ScoreType, int>(bible);
            bible.Clear();


            for (int i = 0; i < scoreTypeArray.Length; i++)
            {
                var scoreType = scoreTypeArray[i];
                if (newBible.ContainsKey(scoreType))
                {
                    bible.Add(scoreType, newBible[scoreType]);
                    continue;
                }
                //�⺻���� 1
                bible.Add(scoreType, 1);
            }
        }
    }
}

#endif


