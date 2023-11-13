using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndTrigger : MonoBehaviour
{
    List<PlayerController> ComePlayer = new List<PlayerController>();
    int TotalPlayerCount;
    PlayerController Stop;
    private void Start()
    {
        PlayerController[] allPlayers = FindObjectsOfType<PlayerController>();//�÷��̾���Ʈ�� �������ִ� �ֵ� ã�Ƽ� �迭�� ����
        TotalPlayerCount = allPlayers.Length; //����÷��̾� ��
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")// �÷��̾� �±׸� ������ ������
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            if(player != null)
            {
                ComePlayer.Add(player);//�÷��̾� �߰�

                if(ComePlayer.Count ==TotalPlayerCount)//��� �÷��̾ ��¼��� ������
                {
                    player.ChangeState(PlayerController.State.Stop);//��ž���·� ����
                    DefeatPlayer();//�й��� ����
                }
            }   
        }
    }
    public void DefeatPlayer()//�й��� ���� �Լ�
    {
        if(ComePlayer.Count > 0)
        {
            PlayerController lastPlayer = ComePlayer[ComePlayer.Count-1];//���������� ���� �÷��̾� ã��
            foreach(PlayerController player in ComePlayer)
            {
                if(player != lastPlayer)
                {
                    player.animator.SetBool("Victory", true);//������ �÷��̾� �¸����
                }
            }
            lastPlayer.animator.SetBool("Die", true);//�÷��̾� ���� ���
        }
    }
    

}
