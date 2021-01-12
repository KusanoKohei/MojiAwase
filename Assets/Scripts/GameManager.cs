using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Linq;
using UniRx;

public class GameManager : MonoBehaviour
{
    public List<string> mojiList;
    public List<GameObject> mojiImgList;

    public MojiPlate mojiPlate;

    [SerializeField]
    private string themeMoji;

    public Text currentScoreText;
    public static ReactiveProperty<int> rp_currentScore = new ReactiveProperty<int>(0);

    public Text highScoreText;
    public static ReactiveProperty<int> rp_highScore = new ReactiveProperty<int>(0);

    public Text correctCount;
    public static ReactiveProperty<int> rp_correctCount = new ReactiveProperty<int>(0);

    [SerializeField]
    private int clearCount = 3;

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

        // PlayerPrefのスコアをハイスコアの値に参照する.
        // 後にハイスコアの値を表示させる値に反映させる.
        rp_highScore.Value = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        highScoreText.text = rp_highScore.Value.ToString();

        // 毎フレームごとに現在スコアの値をチェックし、
        // 値が変動すれば表示させるスコアを変更する.
        rp_currentScore.ObserveEveryValueChanged(_ => _.Value)
            .SubscribeToText(currentScoreText);
        
        rp_currentScore.ObserveEveryValueChanged(_ => _.Value)
            .Where(_ => _ > rp_highScore.Value)
            .Subscribe(_=> {
                rp_highScore.Value = rp_currentScore.Value;
                PlayerPrefs.SetInt("HIGH_SCORE", rp_highScore.Value);
                }); 

        rp_highScore.ObserveEveryValueChanged(_ => _.Value)
            .SubscribeToText(highScoreText);

        rp_correctCount.ObserveEveryValueChanged(_ => _.Value)
            .SubscribeToText(correctCount);

        rp_correctCount.ObserveEveryValueChanged(_ => _.Value)
            .Where(_ => _ >= clearCount)
            .Subscribe(_=> {
                StartCoroutine(GameEvent.instance.StageClearEvent());
                });


        OnClick();
    }
    
    public void OnClick()
    {
        Debug.Log("ゲーム開始時に初期化しました");
        rp_currentScore.Value = 0;
        rp_correctCount.Value = 0;

        mojiPlate = FindObjectOfType<MojiPlate>();
        themeMoji = mojiPlate.SetMojiPlate();

        SetMojiBan.instance.SortFromMojiData(themeMoji);
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

    public void DataReset()
    {
        PlayerPrefs.DeleteKey("HIGH_SCORE");
        Debug.Log("ハイスコアキーを削除しました");
    }
}
