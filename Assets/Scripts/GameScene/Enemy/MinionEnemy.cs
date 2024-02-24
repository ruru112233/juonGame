using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinionEnemy : EnemyShotPattern
{
    private EnemyGenInfo enemyGenInfo_;

    private int enemyMaxHp_ = 0;
    private int enemyHp_ = 0;

    private Animator anime;

    //private Slider hpSlider;

    
    // Item�I�u�W�F�N�g�̃h���b�v���鐔
    private int dropItemCount = 5;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        // EnemyGenInfo�̏�����
        EnemyGenInfoInit();

        enemyMaxHp_ = 10;
        enemyHp_ = enemyMaxHp_;

        anime = this.GetComponent<Animator>();

        // �q�v�f�̃X���C�_�[���擾
        //hpSlider = GetComponentInChildren<Slider>();
        // �X���C�_�[�̍ő�l�ƌ��ݒl�ɍő�HP��������
        //hpSlider.maxValue = enemyHp_;
        //hpSlider.value = enemyHp_;

    }

    void EnemyGenInfoInit()
    {
        enemyGenInfo_.enemyDirectionType = MoveDirectionType.NO_MOVE;
        enemyGenInfo_.firstSpeed = 0;
        enemyGenInfo_.secondSpeed = 0;
        enemyGenInfo_.shotPattern = ShotScriptList.Count;
    }


    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        // �G�l�~�[�̈ړ�
        MoveEnemy(enemyGenInfo_);

        // �G�l�~�[�̍U���p�^�[������
        SetShotScript();
    }

    private void MoveEnemy(EnemyGenInfo enemyInfo)
    {
        switch (CheckMethod(enemyInfo))
        {
            case 0:
                MoveDirection(enemyInfo.enemyDirectionType);
                break;
            case 1:
                MoveDirection(enemyInfo.enemyDirectionType, enemyInfo.firstSpeed);
                break;
            case 2:
                MoveDirection(enemyInfo.enemyDirectionType, enemyInfo.firstSpeed, enemyInfo.secondSpeed);
                break;
            default:
                Debug.Log("��`�l�ȊO");
                break;
        }
    }

    private void SetShotScript()
    {
        if (enemyGenInfo_.shotPattern < ShotScriptList.Count)
        {
            ActiveScriptByIndex(enemyGenInfo_.shotPattern);
        }
    }

    public void SetEnemyGenInfo(EnemyGenInfo enemyInfo)
    {
        EnemyGenInfoInit();
        enemyGenInfo_ = enemyInfo;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {

            enemyHp_--;

            //hpSlider.value = enemyHp_;
            if (enemyHp_ <= 0)
            {
                // �_���[�W�A�j���[�V����
                if (anime)
                {
                    anime.SetTrigger("damage");
                }

                // MaxHp�̍X�V
                enemyHp_ = enemyMaxHp_;
                ScatterItem();
                //Destroy(gameObject);
            }
        }
    }

    // Item���U�炵�Ĕz�u����֐�
    private void ScatterItem()
    {
        for (int i = 0; i < dropItemCount; i++)
        {
            GameObject obj = SetItemObj();

            Debug.Log(obj);

            if (obj)
            {
                Vector3 randomItemPos = RandomPosition(transform.position);
                Instantiate(obj, randomItemPos, Quaternion.Euler(0, 0, 45));
            }

            
        }
    }

    // Item�I�u�W�F�N�g�������_���ɔz�u���邽�߂̊֐�
    private Vector3 RandomPosition(Vector3 targetPos)
    {
        Vector3 pos = targetPos;

        pos.x = Random.Range(targetPos.x - 0.5f, targetPos.x + 0.5f);
        pos.y = Random.Range(targetPos.y - 0.5f, targetPos.y + 0.5f);

        return pos;
    }

    private GameObject SetItemObj()
    {
        GameObject obj = null;

        int rand = Random.Range(0, 5);

        switch (rand)
        {
            case 0:
                obj = GameManager.instance.itemStock.jimiGuiterObj;
                break;
            case 1:
            case 2:
                obj = GameManager.instance.itemStock.johnGuiterObj;
                break;
            case 3:
            case 4:
                obj = GameManager.instance.itemStock.thunder;
                break;
            default:
                Debug.Log("SetItemObj error");
                break;
        }

        return obj;
    }

}
