using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    // �L�����N�^�[����Ɉړ�������
    public virtual void MoveToTop(float speed)
    {
        transform.position += new Vector3(0, Time.deltaTime * speed, 0);
    }

    // �L�����N�^�[���E��Ɉړ�������
    public virtual void MoveToTopRight(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(Time.deltaTime * xSpeed, Time.deltaTime * ySpeed, 0);
    }

    // �L�����N�^�[���E�Ɉړ�������
    public virtual void MoveToRight(float speed)
    {
        transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
    }

    // �L�����N�^�[���E���Ɉړ�������
    public virtual void MoveToBottomRight(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(Time.deltaTime * xSpeed, -Time.deltaTime * ySpeed, 0);
    }

    // �L�����N�^�[�����Ɉړ�������
    public virtual void MoveToBottom(float speed)
    {
        transform.position += new Vector3(0, -Time.deltaTime * speed, 0);
    }

    // �L�����N�^�[�������Ɉړ�������
    public virtual void MoveToBottomLeft(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(-Time.deltaTime * xSpeed, -Time.deltaTime * ySpeed, 0);
    }

    // �L�����N�^�[�����Ɉړ�������
    public virtual void MoveToLeft(float speed)
    {
        transform.position += new Vector3(-Time.deltaTime * speed, 0, 0);
    }

    // �L�����N�^�[������Ɉړ�������
    public virtual void MoveToTopLeft(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(-Time.deltaTime * xSpeed, Time.deltaTime * ySpeed, 0);
    }

}
