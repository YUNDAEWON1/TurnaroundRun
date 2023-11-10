using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    
    public float normarSpeed { get ;set; }
    public float MaxSpeed {  get; set; }
    public float MinSpeed { get; set; }
    
    public float WaitTime { get; set; }
    public bool IsCoolTime { get; private set; }

    public enum State
    {
        Idle,Run,Stop
    }

    State PlayerState;

    private Rigidbody rigid;
    private Animator animator;
    // Start is called before the first frame update
    private void Awake()
    {
        Application.targetFrameRate = 60; //60������ ����(�����Ÿ� ����, Fixed Timestep�� (1 / 60) = 0.016667 �� ����)
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PlayerState = State.Idle;
        WaitTime = 0f;
        IsCoolTime = false;
        MaxSpeed = 10f;
        MinSpeed = 3f;      
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (PlayerState)
        {
            case State.Idle://���̵����
                if (WaitTime >= 3f)
                {
                    ChangeState(State.Run);//3���� ������
                    WaitTime = 0;
                }
                else
                {
                    WaitTime += Time.deltaTime;
                }
                break;


            case State.Run://�޸��»���
                //normalSpeed�� �޸�
                if (WaitTime >= 15f) //�ð��� 15�� �̻��̸�
                {
                    ChangeState(State.Stop);
                }
                else
                {
                    if (!IsCoolTime)//�ð����� ��Ÿ���� �ƴѰ��
                    {
                        StartCoroutine(CollTime());
                    }                  
                    rigid.velocity = new Vector3(0, 0, normarSpeed);

                    WaitTime += Time.deltaTime; //�ð� ����
                }
                break;


            case State.Stop://�������
                animator.SetFloat("Speed", 0);//���� Ʈ�� �Ķ���� Speed�� 0���� ����(���̵� ���� ���)
                break;
        }
    }
    void ChangeState(State state)
    {
        PlayerState = state; //���º���
    }
    IEnumerator CollTime()
    {
        normarSpeed = Random.Range(MinSpeed, MaxSpeed);//ǥ�� ���ǵ� �����ϰ� ����(�ּ�,�ִ�)
        animator.SetFloat("Speed", normarSpeed);//�ִϸ����� ���� Ʈ�� �Ķ���� Speed����
        IsCoolTime = true;
        yield return new WaitForSeconds(2f);//2�ʵ� �ӵ� ���� , �������� �ٲ㵵 ��
        IsCoolTime = false;
    }
}
