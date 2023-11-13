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

    public GameObject SelectFirstPlace()
    {
        float maxZ = 0;
        GameObject firstPlace = new GameObject();
        foreach(GameObject character in characters)
        {
            float zPosition = character.transform.position.z;
            if (maxZ < zPosition)
            {
                maxZ = zPosition;
                firstPlace = character;
            }
        }

        return firstPlace;
    }

    public void AddCharacter(GameObject character)
    {
        characters.Add(character);
    }
}
