using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    

    #region Singleton

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    GameManager gameManager => GameManager.instance;

    private string[] textStrings = { "あ", "い", "う", "え", "お", "か", "き", "く","け" };

    public MojiPlateParameter LoadForSetMojiban(Text mojiText)
    {
        string assetDataPath = "ExcelData/" + mojiText.text;
        MojiPlateParameter mojiParam = Resources.Load(assetDataPath) as MojiPlateParameter;

        return mojiParam;
    }
}
