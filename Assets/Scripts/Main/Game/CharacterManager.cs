using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    private static CharacterManager instance;

    public List<GameObject> characters;       // �޸��� �ִ� ĳ���͵�

    public static CharacterManager Instance
    {
        get 
        { 
            if(!instance)
            {
                instance = FindObjectOfType<CharacterManager>();

                // ���� CharacterManager�� ���� ��� ���� ����
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject("CharacterManager");
                    instance = singletonObject.AddComponent<CharacterManager>();
                }
            }

            return instance; 
        }
    }


    void Start()
    {
        SelectFirstPlace();
    }

    void Update()
    {
        
    }

    private void SelectFirstPlace()
    {

    }

    public void AddCharacter(GameObject character)
    {
        characters.Add(character);
    }
}
