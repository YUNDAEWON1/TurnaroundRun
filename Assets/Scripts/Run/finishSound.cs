using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finishSound : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            SoundMgr.Instance.ComeIn();//������ �Ҹ�
        }
    }
    
}
