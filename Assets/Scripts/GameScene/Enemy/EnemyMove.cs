using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    const float DESTROY_TOP_LINE = 10.0f;
    const float DESTROY_RIGHT_LINE = 8.0f;
    const float DESTROY_REFT_LINE = -8.0f;
    const float DESTROY_BOTTOM_LINE = -10.0f;

    // Enemyに情報を渡す構造体
    public struct EnemyGenInfo
    {
        public MoveDirectionType enemyDirectionType; // Enemyの方向
        public float firstSpeed; // スピードを１種類のみ設定する場合に利用
        public float secondSpeed; // スピードを２種類設定する場合に利用（firstSpeedはx軸、secondSpeedはy軸）
        public int shotPattern; // 攻撃パターン ： 配列に設定する番号を格納する
    }


    public enum MoveDirectionType
    {
        TOP,            // 上方向
        RIGHT,          // 右方向
        BOTTOM,         // 下方向
        LEFT,           // 左方向
        TOP_RIGHT,      // 右上方向
        BOTTOM_RIGHT,   // 右下方向
        TOP_LEFT,       // 左上方向
        BOTTOM_LEFT,    // 左下方向
        NO_MOVE,        // 移動しない
    }

    // methodをチェックする。-1:エラー、0:引数１つ 1:引数２つ 2:引数３つ
    public int CheckMethod(EnemyGenInfo enemyGenInfo)
    {
        if (enemyGenInfo.enemyDirectionType == MoveDirectionType.NO_MOVE)
        {
            // エラー
            //Debug.Log("方向を指定してください");
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

    // キャラクターに移動させる（スピードは、x軸、y軸ともに1.0f）
    public virtual void MoveDirection(MoveDirectionType moveDirectionType)
    {
        // 引数のmoveDirectionTypeによる方向を取得
        Vector2 direction = GetDirectionVector(moveDirectionType);
        
        // 移動
        Move(direction.x, direction.y);
    }

    // キャラクターに移動させる（スピードは、引数に指定した値になる）
    public virtual void MoveDirection(MoveDirectionType moveDirectionType, float speed)
    {
        // 引数のmoveDirectionTypeによる方向を取得
        Vector2 direction = GetDirectionVector(moveDirectionType);

        // 引数の値を絶対値にする
        float absSpeed = Mathf.Abs(speed);
        // 移動
        Move(direction.x * absSpeed, direction.y * absSpeed);
    }

    // キャラクターに移動させる（スピードは、x軸y軸それぞれ指定出来る）
    public virtual void MoveDirection(MoveDirectionType moveDirectionType, float xSpeed, float ySpeed)
    {
        // 引数のmoveDirectionTypeによる方向を取得
        Vector2 direction = GetDirectionVector(moveDirectionType);
        
        // 引数の値を絶対値にする
        float absXSpeed = Mathf.Abs(xSpeed);
        float absYSpeed = Mathf.Abs(ySpeed);
        // 移動
        Move(direction.x * absXSpeed, direction.y * absYSpeed);
    }

    // MoveDirectionTypeにより進ませる方向（vecter2）を返却する
    private Vector2 GetDirectionVector(MoveDirectionType directionType)
    {
        Vector2 direction = Vector2.zero;
        switch (directionType)
        {
            case MoveDirectionType.TOP:
                // 上に移動
                direction = new Vector2(0, 1);
                break;
            case MoveDirectionType.RIGHT:
                // 右に移動
                direction = new Vector2(1, 0);
                break;
            case MoveDirectionType.BOTTOM:
                // 下に移動
                direction = new Vector2(0, -1);
                break;
            case MoveDirectionType.LEFT:
                // 左に移動
                direction = new Vector2(-1, 0);
                break;
            case MoveDirectionType.TOP_RIGHT:
                // 右上に移動
                direction = new Vector2(1, 1);
                break;
            case MoveDirectionType.BOTTOM_RIGHT:
                // 右下に移動
                direction = new Vector2(1, -1);
                break;
            case MoveDirectionType.TOP_LEFT:
                // 左上に移動
                direction = new Vector2(-1, 1);
                break;
            case MoveDirectionType.BOTTOM_LEFT:
                // 左下に移動
                direction = new Vector2(-1, -1);
                break;
            case MoveDirectionType.NO_MOVE:
                // 移動しないに移動
                direction = Vector2.zero;
                break;
            default:
                Debug.Log("Error:MoveDirectionType定義値以外");
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
