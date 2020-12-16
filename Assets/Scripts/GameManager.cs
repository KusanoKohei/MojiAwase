using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    public List<string> mojiList;
    public List<GameObject> mojiImgList;

    public Button mojiPlate;
    public Text mojiText;

    public static GameManager instance;

    #region Singleton
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    
    // Start is called before the first frame update
    void Start()
    {
        Sprite spritePath = Resources.Load("Sprites/" + "animal_alpaca_huacaya") as Sprite;
        // mojiImgList[0].GetComponent<Image>().sprite = spritePath);
        OnClick();
    }

    
    private void SetMojiPlate()
    {
        int number = Random.Range(0, mojiList.Count);
        number = 0; // デバッグ用.
        mojiText.text = mojiList[number].ToString();
        
    }

    public void OnClick()
    {
        SetMojiPlate();
        SetMojiBan.instance.DividRank(DataLoader.instance.LoadForSetMojiban(mojiText));
    }
}
