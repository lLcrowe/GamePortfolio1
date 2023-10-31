using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace lLCroweTool.Session
{
    public class SessionStarterAndEnder : MonoBehaviour
    {
        // Awake�� �����ҽ� ����ϴ°͵� �ϳ��� ���
        //���� �������� ���� ���� ����ϴ� Ŭ������ �� ������ �����ϴ°� �¾ƺ���

        //���Ǹ��� Ư������� �����ϴ� Ŭ������ �������ҵ�
        //�Ŵ���ó�� �̱��������� �ƴ�
        //�̰� �������//Start ������

        //���ǿ���
        //������ ���������
        //������ ������ ���� ����
        //�ı��Ǿ��� ��ü��

        [Header("���ǽ�Ÿ�� ����")]
        public UnityEvent startActionEvent = new UnityEvent();//���� ���������� �۵��� �̺�Ʈ��
        public float starterTimer = 0.2f;

        [Space]
        [Header("���ǿ��� ����")]
        public UnityEvent endActionEvent = new UnityEvent();


        private void Start()
        {
            SessionStarterAction();
            SceneManager.sceneUnloaded += SessionEnderAction;
        }

        [ButtonMethod]
        /// <summary>
        /// ���ǽ�Ÿ�� �̺�Ʈ�۵�
        /// </summary>
        public void SessionStarterAction()
        {
            //
            if (!SessionManager.CheckExistScene())
            {
                return;
            }
            StartCoroutine(SessionStarterCoroutine());
        }

        //���ǽ�Ÿ�� ����
        private IEnumerator SessionStarterCoroutine()
        {
            yield return new WaitForSeconds(starterTimer);
            startActionEvent.Invoke();
        }

        /// <summary>
        /// ���ǿ��� �̺�Ʈ�۵�
        /// </summary>
        /// <param name="scene">��</param>
        private void SessionEnderAction(Scene scene)
        {
            if (!SessionManager.CheckExistScene())
            {
                return;
            }
            endActionEvent.Invoke();
            SceneManager.sceneUnloaded -= SessionEnderAction;//������ �ִ� �׼��� ������
        }
    }
}
