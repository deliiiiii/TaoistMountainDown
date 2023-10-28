using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    [Header("ÔªËØÎüÊÕÌõ")]
    public List<Image> image_baseElem;
    public Image image_advancedElem;

    public Text selected_elem;

    private void Awake()
    {
        instance = this;
    }
    public void RefreshElementUI()
    {
        ClearElementUI();
        for (int i=0;i<PlayerManager.instance.currentPlayer.list_absorb_elem.Count;i++)
        {
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
    }
    public void RefreshAdvancedElementUI()
    {
        List<Element> list_elem = PlayerManager.instance.currentPlayer.list_absorb_elem;
        image_advancedElem.gameObject.SetActive(true);
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

    }

    public void RefreshSelectedElementUI()
    {
        //TODO 
    }
    public bool IsAdvancedElem(Element.TYPE type)
    {
        if (type == Element.TYPE.Fire || type == Element.TYPE.Water || type == Element.TYPE.Mud)
            return false;
        return true;
    }
}
