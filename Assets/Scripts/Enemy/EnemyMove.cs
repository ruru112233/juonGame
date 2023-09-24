using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    // キャラクターを上に移動させる
    public virtual void MoveToTop(float speed)
    {
        transform.position += new Vector3(0, Time.deltaTime * speed, 0);
    }

    // キャラクターを右上に移動させる
    public virtual void MoveToTopRight(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(Time.deltaTime * xSpeed, Time.deltaTime * ySpeed, 0);
    }

    // キャラクターを右に移動させる
    public virtual void MoveToRight(float speed)
    {
        transform.position += new Vector3(Time.deltaTime * speed, 0, 0);
    }

    // キャラクターを右下に移動させる
    public virtual void MoveToBottomRight(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(Time.deltaTime * xSpeed, -Time.deltaTime * ySpeed, 0);
    }

    // キャラクターを下に移動させる
    public virtual void MoveToBottom(float speed)
    {
        transform.position += new Vector3(0, -Time.deltaTime * speed, 0);
    }

    // キャラクターを左下に移動させる
    public virtual void MoveToBottomLeft(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(-Time.deltaTime * xSpeed, -Time.deltaTime * ySpeed, 0);
    }

    // キャラクターを左に移動させる
    public virtual void MoveToLeft(float speed)
    {
        transform.position += new Vector3(-Time.deltaTime * speed, 0, 0);
    }

    // キャラクターを左上に移動させる
    public virtual void MoveToTopLeft(float xSpeed, float ySpeed)
    {
        transform.position += new Vector3(-Time.deltaTime * xSpeed, Time.deltaTime * ySpeed, 0);
    }

}
