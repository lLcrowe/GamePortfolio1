using Doozy.Engine.UI;
using lLCroweTool.Cinemachine;
using lLCroweTool.UI.Confirm;
using lLCroweTool.UI.Option;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace lLCroweTool.UI.MainMenu
{
    public class Mainmenu : MonoBehaviour
    {
        //����ȭ�鿡�� �߰��߰��κ��� �������ִ� ����� ��
        //�ó׸ӽ� ID��
        //In => ���� ���ڴ�
        //Out => ��𿡼� ���������ڴ�

        //�ó׸ӽ��۵��Ǵ� ��ư��
        public Button searchBtn;
        public Button basementBtn;
        public Button supplyBtn;
        public Button shopBtn;

        //���� ���޼Ҹ��Դٰ����ϴ� 
        public Button shopToSupplyBtn;
        public Button supplyToShopBtn;

        //UI��� + �Ϲݹ�ư��

        //���θ޴�
        public UIView mainMenuUIView;        
        
        //���ʱ���
        public BasementUI basementUI;

        //Ž��
        public SearchUI searchUI;

        //��
        public ShopUI shopUI;        
        public PlayerSupplyDataUI playerSupplyDataUI;//��ȭó��

        //���ö���
        public SupplyUI supplyUI;

        //����
        public Button showMyPartnersBtn;
        public MyPartnersUI myPartnersUI;

        //����
        public Button achievementBtn;
        public AchievementUI achievementUI;

        //���μ��� ����
        public Button changeMainScreenSoliderBtn;
        public ChangeMainScreentSoliderUI changeMainScreenSoliderUI;

        //�ɼ�
        public Button showOptionBtn;
        public OptionSettingMenu OptionUI;


        [Header("�ó׸ӽŰ���")]
        public MenuPlaceType menuPlaceType;
        public CustomCinemachineManager cinemachineManager;//�ó׸ӽ� �Ŵ���

        private CustomCinemachine targetCinemachine;
        public CustomCinemachine plane1Cinemachine;
        public CustomCinemachine plane2Cinemachine;

        //����ó��
        public bool showAllView = false;        

        /// <summary>
        /// �ó׸ӽ��۵��� ���ѱ���. ��ġ�� ����
        /// </summary>
        public enum MenuPlaceType
        {
            MainMenu,
            SearchMap,
            Basement,
            Supply,
            Shop,

            SuppyToShop,//���޼ҿ��� ��������
            ShopToSuppy,//�������� ���޼ҷ�
        }

        protected void Awake()
        {  
            //��ư�� ����
            //UI����
            basementUI.showAllViewBtn.onClick.AddListener(() => ShowBasementUI(false, true));
            showMyPartnersBtn.onClick.AddListener(() =>
            {
                myPartnersUI.ShowUI(true);
                ShowMainMenuUI(false);
            });

            achievementBtn.onClick.AddListener(() => achievementUI.ShowUI(true));
            changeMainScreenSoliderBtn.onClick.AddListener(() => changeMainScreenSoliderUI.ShowUI(true));
            showOptionBtn.onClick.AddListener(() => OptionUI.ShowUI(true));

            shopUI.SetPlayerSupplyDataUI(playerSupplyDataUI);


            //�ó׸ӽŰ���            
            searchBtn.onClick.AddListener(() => GoTargetPlace(MenuPlaceType.SearchMap));
            basementBtn.onClick.AddListener(() => GoTargetPlace(MenuPlaceType.Basement));
            supplyBtn.onClick.AddListener(() => GoTargetPlace(MenuPlaceType.Supply));
            shopBtn.onClick.AddListener(() => GoTargetPlace(MenuPlaceType.Shop));

            shopToSupplyBtn.onClick.AddListener(() => GoTargetPlace(MenuPlaceType.ShopToSuppy));
            supplyToShopBtn.onClick.AddListener(() => GoTargetPlace(MenuPlaceType.SuppyToShop));

            //���ư��
            basementUI.AddOverrideBackAction(() => GoTargetPlace(MenuPlaceType.MainMenu));
            searchUI.AddOverrideBackAction(() => GoTargetPlace(MenuPlaceType.MainMenu));
            shopUI.AddOverrideBackAction(() => GoTargetPlace(MenuPlaceType.MainMenu));
            supplyUI.AddOverrideBackAction(() => GoTargetPlace(MenuPlaceType.MainMenu));
            myPartnersUI.AddOverrideBackAction(() => 
            {
                myPartnersUI.ShowUI(false);
                ShowMainMenuUI(true);
            });
            //achievementUI.AddOverrideBackAction(() => achievementUI.ShowUI(false));
            //changeMainScreenSoliderUI.AddOverrideBackAction(() => changeMainScreenSoliderUI.ShowUI(false));


            //������ �ó׸ӽŵ鿡 �̺�Ʈ�߰�//�����߰��� ����
            SetActionCinemachine("InSearchMap", () => ShowMainMenuUI(false), () => ShowSearchUI(true));
            SetActionCinemachine("OutSearchMap", () => ShowSearchUI(false), () => ShowMainMenuUI(true));

            SetActionCinemachine("InBasement", () => ShowMainMenuUI(false), () => ShowBasementUI(true, false));
            SetActionCinemachine("OutBasement", () => ShowBasementUI(false, false), () => ShowMainMenuUI(true));

            SetActionCinemachine("InSupply", () => ShowMainMenuUI(false), () => ShowSupplyUI(true));
            SetActionCinemachine("OutSupply", () => ShowSupplyUI(false), () => ShowMainMenuUI(true));

            SetActionCinemachine("InShop", () => ShowMainMenuUI(false), () => ShowShopUI(true));
            SetActionCinemachine("OutShop", () => ShowShopUI(false), () => ShowMainMenuUI(true));

            SetActionCinemachine("SuppyToShop", () => ShowSupplyUI(false), () => ShowShopUI(true));
            SetActionCinemachine("ShopToSuppy", () => ShowShopUI(false), () => ShowSupplyUI(true));
        }
        
        private IEnumerator Start()
        {
            // �ʱ�ī�޶��ġ//ó������ ��ŵ ���ϰ� ó��
            if (cinemachineManager.RequestCustomCinemachine("OutBasement", out CustomCinemachine outCinemachine))
            {
                //�����ϸ� �����ͼ� �۵�
                ActionCinemachineCamera(outCinemachine);
                //ActionCinemachineCamera(refCinemachine);//��ŵ
            }
            playerSupplyDataUI.SetActive(false);

            //����� �ó׸ӽ��۵�
            yield return new WaitForSeconds(1);
            plane1Cinemachine.ActionCamera();
            yield return new WaitForSeconds(2);
            plane2Cinemachine.ActionCamera();
        }

        private void SetActionCinemachine(string id, UnityAction startAction, UnityAction endAction)
        {
            if (cinemachineManager.RequestCustomCinemachine(id, out CustomCinemachine refCinemachine))
            {
                //�����ϸ� �����ͼ� �̺�Ʈ����ֱ�
                refCinemachine.startEvent.AddListener(startAction);
                refCinemachine.endEvent.AddListener(endAction);
            }
        }

        private void Update()
        {
            if (showAllView)
            {
                if (Input.anyKey)
                {
                    ShowBasementUI(true, true);
                }
            }
            if (targetCinemachine != null)
            {
                if (targetCinemachine.IsRun())
                {
                    //Debug.Log("�ó׸ӽŷ�");
                    if (Input.anyKey)
                    {
                        ActionCinemachineCamera(targetCinemachine);
                    }
                }
            }
        }


        /// <summary>
        /// �ش�Ǵ� �������� �̵��ϴ� �Լ�
        /// </summary>
        /// <param name="targetMenuPlace">Ÿ�ٸ޴� ��ġ</param>
        public void GoTargetPlace(MenuPlaceType targetMenuPlace)
        {
            //������ �ƹ�ó������
            if (menuPlaceType == targetMenuPlace)
            {
                return;
            }

            //�� ���̵�ã��
            string id = null;
            switch (targetMenuPlace)
            {
                case MenuPlaceType.MainMenu:
                    //���ƿö��� ��F�� �Ҳ� �޾Ƽ�ó��
                    id = GetReturnKey(menuPlaceType);
                    break;
                case MenuPlaceType.SearchMap:
                    //��Ž��
                    //������ Ž��
                    id = "InSearchMap";
                    break;
                case MenuPlaceType.Basement:
                    //�������±���
                    //������ ���ƽý�
                    id = "InBasement";
                    break;
                case MenuPlaceType.Supply:
                    //���޼�
                    //������ ���ۼ�
                    id = "InSupply";
                    break;
                case MenuPlaceType.Shop:
                    //����
                    //������ ����UI��� ��ġ��
                    id = "InShop";
                    break;
                case MenuPlaceType.SuppyToShop:
                    //���޼ҿ��� ��������
                    id = "SuppyToShop";
                    targetMenuPlace = MenuPlaceType.Shop;
                    break;
                case MenuPlaceType.ShopToSuppy:
                    //�������� ���޼�����
                    id = "ShopToSuppy";
                    targetMenuPlace = MenuPlaceType.Supply;
                    break;

            }
            menuPlaceType = targetMenuPlace;

            //�ó׸ӽ��۵�
            if (cinemachineManager.RequestCustomCinemachine(id, out targetCinemachine))
            {              
                //�����ϸ� �����ͼ� �۵�
                ActionCinemachineCamera(targetCinemachine);
            }
        }

        /// <summary>
        /// ���θ޴��� ���ư��� Ű���� �������� �Լ�
        /// </summary>
        /// <param name="targetMenuPlaceType"></param>
        /// <returns></returns>
        private string GetReturnKey(MenuPlaceType targetMenuPlaceType)
        {
            switch (targetMenuPlaceType)
            {
                case MenuPlaceType.SearchMap:
                    return "OutSearchMap";                    
                case MenuPlaceType.Basement:
                    return "OutBasement";                    
                case MenuPlaceType.Supply:
                    return "OutSupply";                    
                case MenuPlaceType.Shop:
                    return "OutShop";                    
                case MenuPlaceType.MainMenu:
                    return "";//�����Ѱ�                    
            }
            return "";
        }

        /// <summary>
        /// �ó׸ӽ�
        /// </summary>
        /// <param name="cinemachine"></param>
        private void ActionCinemachineCamera(CustomCinemachine cinemachine)
        {
            if (cinemachine.IsRun())
            {
                //�۵����̸� ��ŵ
                cinemachine.Skip();
            }
            else
            {
                //�ƴϸ� �����
                cinemachine.ActionCamera();
            }
        }

        //UI����======================================================================

        private void ShowMainMenuUI(bool isActive)
        {
            if (isActive)
            {
                mainMenuUIView.Show();
                playerSupplyDataUI.UpdateUI();
            }
            else
            {
                mainMenuUIView.Hide();
            }
            playerSupplyDataUI.SetActive(isActive);
        }

        private void ShowSearchUI(bool isActive)
        {
            searchUI.ShowUI(isActive);
        }

        private void ShowBasementUI(bool isActive, bool isChangeState)
        {
            basementUI.ShowUI(isActive);
            if (isChangeState)
            {
                showAllView = !isActive;
            }
        }

        private void ShowSupplyUI(bool isActive)
        {
            supplyUI.ShowUI(isActive);
            playerSupplyDataUI.SetActive(isActive);
        }

        private void ShowShopUI(bool isActive)
        {
            shopUI.ShowUI(isActive);
            playerSupplyDataUI.SetActive(isActive);
        }

        public void ShowMyPartnersUI(bool isActive)
        {
            //�Ʊ��� ����


        }

        public void ShowAchievementUI(bool isActive)
        {
            //����
            //�����ý���
            //����Ʈ�ý���
            //���� �� ����ؾߵǴ°���
        }

        public void ShowChangeMainScreenSoliderUI(bool isActive)
        {
            //����ȭ�� ĳ���͹�� ���ú����ϱ� ���� ���
        }

        public void ShowOptionUI(bool isActive)
        {

        }


        private void SignalToGameQuit()
        {
            //GlobalConfirmWindow.Instance.SetConfirmWindow(LocalizingManager.Instance.GetLocalLizeText("Game Exit?"), GameExit);            
            GlobalConfirmWindow.Instance.SetConfirmWindow("Game Exit?", GameExit);
        }

        //��������
        private void GameExit()
        {
            Debug.Log("Bye~");
            Application.Quit();
        }
    }
}