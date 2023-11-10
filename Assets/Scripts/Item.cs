using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private int itemPoint = 10; // �A�C�e���̉��Z�|�C���g
    private GameObject uiManagerObj; // UiManager�̃I�u�W�F�N�g
    private UiManager uiManager; // UiManager�̃R���|�[�l���g
    // Start is called before the first frame update
    void Start()
    {
        // UiManager�̃I�u�W�F�N�g���擾����
        uiManagerObj = GameObject.FindGameObjectWithTag("UiManager");
        // UiManager�̃R���|�[�l���g���擾����B
        uiManager = uiManagerObj.GetComponent<UiManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerItemMove();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �X�R�A�̉��Z
            uiManager.SetScore(itemPoint);
            // Item�I�u�W�F�N�g���폜����
            Destroy(gameObject);
        }
    }

    private void PlayerItemMove()
    {
        float movePoint = 2.0f;
        float itemMoveSpeed = 1.5f;
        // �v���C���[�̃I�u�W�F�N�g���擾
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            // �v���C���[��Item�̋������v��
            float distance = Vector3.Distance(playerObj.transform.position, transform.position);
            // �v���C���[�ƃA�C�e���̊Ԋu������菬�����Ȃ�����
            if (distance < movePoint)
            {
                // �v���C���[�̕��Ɍ������Đi��
                Vector3 playerDistance = (playerObj.transform.position - transform.position).normalized;
                transform.position += playerDistance * itemMoveSpeed * Time.deltaTime;
            }
        }
    }

}
