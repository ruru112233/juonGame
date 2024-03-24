using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    const float DESTROY_TOP_LINE = 10.0f;
    const float DESTROY_RIGHT_LINE = 8.0f;
    const float DESTROY_REFT_LINE = -8.0f;
    const float DESTROY_BOTTOM_LINE = -10.0f;

    // Enemy�ɏ���n���\����
    public struct EnemyGenInfo
    {
        public MoveDirectionType enemyDirectionType; // Enemy�̕���
        public float firstSpeed; // �X�s�[�h���P��ނ̂ݐݒ肷��ꍇ�ɗ��p
        public float secondSpeed; // �X�s�[�h���Q��ސݒ肷��ꍇ�ɗ��p�ifirstSpeed��x���AsecondSpeed��y���j
        public int shotPattern; // �U���p�^�[�� �F �z��ɐݒ肷��ԍ����i�[����
    }


    public enum MoveDirectionType
    {
        TOP,            // �����
        RIGHT,          // �E����
        BOTTOM,         // ������
        LEFT,           // ������
        TOP_RIGHT,      // �E�����
        BOTTOM_RIGHT,   // �E������
        TOP_LEFT,       // �������
        BOTTOM_LEFT,    // ��������
        NO_MOVE,        // �ړ����Ȃ�
    }

    // method���`�F�b�N����B-1:�G���[�A0:�����P�� 1:�����Q�� 2:�����R��
    public int CheckMethod(EnemyGenInfo enemyGenInfo)
    {
        if (enemyGenInfo.enemyDirectionType == MoveDirectionType.NO_MOVE)
        {
            // �G���[
            //Debug.Log("�������w�肵�Ă�������");
            return -1;
        }

        int methodNo = 0;

        if (enemyGenInfo.firstSpeed != 0)
        {
            methodNo++;
            if (enemyGenInfo.secondSpeed != 0)
            {
                methodNo++;
            }
        }

        return methodNo;
    }

    // �L�����N�^�[�Ɉړ�������i�X�s�[�h�́Ax���Ay���Ƃ���1.0f�j
    public virtual void MoveDirection(MoveDirectionType moveDirectionType)
    {
        // ������moveDirectionType�ɂ��������擾
        Vector2 direction = GetDirectionVector(moveDirectionType);
        
        // �ړ�
        Move(direction.x, direction.y);
    }

    // �L�����N�^�[�Ɉړ�������i�X�s�[�h�́A�����Ɏw�肵���l�ɂȂ�j
    public virtual void MoveDirection(MoveDirectionType moveDirectionType, float speed)
    {
        // ������moveDirectionType�ɂ��������擾
        Vector2 direction = GetDirectionVector(moveDirectionType);

        // �����̒l���Βl�ɂ���
        float absSpeed = Mathf.Abs(speed);
        // �ړ�
        Move(direction.x * absSpeed, direction.y * absSpeed);
    }

    // �L�����N�^�[�Ɉړ�������i�X�s�[�h�́Ax��y�����ꂼ��w��o����j
    public virtual void MoveDirection(MoveDirectionType moveDirectionType, float xSpeed, float ySpeed)
    {
        // ������moveDirectionType�ɂ��������擾
        Vector2 direction = GetDirectionVector(moveDirectionType);
        
        // �����̒l���Βl�ɂ���
        float absXSpeed = Mathf.Abs(xSpeed);
        float absYSpeed = Mathf.Abs(ySpeed);
        // �ړ�
        Move(direction.x * absXSpeed, direction.y * absYSpeed);
    }

    // MoveDirectionType�ɂ��i�܂�������ivecter2�j��ԋp����
    private Vector2 GetDirectionVector(MoveDirectionType directionType)
    {
        Vector2 direction = Vector2.zero;
        switch (directionType)
        {
            case MoveDirectionType.TOP:
                // ��Ɉړ�
                direction = new Vector2(0, 1);
                break;
            case MoveDirectionType.RIGHT:
                // �E�Ɉړ�
                direction = new Vector2(1, 0);
                break;
            case MoveDirectionType.BOTTOM:
                // ���Ɉړ�
                direction = new Vector2(0, -1);
                break;
            case MoveDirectionType.LEFT:
                // ���Ɉړ�
                direction = new Vector2(-1, 0);
                break;
            case MoveDirectionType.TOP_RIGHT:
                // �E��Ɉړ�
                direction = new Vector2(1, 1);
                break;
            case MoveDirectionType.BOTTOM_RIGHT:
                // �E���Ɉړ�
                direction = new Vector2(1, -1);
                break;
            case MoveDirectionType.TOP_LEFT:
                // ����Ɉړ�
                direction = new Vector2(-1, 1);
                break;
            case MoveDirectionType.BOTTOM_LEFT:
                // �����Ɉړ�
                direction = new Vector2(-1, -1);
                break;
            case MoveDirectionType.NO_MOVE:
                // �ړ����Ȃ��Ɉړ�
                direction = Vector2.zero;
                break;
            default:
                Debug.Log("Error:MoveDirectionType��`�l�ȊO");
                break;
        }

        return direction;
    }

    private void Move(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(Time.deltaTime * xSpeed, Time.deltaTime * ySpeed, 0);
    }

    public virtual void Update()
    {
        DestroyEnemy();
    }

    private void DestroyEnemy()
    {
        if (this.transform.position.x > DESTROY_RIGHT_LINE ||
            this.transform.position.x < DESTROY_REFT_LINE ||
            this.transform.position.y > DESTROY_TOP_LINE ||
            this.transform.position.y < DESTROY_BOTTOM_LINE)
        {
            Destroy(this.gameObject);
        }
    }

}
