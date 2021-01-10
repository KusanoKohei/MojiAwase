using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMojiBan : MonoBehaviour
{
    public static SetMojiBan instance;

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

    // private MojiPlateParameter mojiParam;


    [SerializeField]
    private List<SelectedMojiPlate> rank0List;
    [SerializeField]
    private List<SelectedMojiPlate> rank1List;
    [SerializeField]
    private List<SelectedMojiPlate> rank2List;

    public char myChar;
    public char myThemeMoji;
    public char myKatakana;

    private MojiDataGeneral mojiParam;

    [SerializeField]
    public int rank0_kazu;
    [SerializeField]
    public int rank1_kazu;
    [SerializeField]
    public int rank2_kazu;

    [SerializeField]
    // List<SelectedMojiPlate> selectedRank0 = new List<SelectedMojiPlate>();
    List<SelectedMojiPlate> selectedRank0 = new List<SelectedMojiPlate>();
    [SerializeField]
    // List<SelectedMojiPlate> selectedRank1 = new List<SelectedMojiPlate>();
    List<SelectedMojiPlate> selectedRank1 = new List<SelectedMojiPlate>();
    [SerializeField]
    // List<SelectedMojiPlate> selectedRank2 = new List<SelectedMojiPlate>();
    List<SelectedMojiPlate> selectedRank2 = new List<SelectedMojiPlate>();

    [SerializeField]
    List<string> selectedSprite = new List<string>();
    [SerializeField]
    List<SelectedMojiPlate> summarySelected;
    
    public List<int> integralNum = new List<int>();
    public List<int> randomNum = new List<int>();

    public List<string> texts = new List<string>();

    GameManager gameManager => GameManager.instance;

    public void SortFromMojiData(string themeMoji)
    {
        mojiParam = gameManager.MojiDataLoad();
        // themeMoji を参照し、Rank_0_Text_0とRank_0_Text_1から、頭文字がthemeMojiと一致するものを定数文拾う.
        string kataMoji = GameManager.KatakanaConvert(themeMoji);

        foreach (var list in mojiParam.sheets[0].list)
        {
            // Debug.Log("リソースから参照されたリストに入る名前は、" + list.Rank_0_Text_0.ToString());

            // themeMojiのひらがなとカタカナの二つを引数に、listを検索.

            myThemeMoji = themeMoji[0];
            myKatakana = kataMoji[0];


            // まずはリスト全体から、テキストの先頭の文字を参照し、
            // お題の文字に対してランクごとに選別し、IDを登録する。
            if (list.Rank_0_Text_0 != "")
            {
                myChar = list.Rank_0_Text_0[0];

                if (myThemeMoji == myChar || myKatakana == myChar)
                {
                    rank0List.Add(new SelectedMojiPlate()
                    {
                        id = list.ID,
                        rank = 0,
                        sprite = list.Sprite,
                        text = list.Rank_0_Text_0,
                    });
                }
            }

            if (list.Rank_0_Text_1 != "")
            {
                myChar = list.Rank_0_Text_1[0];

                if (myThemeMoji == myChar || myKatakana == myChar)
                {
                    rank0List.Add(new SelectedMojiPlate()
                    {
                        id = list.ID,
                        rank = 0,
                        sprite = list.Sprite,
                        text = list.Rank_0_Text_1,
                    });
                }
            }

            if (list.Rank_1_Text_0 != "")
            {
                myChar = list.Rank_1_Text_0[0];

                if (myThemeMoji == myChar || myKatakana == myChar)
                {
                    rank1List.Add(new SelectedMojiPlate()
                    {
                        id = list.ID,
                        rank = 1,
                        sprite = list.Sprite,
                        text = list.Rank_1_Text_0,
                    });
                }
            }

            if (list.Rank_1_Text_1 != "")
            {
                myChar = list.Rank_1_Text_1[0];

                if (myThemeMoji == myChar || myKatakana == myChar)
                {
                    rank1List.Add(new SelectedMojiPlate()
                    {
                        id = list.ID,
                        rank = 1,
                        sprite = list.Sprite,
                        text = list.Rank_1_Text_1,
                    });
                }
            }

            if (list.Rank_1_Text_2 != "")
            {
                myChar = list.Rank_1_Text_2[0];

                if (myThemeMoji == myChar || myKatakana == myChar)
                {
                    rank1List.Add(new SelectedMojiPlate()
                    {
                        id = list.ID,
                        rank = 1,
                        sprite = list.Sprite,
                        text = list.Rank_1_Text_2,
                    });
                }
            }

            if (list.Rank_1_Text_3 != "")
            {
                myChar = list.Rank_1_Text_3[0];

                if (myThemeMoji == myChar || myKatakana == myChar)
                {
                    rank1List.Add(new SelectedMojiPlate()
                    {
                        id = list.ID,
                        rank = 1,
                        sprite = list.Sprite,
                        text = list.Rank_1_Text_3,
                    });
                }
            }

            if (list.Rank_2_Text != "")
            {
                myChar = list.Rank_2_Text[0];

                if (myThemeMoji != myChar || myKatakana != myChar)
                {
                    rank2List.Add(new SelectedMojiPlate()
                    {
                        id = list.ID,
                        rank = 2,
                        sprite = list.Sprite,
                        text = list.Rank_2_Text,
                    });
                }
            }
        }
        
        ChusenRank0();
        ChusenRank1();
        ChusenRank2();

        SortMojiBan();
    }

    private void ChusenRank0()
    {
        for (int i = 0; i < rank0_kazu; i++)
        {
            int r = Random.Range(0, rank0List.Count);
            
            selectedRank0.Add(rank0List[r]);
        }
    }

    private void ChusenRank1()
    {
        for (int i = 0; i < rank1_kazu; i++)
        {
            int r = Random.Range(0, rank1List.Count);
            selectedRank1.Add(rank1List[r]);
            
            i += CheckDaburi(i, selectedRank1[i].id, selectedRank1);
        }
    }

    private void ChusenRank2()
    {
        for (int i = 0; i < rank2_kazu; i++)
        {
            int r = Random.Range(0, rank2List.Count);
            
            selectedRank2.Add(rank2List[r]);
            
            i += CheckDaburi(i, selectedRank2[i].id, selectedRank2);
            // numOfRank2.RemoveAt(r); // 選択された要素がだぶらないように選択された要素を省く.
        }
    }


    private int CheckDaburi(int num, int id, List<SelectedMojiPlate> selectedRank)
    {
        if (num >= 1)
        {
            for (int i = 0; i < selectedRank.Count - 1; i++)
            {
                if (id == selectedRank[i].id)
                {
                    Debug.Log("重複しました");
                    selectedRank.RemoveAt(num);
                    return -1;
                }
            }
        }
        return 0;
    }


    private void SortMojiBan()
    {
        // randomNumの素を作る.
        for(int i=0; i<GameManager.instance.mojiImgList.Count; i++)
        {
            integralNum.Add(i);
        }
        
        // randomNumの要素をバラバラにする.
        for(int i=0; i<GameManager.instance.mojiImgList.Count; i++)
        {
            int num = Random.Range(0, integralNum.Count);

            randomNum.Add(integralNum[num]);
            
            integralNum.RemoveAt(num);
        }

        summarySelected = new List<SelectedMojiPlate>(gameManager.mojiImgList.Count);
        
        foreach(var sec in selectedRank0)
        {
            summarySelected.Add(sec);
        }

        foreach(var sec in selectedRank1)
        {
            summarySelected.Add(sec);
        }

        foreach(var sec in selectedRank2)
        {
            summarySelected.Add(sec);
        }

        
        for(int i=0; i<summarySelected.Count; i++)
        {
            //Debug.Log(randomNum[i]);
            summarySelected[i].junban = randomNum[i];
        }


        // 文字盤にselectedNumを埋め込んでいく.
        for(int i=0; i<gameManager.mojiImgList.Count; i++)
        {
            for(int j=0; j<summarySelected.Count; j++)
            {
                if (i == summarySelected[j].junban)
                {
                    MojiSlot mojiSlot = gameManager.mojiImgList[i].GetComponent<MojiSlot>();

                    mojiSlot.id = summarySelected[j].id;

                    mojiSlot.rank = summarySelected[j].rank;

                    // Debug.Log("ロードしたスプライトのパスは、" + Resources.Load("Sprites/" + summarySelected[j].sprite).name);
                    gameManager.mojiImgList[i].GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/" + summarySelected[j].sprite);
                    
                    Text mojiText = gameManager.mojiImgList[i].GetComponentInChildren<Text>();
                    mojiText.text = summarySelected[j].text;
                    mojiSlot.dialogText = mojiText.text;
                    // Debug.Log(j + "番目のボタンのテキストは" + mojiButton.dialogText + "であり、しかし表示されているテキストは" + mojiText.text);
                
                }
            }
        }
    }
   

    private void ResetMojiBan()
    {
        selectedRank0.Clear();
        selectedRank1.Clear();
        selectedRank2.Clear();

        summarySelected.Clear();

        integralNum.Clear();
        randomNum.Clear();
    }
}
