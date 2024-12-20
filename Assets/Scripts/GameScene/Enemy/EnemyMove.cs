using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    protected const float DESTROY_TOP_LINE = 6.0f;
    protected const float DESTROY_RIGHT_LINE = 4.5f;
    protected const float DESTROY_REFT_LINE = -4.5f;
    protected const float DESTROY_BOTTOM_LINE = -6.0f;

    // スタート時点の自分の位置
    protected Vector3 startPos;

    // Enemyに情報を渡す構造体
    public struct EnemyGenInfo
    {
        public EnumData.MoveDirectionType enemyDirectionType; // Enemyの方向
        public float firstSpeed; // スピードを１種類のみ設定する場合に利用
        public float secondSpeed; // スピードを２種類設定する場合に利用（firstSpeedはx軸、secondSpeedはy軸）
        public int shotPattern; // 攻撃パターン ： 配列に設定する番号を格納する
    }



    // methodをチェックする。-1:エラー、0:引数１つ 1:引数２つ 2:引数３つ
    public int CheckMethod(EnemyGenInfo enemyGenInfo)
    {
        if (enemyGenInfo.enemyDirectionType == EnumData.MoveDirectionType.NO_MOVE)
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
    public virtual void MoveDirection(EnumData.MoveDirectionType moveDirectionType)
    {
        // 引数のmoveDirectionTypeによる方向を取得
        Vector2 direction = GetDirectionVector(moveDirectionType);
        
        // 移動
        Move(direction.x, direction.y);
    }

    // キャラクターに移動させる（スピードは、引数に指定した値になる）
    public virtual void MoveDirection(EnumData.MoveDirectionType moveDirectionType, float speed)
    {
        // 引数のmoveDirectionTypeによる方向を取得
        Vector2 direction = GetDirectionVector(moveDirectionType);

        // 引数の値を絶対値にする
        float absSpeed = Mathf.Abs(speed);
        // 移動
        Move(direction.x * absSpeed, direction.y * absSpeed);
    }

    // キャラクターに移動させる（スピードは、x軸y軸それぞれ指定出来る）
    public virtual void MoveDirection(EnumData.MoveDirectionType moveDirectionType, float xSpeed, float ySpeed)
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
    private Vector2 GetDirectionVector(EnumData.MoveDirectionType directionType)
    {
        Vector2 direction = Vector2.zero;
        switch (directionType)
        {
            case EnumData.MoveDirectionType.TOP:
                // 上に移動
                direction = new Vector2(0, 1);
                break;
            case EnumData.MoveDirectionType.RIGHT:
                // 右に移動
                direction = new Vector2(1, 0);
                break;
            case EnumData.MoveDirectionType.BOTTOM:
                // 下に移動
                direction = new Vector2(0, -1);
                break;
            case EnumData.MoveDirectionType.LEFT:
                // 左に移動
                direction = new Vector2(-1, 0);
                break;
            case EnumData.MoveDirectionType.TOP_RIGHT:
                // 右上に移動
                direction = new Vector2(1, 1);
                break;
            case EnumData.MoveDirectionType.BOTTOM_RIGHT:
                // 右下に移動
                direction = new Vector2(1, -1);
                break;
            case EnumData.MoveDirectionType.TOP_LEFT:
                // 左上に移動
                direction = new Vector2(-1, 1);
                break;
            case EnumData.MoveDirectionType.BOTTOM_LEFT:
                // 左下に移動
                direction = new Vector2(-1, -1);
                break;
            case EnumData.MoveDirectionType.NO_MOVE:
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

    public virtual void Start()
    {
        startPos = this.transform.position;
    }

    public virtual void Update()
    {
        //DestroyEnemy();
    }

    //private void DestroyEnemy()
    //{
    //    if (this.transform.position.x > DESTROY_RIGHT_LINE ||
    //        this.transform.position.x < DESTROY_REFT_LINE ||
    //        this.transform.position.y > DESTROY_TOP_LINE ||
    //        this.transform.position.y < DESTROY_BOTTOM_LINE)
    //    {

    //        this.transform.position = startPos;
    //        MoveDirection(MoveDirectionType.NO_MOVE);
    //        //Destroy(this.gameObject);
    //    }
    //}

}
