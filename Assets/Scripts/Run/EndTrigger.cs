using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;


public class EndTrigger : MonoBehaviour
{
    List<PlayerController> ComePlayer = new List<PlayerController>();
    public PlayerController lastPlayer;
    public PlayerController player;
    int TotalPlayerCount;
    float delayTime;
    private void Start()
    {
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();//�÷��̾���Ʈ�� �������ִ� �ֵ� ã�Ƽ� �迭�� ����
        TotalPlayerCount = allPlayers.Length; //����÷��̾� ��
        delayTime = 0;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// �÷��̾� �±׸� ������ ������
        {
            player = other.gameObject.GetComponent<PlayerController>();
            if (player != null)
            {
                ComePlayer.Add(player);//�÷��̾� �߰�
                SoundMgr.Instance.ComeIn();//������ �Ҹ�
                if (ComePlayer.Count == TotalPlayerCount)//��� �÷��̾ ��¼��� ������
                {
                    StartCoroutine(DefeatPlayerAfterDelay());
                }
            }
        }
    }
    private void SetCameraLastPlayer(PlayerController lastPlayer)
    {
        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.SetLastPlaceCharacter(lastPlayer.gameObject);
        }
    }
    IEnumerator DefeatPlayerAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (ComePlayer.Count > 0)
        {
            lastPlayer = ComePlayer[ComePlayer.Count - 1];
            foreach (PlayerController player in ComePlayer)
            {
                if (player != lastPlayer)
                {
                    Debug.Log("�¸�!");
                }
            }

            lastPlayer.ChangeState(PlayerController.State.Stop);
            lastPlayer.animator.SetBool("Die", true);
            Debug.Log("����");

            // CameraController�� lastPlayer ����
            SetCameraLastPlayer(lastPlayer);
        }
    }
}
