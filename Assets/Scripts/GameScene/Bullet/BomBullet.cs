using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomBullet : MonoBehaviour
{
    private const int BOM_COUNT = 12;

    public GameObject hahen;

    private List<GameObject> hahenObjList = new List<GameObject>();

    private bool moveStop = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BomStart());
    }

    private void Update()
    {
        if (moveStop) return;
        transform.position += new Vector3(0, 1.0f * Time.deltaTime, 0);
    }

    IEnumerator BomStart()
    {
        yield return new WaitForSeconds(2.0f);

        Bom();

        yield return new WaitForSeconds(1.0f);

        foreach (GameObject obj in hahenObjList)
        {
            Destroy(obj);
        }

        Destroy(this.gameObject);
    }

    void Bom()
    {
        moveStop = true;

        float angleStep = 360f / BOM_COUNT;

        for (int i = 0; i < BOM_COUNT; i++)
        {
            if (hahen)
            {
                GameObject instance = Instantiate(hahen, transform.position, Quaternion.identity);
                instance.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                hahenObjList.Add(instance);

                Rigidbody2D rb = instance.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.drag = 4.5f;

                    // �p�x���v�Z�i���W�A���P�ʂɕϊ��j
                    float angle = i * angleStep * Mathf.Deg2Rad;
                    // �P�ʉ~��̓_���v�Z�iX��Y�j
                    Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));

                    // ���x�̑傫���𒲐����ĕ����ɏ�Z
                    Vector2 force = direction * 300f; // ���x�̑傫���͓K�X�������Ă�������
                    rb.AddForce(force);
                }
            }
        }
    }
}