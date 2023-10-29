using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("玩家血量(左上)")]
    public Image image_HP;

    [Header("元素蓄能(右下)")]
    public GameObject elementMask;
    public List<Image> image_half;
    public Image image_total;
    public List<Image> image_baseElem;
    public Image image_advancedElem;
    public Text selected_elem;

    [Header("玩家防御(玩家头顶)")]
    public GameObject panel_AbsorbCD;
    public Image image_AbsorbCDTimer; 
    //public GameObject panel_AbsorbUsing;
    //public Image image_AbsorbUsingTimer;
    public AbsorbCircle absorbCircle;

    [Header("界面交互")]
    public GameObject panel_Mainmenu;
    public GameObject panel_Pause;
    public GameObject panel_Gameover;
    public GameObject panel_Win;
    public GameObject panel_SelectLevel;
    public List<Grid> grids; 
    public int index_room = 0;
    public Dictionary<Element.TYPE, int[]> dic_elemSprite;
    private void Awake()
    {
        instance = this;
        dic_elemSprite = new()
        {
            {Element.TYPE.None, new int[]{ 255, 255, 255}},
            {Element.TYPE.Fire, new int[]{255,0,54}},
            {Element.TYPE.Water, new int[]{11,178,255}},
            {Element.TYPE.Mud, new int[]{255,216,0}},

            {Element.TYPE.Rock, new int[]{184,172,147}},
            {Element.TYPE.Melt, new int[]{255,150,0}},
            {Element.TYPE.Wind, new int[]{121,249,217}},
        };
    }
    private void Update()
    {
        RefreshAbsorbUI();
        InteractUI();
    }
    void InteractUI()
    {
        if(panel_Mainmenu.activeSelf)
        {
            //按esc退出 区分编辑器和游戏
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //if (Application.isEditor)
                //{
                //    UnityEditor.EditorApplication.isPlaying = false;
                //} 
                //else
                {
                    Application.Quit();
                }
            }
            if (Input.anyKeyDown)
            {
                StartGame();
            }
        }
        
        if(panel_Pause.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                panel_Pause.SetActive(false);
                Time.timeScale = 1;
            }
        }
        else 
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                panel_Pause.SetActive(true);
                Time.timeScale = 0;
            }
        }

        
    }
    public void StartGame()
    {
        PlayerManager.instance.currentPlayer.curHP = PlayerManager.instance.currentPlayer.maxHP;
        image_HP.fillAmount = (float)PlayerManager.instance.currentPlayer.curHP / (float)PlayerManager.instance.currentPlayer.maxHP;

        panel_Mainmenu.SetActive(false);
        panel_Win.SetActive(false);
        floorManager.instance.StartFloor(index_room);
        Time.timeScale = 1;
    }

    public void ReturnToMain()
    {
        panel_Win.SetActive(false);
        panel_Gameover.SetActive(false);
        panel_Pause.SetActive(false);
        panel_Mainmenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        panel_Gameover.SetActive(false);
        panel_Pause.SetActive(false);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        panel_Gameover.SetActive(true);
        Time.timeScale = 0;
    }
    public void GameWin()
    {
        index_room++;
        panel_Win.SetActive(true);
        Time.timeScale = 0;
    }
    public void RefreshElementUI()
    {
        ClearElementUI();
        if(PlayerManager.instance.currentPlayer.list_absorb_elem.Count == 0)
        {
            image_total.gameObject.SetActive(true);
            image_total.transform.GetChild(0).gameObject.SetActive(false);
            return;
        }
        for (int i=0;i<PlayerManager.instance.currentPlayer.list_absorb_elem.Count;i++)
        {
            image_half[i].gameObject.SetActive(true);
            if (IsAdvancedElem(PlayerManager.instance.currentPlayer.list_absorb_elem[0].type))
            {
                RefreshAdvancedElementUI();
                return;
            }
            image_baseElem[i].gameObject.SetActive(true);
            List<Element> list_elem = PlayerManager.instance.currentPlayer.list_absorb_elem;
            Debug.Log("amount = " + list_elem[i].amount.Value + " max =  " + Element.dic_elem[list_elem[i].type]);
            if (list_elem[i].amount.Value < Element.dic_elem[list_elem[i].type])
                image_baseElem[i].fillAmount = (float)list_elem[i].amount.Value / (float)Element.dic_elem[list_elem[i].type] * 0.9f;
            else
                image_baseElem[i].fillAmount = 1;

            image_baseElem[i].color = new Color(dic_elemSprite[list_elem[i].type][0] / 255f, dic_elemSprite[list_elem[i].type][1] / 255f, dic_elemSprite[list_elem[i].type][2] / 255f);
            
            image_baseElem[i].transform.GetChild(2).GetComponent<Text>().text = list_elem[i].type.ToString();
            image_baseElem[i].transform.GetChild(1).GetComponent<Text>().text = list_elem[i].amount.Value.ToString();
            image_baseElem[i].transform.GetChild(0).GetComponent<Text>().text = Element.dic_elem[list_elem[i].type].ToString();
            if (list_elem[i].amount.Value == 0)
            {
                PlayerManager.instance.currentPlayer.list_absorb_elem.RemoveAt(i);
                PlayerManager.instance.currentPlayer.seleted_elem.Value = -1;
            }
        }
        
    }
    public void ClearElementUI()
    {
        for (int i = 0; i < 2; i++)
            image_baseElem[i].gameObject.SetActive(false);
        image_advancedElem.gameObject.SetActive(false);

        image_half[0].gameObject.SetActive(false);
        image_half[1].gameObject.SetActive(false);
        image_total.gameObject.SetActive(true);

    }
    public void RefreshAdvancedElementUI()
    {
        image_total.gameObject.SetActive(true);
        List<Element> list_elem = PlayerManager.instance.currentPlayer.list_absorb_elem;
        image_advancedElem.gameObject.SetActive(true);
        image_advancedElem.color = new Color(dic_elemSprite[list_elem[0].type][0] / 255f, dic_elemSprite[list_elem[0].type][1] / 255f, dic_elemSprite[list_elem[0].type][2] / 255f);
        image_advancedElem.transform.GetChild(2).GetComponent<Text>().text = list_elem[0].type.ToString();
        image_advancedElem.transform.GetChild(1).GetComponent<Text>().text = list_elem[0].amount.Value.ToString();
        image_advancedElem.transform.GetChild(0).GetComponent<Text>().text = Element.dic_elem[list_elem[0].type].ToString();
        image_advancedElem.fillAmount = (float)list_elem[0].amount.Value / (float)Element.dic_elem[list_elem[0].type];
        if(list_elem[0].amount.Value == 0)
        {
            ClearElementUI();
            PlayerManager.instance.currentPlayer.list_absorb_elem.RemoveAt(0);
            PlayerManager.instance.currentPlayer.seleted_elem.Value = -1;
        }

        elementMask.GetComponent<Image>().sprite = image_total.sprite;
    }
    public void RefreshSelectedElementUI()
    {
        //TODO -1
    }
    public bool IsAdvancedElem(Element.TYPE type)
    {
        if (type == Element.TYPE.Fire || type == Element.TYPE.Water || type == Element.TYPE.Mud)
            return false;
        return true;
    }

    public void RefreshAbsorbUI()
    {
        Player player = PlayerManager.instance.currentPlayer;
        panel_AbsorbCD.transform.position = player.transform.position + new Vector3(0, 1.5f, 0);
        if (player.bulletAbsorbTimer < player.bulletAbsorbCD)
        {
            image_AbsorbCDTimer.fillAmount = (float)player.bulletAbsorbTimer / (float)player.bulletAbsorbCD * 0.98f;
            image_AbsorbCDTimer.color = Color.black;
            //if (absorbCircle.existTimer < absorbCircle.existTime)
            //    panel_AbsorbUsing.SetActive(true);
            //else
            //    panel_AbsorbUsing.SetActive(false);
        }
        else
        {
            image_AbsorbCDTimer.fillAmount = 1 - ((float)absorbCircle.existTimer / (float)absorbCircle.existTime);
            image_AbsorbCDTimer.color = Color.red;
        }
        

    }
}
