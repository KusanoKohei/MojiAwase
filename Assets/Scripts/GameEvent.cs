using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public static GameEvent instance;

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


    GameManager gameManager => GameManager.instance;

    public IEnumerator StageClearEvent()
    {
        Debug.Log("GameEventクラス内StageClearEvent関数です\nクリア条件達成です");
        AudioManager.instance.audioSource.Stop();
        AudioManager.instance.audioSource.PlayOneShot(AudioManager.instance.voiceClips[4]);

        gameManager.OnClick();
        yield return null;
    }
}
