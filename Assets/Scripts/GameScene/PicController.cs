using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PicController : MonoBehaviour
{
    const float SHOT_TIME = 0.3f;
    private Vector3 RIGHT_OFSET_POS = new Vector3(1, 0, 0);
    private Vector3 LEFT_OFSET_POS = new Vector3(-1, 0, 0);

    public enum POS_TYPE
    {
        RIGHT,
        LEFT,
    }

    public POS_TYPE posType;

    private Player playerScript = null;
    private Transform playerTransform;

    private GameObject[] enemys;

    private float smoothSpeed = 4.0f;
    private Vector3 targetPoition;

    // î≠éÀéûä‘
    private float shotTime = 0.0f;

    private float pickSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerScript = playerTransform.gameObject.GetComponent<Player>();

        transform.position = playerTransform.position + RIGHT_OFSET_POS;
    }

    private GameObject DistanceEnemyCheck()
    {
        if (enemys != null && enemys.Length > 0)
        {
            GameObject obj = null;
            float afterDistance = 10.0f;
            for (int i = 0; i < enemys.Length; i++)
            {
                MinionEnemy minion = enemys[i].GetComponent<MinionEnemy>();
                if (minion.GetEnemyGenInfo.enemyDirectionType == EnemyMove.MoveDirectionType.NO_MOVE) continue;
                
                float distance = Vector3.Distance(transform.position, enemys[i].transform.position);

                if (afterDistance > distance)
                {
                    afterDistance = distance;
                    obj = enemys[i];
                }
            }

            return obj;
        }
        else
        {
            enemys = GameObject.FindGameObjectsWithTag("Enemy");
        }

        return null;
    }

    // Update is called once per frame
    void Update()
    {
        if (posType == POS_TYPE.RIGHT)
        {
            targetPoition = playerTransform.position + RIGHT_OFSET_POS;
        }
        else
        {
            targetPoition = playerTransform.position + LEFT_OFSET_POS;
        }

        // åªç›ÇÃà íuÇ∆ñ⁄ïWÇÃà íuÇï‚ä‘
        transform.position = Vector3.Lerp(transform.position, targetPoition, smoothSpeed * Time.deltaTime);

        PickShot(transform);

        
    }

    // î≠éÀéûä‘
    bool ShotTimer()
    {
        bool shotFlag = false;

        shotTime -= Time.deltaTime;

        if (shotTime <= 0)
        {
            shotTime = SHOT_TIME;
            shotFlag = true;
        }

        return shotFlag;
    }

    private void PickShot(Transform transform)
    {
        if (ShotTimer())
        {
            Fire(transform);
        }
    }
    void Fire(Transform SpawnPoint)
    {
        GameObject bullet = BulletPool.Instance.GetPooledObject();

        GameObject enemyObj = DistanceEnemyCheck();

        if (bullet != null && enemyObj != null)
        {
            Vector2 directionToEnemy = (enemyObj.transform.position - transform.position).normalized;

            bullet.transform.position = SpawnPoint.position;
            bullet.transform.rotation = Quaternion.identity;
            bullet.SetActive(true);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = directionToEnemy * pickSpeed;
        }
    }
}
