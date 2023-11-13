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
        Idle,Run,Stop,Boost
    }

    State PlayerState;

    public Boost.BoostEvent onBoost;

    private float plusSpeed;

    private Rigidbody rigid;

    public bool canBoost;

    private Animator animator;

    private float BoostTime;

    public GameObject BoostParticlePrefab;//�ν��� ��ƼŬ ���� ������Ʈ 
    private GameObject BoostParticleInstance;//�� ������Ʈ�� ������ �ν��Ͻ� 
    // Start is called before the first frame update
    private void Awake()
    {
        onBoost = new Boost.BoostEvent();
        onBoost.AddListener(BoostPlayerSpeed);
        Application.targetFrameRate = 60; //60������ ����(�����Ÿ� ����, Fixed Timestep�� (1 / 60) = 0.016667 �� ����)
    }

    private void BoostPlayerSpeed()//�ν��� �Լ�
    {
        plusSpeed = MaxSpeed - normarSpeed;
        normarSpeed = normarSpeed + plusSpeed; //���ǵ带 �ִ�� ��
    }
    
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        PlayerState = State.Idle;
        WaitTime = 0f;
        BoostTime = 0f;
        IsCoolTime = false;
        MaxSpeed = 10f;
        MinSpeed = 3f;
        BoostParticleInstance = Instantiate(BoostParticlePrefab, transform.position, Quaternion.identity, transform); //�ν��� �������� �÷��̾� �߾ӿ� ���� > �ν��� �ν��Ͻ� ����
        BoostParticleInstance.SetActive(false);
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
                //Debug.Log("�޸���");
                if (WaitTime > 15f) //�ð��� 15�� �̻��̸�
                {
                    ChangeState(State.Stop);
                }
               
                else
                {
                    if (canBoost == true && WaitTime >= 7f)// �ν�Ʈ ��밡�ɻ��� �̰� �ð��� 7�� �̻��̸�
                    {
                        Debug.Log("�ν��� ���·� ��ȯ");
                        ChangeState(State.Boost);//�ν��� ���·� ��ȯ
                    }
                    if (!IsCoolTime)//�ð����� ��Ÿ���� �ƴѰ��
                    {
                        StartCoroutine(CollTime());
                    }                  
                    rigid.velocity = new Vector3(0, 0, normarSpeed);

                    WaitTime += Time.deltaTime; //�ð� ����
                }
                break;


            case State.Stop://�������
                Debug.Log("����!");
                animator.SetFloat("Speed", 0);//���� Ʈ�� �Ķ���� Speed�� 0���� ����(���̵� ���� ���)
                break;

            case State.Boost://�ν�Ʈ �����϶�
                Debug.Log("�ν�Ʈ");
                WaitTime += Time.deltaTime; //�ð� ����
                if (BoostTime >= 3f)
                {
                    BoostParticleInstance.SetActive(false);//������� ��ƼŬ ��Ȱ��ȭ                   
                    normarSpeed = normarSpeed - plusSpeed; //���ǵ� ���� ���ǵ�
                    rigid.velocity = new Vector3(0, 0, normarSpeed);
                    animator.SetFloat("Speed", normarSpeed);//�ִϸ��̼� �ӵ��� �������
                    canBoost = false;//�ν�Ʈ ��Ȱ��ȭ
                    ChangeState(State.Run);//�޸��� ���·� ��ȯ
                }
                else
                {
                    BoostParticleInstance.transform.rotation = Quaternion.Euler(0f,180f,0f);//�ν��� ȸ�� 
                    BoostParticleInstance.SetActive(true);//�ν��� Ȱ��ȭ
                    BoostPlayerSpeed();//�ִ� �ӵ�
                    rigid.velocity = new Vector3(0, 0, normarSpeed);
                    animator.SetFloat("Speed", 10);//�ִϸ��̼ǵ� �ִ�ӵ�
                    BoostTime += Time.deltaTime;//�ð�����
                }              
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
