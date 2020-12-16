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

    private MojiPlateParameter mojiParam;

    List<int> numOfRank0 = new List<int>();
    List<int> numOfRank1 = new List<int>();
    List<int> numOfRank2 = new List<int>();
    
    [SerializeField]
    public int rank0_kazu;
    [SerializeField]
    public int rank1_kazu;
    [SerializeField]
    public int rank2_kazu;

    [SerializeField]
    List<SelectedMojiPlate> selectedRank0 = new List<SelectedMojiPlate>();
    [SerializeField]
    List<SelectedMojiPlate> selectedRank1 = new List<SelectedMojiPlate>();
    [SerializeField]
    List<SelectedMojiPlate> selectedRank2 = new List<SelectedMojiPlate>();
    
    [SerializeField]
    List<SelectedMojiPlate> summarySelected;

    public List<int> integralNum = new List<int>();
    public List<int> randomNum = new List<int>();

    public List<string> texts = new List<string>();

    GameManager gameManager => GameManager.instance;

    
    public void DividRank(MojiPlateParameter mojiParam)
    {
        ResetMojiBan();

        foreach (var param in mojiParam.param)
        {
            if (param.Rank == 0)
            {
                numOfRank0.Add(mojiParam.param.IndexOf(param));
            }

            if (param.Rank == 1)
            {
                numOfRank1.Add(mojiParam.param.IndexOf(param));
            }

            if (param.Rank == 2)
            {
                numOfRank2.Add(mojiParam.param.IndexOf(param));
            }
        }
        
        ChusenRank0(mojiParam);
        ChusenRank1(mojiParam);
        ChusenRank2(mojiParam);

        SortMojiBan();
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
            summarySelected.Add(new SelectedMojiPlate()
                { id = sec.id, rank = sec.rank, sprite = sec.sprite, text = sec.text});
        }
        foreach(var sec in selectedRank1)
        {
            summarySelected.Add(new SelectedMojiPlate()
            { id = sec.id, rank = sec.rank, sprite = sec.sprite, text = sec.text });
        }
        foreach(var sec in selectedRank2)
        {
            summarySelected.Add(new SelectedMojiPlate()
            { id = sec.id, rank = sec.rank, sprite = sec.sprite, text = sec.text });
        }

        for(int i=0; i<summarySelected.Count; i++)
        {
            Debug.Log(randomNum[i]);
            summarySelected[i].junban = randomNum[i];
        }


        // 文字盤にselectedNumを埋め込んでいく.
        for(int i=0; i<gameManager.mojiImgList.Count; i++)
        {
            for(int j=0; j<summarySelected.Count; j++)
            {
                if (i == summarySelected[j].junban)
                {
                    MojiButton mojiButton = gameManager.mojiImgList[i].GetComponent<MojiButton>();

                    mojiButton.id = summarySelected[j].id;

                    mojiButton.rank = summarySelected[j].rank;

                    // Debug.Log("ロードしたスプライトのパスは、" + Resources.Load("Sprites/" + summarySelected[j].sprite).name);
                    gameManager.mojiImgList[i].GetComponent<Image>().overrideSprite = Resources.Load<Sprite>("Sprites/" + summarySelected[j].sprite);
                    
                    Text mojiText = gameManager.mojiImgList[i].GetComponentInChildren<Text>();
                    mojiText.text = summarySelected[j].text;
                    mojiButton.dialogText = mojiText.text;
                    // Debug.Log(j + "番目のボタンのテキストは" + mojiButton.dialogText + "であり、しかし表示されているテキストは" + mojiText.text);
                    

                }
            }
        }
    }
    

    private void ChusenRank0(MojiPlateParameter mojiParam)
    {
        for (int i = 0; i < rank0_kazu; i++)
        {
            int r = Random.Range(0, numOfRank0.Count);
            int num = numOfRank0[r];
            
            selectedRank0.Add(new SelectedMojiPlate()
                { id = mojiParam.param[num].ID,
                  rank = mojiParam.param[num].Rank,
                  sprite = mojiParam.param[num].Sprite,
                  text = mojiParam.param[num].Text });
        }
    }

    private void ChusenRank1(MojiPlateParameter mojiParam)
    {
        for (int i = 0; i < rank1_kazu; i++)
        {
            int r = Random.Range(0, numOfRank1.Count);
            int num = numOfRank1[r];
            
            selectedRank1.Add(new SelectedMojiPlate()
                { id = mojiParam.param[num].ID,
                  rank = mojiParam.param[num].Rank,
                  sprite = mojiParam.param[num].Sprite,
                  text = mojiParam.param[num].Text });

            i += CheckDaburi(i, selectedRank1[i].id, selectedRank1);
        }
    }

    private void ChusenRank2(MojiPlateParameter mojiParam)
    {
        for (int i = 0; i < rank2_kazu; i++)
        {
            int r = Random.Range(0, numOfRank2.Count);
            int num = numOfRank2[r];
            
            selectedRank2.Add(new SelectedMojiPlate()
                { id = mojiParam.param[num].ID,
                  rank = mojiParam.param[num].Rank,
                  sprite = mojiParam.param[num].Sprite,
                  text = mojiParam.param[num].Text });

            i += CheckDaburi(i, selectedRank2[i].id, selectedRank2);
            // numOfRank2.RemoveAt(r); // 選択された要素がだぶらないように選択された要素を省く.
        }
    }

    private int CheckDaburi(int num, int id, List<SelectedMojiPlate> selected)
    {
        if(num >= 1)
        {
            for (int i = 0; i < selected.Count - 1; i++)
            {
                if (id == selected[i].id)
                {
                    Debug.Log("重複しました");
                    selected.RemoveAt(num);
                    return -1;
                }
            }
        }

        return 0;
    }

    private void ResetMojiBan()
    {
        numOfRank0.Clear();
        numOfRank1.Clear();
        numOfRank2.Clear();
        selectedRank0.Clear();
        selectedRank1.Clear();
        selectedRank2.Clear();

        summarySelected.Clear();

        integralNum.Clear();
        randomNum.Clear();

    }
}
