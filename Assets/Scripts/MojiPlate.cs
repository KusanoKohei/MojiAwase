using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MojiPlate : MonoBehaviour,IBeginDragHandler, IDragHandler,IDropHandler, IEndDragHandler
{
    private GameObject mojiPlate;

    [SerializeField]
    private Image mojiPlateImage;

    public Text mojiText;

    [SerializeField]
    private Vector3 initPos;
    [SerializeField]
    private Vector3 prePos;

    private Hand hand;

    RaycastHit hit;

    GameManager gameManager => GameManager.instance;

    private void Start()
    {
        mojiPlate = this.gameObject;
        initPos = mojiPlate.transform.position;

        hand = FindObjectOfType<Hand>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (mojiText.text == "") return;

        // 文字プレートを動かす前のポジションを記録する.
        prePos = mojiPlate.transform.position;

        // 文字プレートを最前面に表示.
        mojiPlate.transform.SetAsLastSibling();

        // ドラッグ中の文字プレートを少し暗くする.
        mojiPlateImage.color = Color.gray;


        // 文字の情報を仲介人に渡す.
        hand.SetGrabbingMojiPlate(this);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (mojiText.text == "") return;

        // 文字プレートがポインタを追いかける.
        this.transform.position = hand.transform.position;
    }

    public string SetMojiPlate()
    {
        int number = Random.Range(0, gameManager.mojiList.Count);
        number = 0; // デバッグ用.
        mojiText.text = gameManager.mojiList[number].ToString();

        return mojiText.text;
    }
    
    public void OnDrop(PointerEventData eventData)
    {
        // 仲介人からデータを受け取る.

        MojiPlate gotMojiPlate = hand.GetMojiPlateData();
        hand.SetGrabbingMojiPlate(null);

        // ドロップした瞬間に、マウスポインタ直下にあるオブジェクトを取得.
        // 参考（https://qiita.com/shundroid/items/40b619acfdd1605c32ee).

        Vector3 pos = this.transform.position;

        RaycastHit2D hit = Physics2D.Raycast(pos, new Vector3(0, 0, 1), 100);

        //なにかと衝突した時だけそのオブジェクトの名前をログに出す.
        if(hit.collider == null)
        {
            Debug.Log("何物にもヒットしていない");
            


            return;
        }
        else if (hit.collider)
        {
            MojiSlot mojiSlot = hit.collider.GetComponent<MojiSlot>();
            Debug.Log(mojiSlot.dialogText);

            
            if (mojiSlot.rank == 0)
            {
                Debug.Log("Excellent !!");
                DialogManager.instance.dialogText.text = "大正解！";
            }
            else if (mojiSlot.rank == 1)
            {
                Debug.Log("Correct");
                DialogManager.instance.dialogText.text = "せいか～い";
            }
            else if (mojiSlot.rank == 2)
            {
                Debug.Log("incorrect lol");
                DialogManager.instance.dialogText.text = "それじゃない!!! lol";
            }
            else
            {
                // 文字プレートを直前の位置に戻す.
                mojiPlate.transform.position = prePos;
            }

            // 文字プレートを初期位置に戻す.
            mojiPlate.transform.position = initPos;

        }




        // 何もなければ早期リターン.
        // 文字スロット以外であれば早期リターン.
        // 文字スロットであればそのデータを取得.

        // 文字スロットのデータと文字プレートのデータを照合する.

        // 照合したデータの正誤を判定する.
        
        // 自身の持つデータと合わせて正誤の判定をする.

        // 正誤の判定結果を渡す.
        
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 灰色になった文字プレートを戻す.
        mojiPlateImage.color = Color.white;

        // ドロップ終了後にレイキャストを飛ばし、他のオブジェクトと当たったなら、

        
    }
}
