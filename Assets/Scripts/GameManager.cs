using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Linq;

public class GameManager : MonoBehaviour
{
    public List<string> mojiList;
    public List<GameObject> mojiImgList;

    public MojiPlate mojiPlate;

    [SerializeField]
    private string themeMoji;

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
        // 効果音；スタート.
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.voiceClips[0]);

        OnClick();
    }
    
    public void OnClick()
    {
        mojiPlate = FindObjectOfType<MojiPlate>();
        themeMoji = mojiPlate.SetMojiPlate();

        SetMojiBan.instance.SortFromMojiData(themeMoji);

        // SetMojiBan.instance.DividRank(DataLoader.instance.LoadForSetMojiban(themeMoji));
    }

    public MojiDataGeneral MojiDataLoad()
    {
        string assetDataPath = "ExcelData/MojiDataGeneral";

        MojiDataGeneral mojiParam = Resources.Load(assetDataPath) as MojiDataGeneral;

        return mojiParam;
    }

   

    static internal string KatakanaConvert(string themeMoji)
    {
        // themeMojiの先頭一文字をstring型からchar型に変換.
        char charMoji = themeMoji[0];
        

        StringBuilder sb = new StringBuilder();
        

        if(charMoji >= 'あ' && charMoji <= 'ん')
        {//-> カタカナの範囲.
            charMoji = (char)(charMoji + 0x0060);   // 変換.
        }
        sb.Append(charMoji);

        return sb.ToString();
    }
}
