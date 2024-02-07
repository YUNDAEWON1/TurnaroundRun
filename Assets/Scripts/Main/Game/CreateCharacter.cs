using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateCharacter : MonoBehaviour
{
    [SerializeField] List<GameObject> characterGroup;       // ĳ���� �����յ��� ��
    [SerializeField] public List<GameObject> selectedCharacter;    // ���õ� ĳ����
    List<int> selectedIndex;

 
    void Awake()
    { 
        selectedIndex = FindObjectOfType<LobbyUIManager>().GetSelectedToggleIndexes();

        SetCharactersPosition();

        CharacterManager.Instance.characters = selectedCharacter;
    }


    public void SetCharactersPosition()
    {
        int selectedNum = selectedIndex.Count;
        float firstPosition = 0 - 1f * (selectedNum - 1);

        // selectedIndex�� �ִ� ������ selectedCharacter�� �־��ֱ� 
        // ���õ� ĳ���͵鸸 ȭ�鿡 ���̰� Ȱ��ȭ
        for (int i = 0; i < selectedIndex.Count; i++)
        {
            GameObject cha = Instantiate(characterGroup[selectedIndex[i]], new Vector3(firstPosition, 0, 0), Quaternion.identity);
            selectedCharacter.Add(cha);

            firstPosition += 2;
        }
    }
}
