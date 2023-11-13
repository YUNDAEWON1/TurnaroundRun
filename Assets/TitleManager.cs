using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    //��ư ���� ����

    public Button btnSetting;
    public Button btnExit;
    public Button btnGameStart;


    public GameObject pnlSetting;

    //���� â ���� ����
    public Slider sldSoundFxVolume;
    public Slider sldBGMVolume;

    public Toggle toggleSoundFxMute;
    public Toggle toggleBGMMute;


    private bool isSoundFxMuted = false;
    private bool isBGMMuted = false;



    #region AddListner ����
    private void Start()
    {
        //��ư AddListner
        btnExit.onClick.AddListener(() =>
        {
            SoundMgr.Instance.PlayButtonClickSound();
            UIManager.Instance.OnClickExit();
        });
        btnSetting.onClick.AddListener(() =>
        {
            SoundMgr.Instance.PlayButtonClickSound();
            UIManager.Instance.ActivePnl(pnlSetting);
        });
       
        btnGameStart.onClick.AddListener(() =>
        {
            SoundMgr.Instance.PlayGameStartSound();
            OnClickGameStart();
        });


        //����
        sldBGMVolume.onValueChanged.AddListener((value) =>
        {
            SoundMgr.Instance.SetBGMVolume(value);

        });

        sldSoundFxVolume.onValueChanged.AddListener((value) =>
        {
            SoundMgr.Instance.SetSoundFxVolume(value);

        });

        toggleBGMMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundMgr.Instance.MuteBGM(isMuted);

        });

        toggleSoundFxMute.onValueChanged.AddListener((isMuted) =>
        {
            SoundMgr.Instance.MuteSoundFx(isMuted);

        });
    }

    #endregion

    #region TItle �� ��ư ��� �Լ�
    

    public void OnClickGameStart()
    {
        SoundMgr.Instance.ChangeBGMForScene();//���� ����
        SceneManager.LoadScene("Game");
    }
    #endregion

    #region SETTING â ��� �Լ�

    //���� ���� �Լ�
    public void SetSoundFxVolume(float volume)
    {

        SoundMgr.Instance.SetSoundFxVolume(volume);

        if (volume <= 0)
        {
            isSoundFxMuted = true;
            toggleSoundFxMute.isOn = true;
        }
        else
        {
            isSoundFxMuted = false;
            toggleSoundFxMute.isOn = false;
        }
    }

    public void SetBGMVolume(float volume)
    {
        Debug.Log("Setting BGM volume to: " + volume);
        SoundMgr.Instance.SetBGMVolume(volume);

        if (volume <= 0)
        {
            isBGMMuted = true;
            toggleBGMMute.isOn = true;
        }
        else
        {
            isBGMMuted = false;
            toggleBGMMute.isOn = false;
        }
    }

    //���Ұ� �Լ�
    public void ToggleBGMMute(bool isMuted)
    {
        isBGMMuted = isMuted;

        // SoundManager�� ���Ұ� ��� ȣ��
        SoundMgr.Instance.MuteBGM(isBGMMuted);
    }

    public void ToggleSoundFxMute(bool isMuted)
    {
        isSoundFxMuted = isMuted;

        // SoundManager�� ���Ұ� ��� ȣ��
        SoundMgr.Instance.MuteSoundFx(isSoundFxMuted);

    }
    #endregion


    


}
