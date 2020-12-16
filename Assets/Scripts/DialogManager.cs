using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField]
    Text dialogText;

    public static DialogManager instance;

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void HyoujiText(string text)
    {
        this.dialogText.text = text;
    }
}
