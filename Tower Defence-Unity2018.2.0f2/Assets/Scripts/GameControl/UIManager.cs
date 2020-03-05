using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public static GameObject informationUI;
    public static Text moneyText;
    public static List<string> information = new List<string>();
    public static Animator money;
    public static PlayerState playerState;
    public static Texture2D defaultPointer;
    public static Texture2D onEnemyPointer;
    public static Texture2D onFriendPointer;
    public static Texture2D onPointPointer;


    private void Start()
    {
        defaultPointer = (Texture2D)Resources.Load("Image/Pointer/Pointer");
        onEnemyPointer = (Texture2D)Resources.Load("Image/Pointer/OnEnemy");
        onFriendPointer = (Texture2D)Resources.Load("Image/Pointer/OnFriend");
        onPointPointer = (Texture2D)Resources.Load("Image/Pointer/OnPoint");
        Cursor.SetCursor(defaultPointer, Vector2.zero, CursorMode.Auto);
        informationUI = GameObject.Find("InformationUI");
        moneyText = GameObject.Find("Money").GetComponent<Text>();
        money = GameObject.Find("Money").GetComponent<Animator>();
        playerState = GameObject.Find("Player").GetComponent<PlayerState>();
        
    }

    public static void UpdateMoneyUI()
    {
        moneyText.text = "￥ " + playerState.money;
    }

    public static void ShowInformation()
    {
        for(int i = 0; i < informationUI.transform.childCount; i++)
        {
            if (i < information.Count)
                informationUI.transform.GetChild(i).GetComponent<Text>().text = information[i];
            else
                informationUI.transform.GetChild(i).GetComponent<Text>().text = "";
        }
        informationUI.GetComponent<Animator>().ResetTrigger("Hide");
        informationUI.GetComponent<Animator>().SetTrigger("Show");
        ResetInformation();
    }

    public static void HideInformation()
    {
        if (informationUI != null)
        {
            informationUI.GetComponent<Animator>().ResetTrigger("Show");
            informationUI.GetComponent<Animator>().SetTrigger("Hide");
        }
    }

    private static void ResetInformation()
    {
        information.RemoveRange(0, information.Count);
    }

    public static void NotEnoughMoney()
    {
        money.SetTrigger("NotEnoughMoney");
    }
}
