using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostManager : MonoBehaviour
{
    private static BoostManager instance;
    public static BoostManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<BoostManager>();
                if(instance = null)
                {
                    GameObject obj = new GameObject("BoostManager");
                    instance = obj.AddComponent<BoostManager>();
                }
            }
            return instance;
        }
    }

    private List<PlayerController> players = new List<PlayerController>();
    public int currentBoostPlayerIndex;


    void Start()
    {
        List<GameObject> findPlayers = CharacterManager.Instance.characters;
        foreach(GameObject character in findPlayers)
        {
            players.Add(character.GetComponent<PlayerController>());
        }
        currentBoostPlayerIndex = UnityEngine.Random.Range(0, players.Count);//�������� �÷��̾� ����
        setCurrentBoostPlayer(currentBoostPlayerIndex);//���õ� �÷��̾��� �ν��� Ȱ��ȭ
        Debug.Log(currentBoostPlayerIndex);
    }
    public void setCurrentBoostPlayer(int index) //Ư�� �ε����� �÷��̾ �ν�Ʈ ����Ҽ� �ְ� ����
    {
        foreach (PlayerController player in players) //��� �÷��̾� �ν�Ʈ ��Ȱ��ȭ
        {
            player.canBoost = false;
        }
        players[index].canBoost = true; //���� �ν�Ʈ�� ����Ҽ� �ִ� �÷��̾ Ȱ��ȭ
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
