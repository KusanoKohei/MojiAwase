using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MojiButton : MonoBehaviour
{
    public int id;
    public int rank;
    public string spritePath;
    public string dialogText;

    public void OnClick()
    {
        Debug.Log("クリックされました");
        DialogManager.instance.HyoujiText(dialogText);
    }
}
