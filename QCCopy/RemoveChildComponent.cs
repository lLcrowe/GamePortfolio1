using System;
using UnityEngine;

namespace lLCroweTool.QC.EditorOnly
{
    public class RemoveChildComponent : MonoBehaviour
    {
        //�ŵ�ũȸ�粨 �𵨸��� �ִ� Ư�� ������Ʈ�� ������ ����°ͺ���
        //�̷��� ������ ������ ���������
        //Ư��������Ʈ�� ����� ������
        //�ش� ������Ʈ�� �߰��Ŀ� ������ �����ϱ�

        public Component targetComponent;

        [ButtonMethod]
        public void RemoveTargetComponentForAllChild()
        {
            if (targetComponent == null)
            {
                Debug.Log("Ÿ���õ� ������Ʈ�� �����ϴ�.", gameObject);
                return;
            }

            var type = targetComponent.GetType();
            RemoveComponent(type);
        }

        private void RemoveComponent(Type type)
        {
            var componentArray = gameObject.GetComponentsInChildren(type);

            print(componentArray.Length);

            for (int i = 0; i < componentArray.Length; i++)
            {
                var temp = componentArray[i];
                DestroyImmediate(temp);
            }
        }

        [ButtonMethod]
        public void DeleteThisComponent()
        {
            DestroyImmediate(this);
        }

    }

}


