using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO : ĳ���͵� Prefab ��ο� �ش� ��ũ��Ʈ�� ���־�� �� 
public class CharacterInfo : MonoBehaviour
{
    public int characterID; // ĳ������ ������ ID ��
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    // �ش� ĳ���Ͱ� ���õǾ��� �� ȣ��Ǵ� �Լ�
    public void OnSelect()
    {
        // ĳ���� �ִϸ��̼� Ȱ��ȭ 
    }
}
