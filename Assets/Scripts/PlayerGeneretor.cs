using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGeneretor : MonoBehaviour
{
    // �v���C���[�̎c�@��\��UI�̃I�u�W�F�N�g
    [SerializeField] private GameObject playerLife1, playerLife2;
    // �v���C���[�̃I�u�W�F�N�g
    [SerializeField] private GameObject playerObj;
    // �v���C���[�̐����ʒu
    private Vector3 playerPosition = new Vector3(0,-4,0);
    // �v���C���[�̃��C�t�|�C���g
    private int playerLifePoint;

    // Start is called before the first frame update
    void Start()
    {
        playerLifePoint = 3;
        playerLife1.SetActive(true);
        playerLife2.SetActive(true);
    }

    public void ReducePlayerLife(int value)
    {
        playerLifePoint -= value;
        DelPlayerIcon(playerLifePoint);
    }

    private void DelPlayerIcon(int value)
    {
        switch (value)
        {
            case 0:
                // �Q�[���I�[�o�[����
                Debug.Log("�Q�[���I�[�o�[");
                break;
            case 1:
                playerLife1.SetActive(false);
                RegenerationPlayer();
                break;
            case 2:
                playerLife2.SetActive(false);
                RegenerationPlayer();
                break;
            default:
                break;
        }
    }

    private void RegenerationPlayer()
    {
        Instantiate(playerObj, playerPosition, Quaternion.identity);
    }
}

//------------------------
