using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EndTrigger : MonoBehaviour
{
    List<PlayerController> ComePlayer = new List<PlayerController>();
    int TotalPlayerCount;
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
                player.ChangeState(PlayerController.State.Stop);//��ž���·� ����

                if (ComePlayer.Count ==TotalPlayerCount)//��� �÷��̾ ��¼��� ������
                {
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
                    Debug.Log("�¸�!");
                    player.animator.SetBool("Victory", true);//������ �÷��̾� �¸����
                }
            }
            lastPlayer.animator.SetBool("Die", true);//�÷��̾� ���� ���
            Debug.Log("����");
        }
    }
    

}
