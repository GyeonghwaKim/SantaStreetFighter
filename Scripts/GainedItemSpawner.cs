using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GainedItemSpawner : MonoBehaviourPun
{
    public Transform target;//player
    private float relativeHeigth = 1.3f; //  y��
    private float zDistance = -1.0f;// z��
    private float xDistance = 1.0f; // x��
    float Speed = 7f;  //�ӵ�

    PresentBox present;

    GameObject tracing;
    PlayerMovement touchSled;

    public GameObject[] gainedItems;
    public int gainedItemMax = 5;
    public int gainedItemMin = 0;
    public int gainedItemCount;

    //public bool nullcheck;


    // Start is called before the first frame update
    void Start()
    {

        //present = GetComponent<PresentBox>();
        gainedItemCount = gainedItemMin;

        tracing = GameObject.Find("Object");
       
        
        touchSled = gameObject.GetComponent<PlayerMovement>();

        gainedItems = new GameObject[gainedItemMax];

        //nullcheck = false;

    }

    [PunRPC]
    public void gainedItemSpawn(GameObject gameObject) //PresentBox ��ũ��Ʈ�� ���
    {
        tracing = Instantiate(gameObject, transform.position, Quaternion.identity);//���Ʒ� �ι��� ���� �ٲ���!

        addItem(gameObject);

        //relativeHeigth += 0.4f;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < gainedItemMax; i++) //gainedItemCount->gainedItemMax
        {

            if (gainedItems[i] != null)
            {
                Vector3 newPos = target.position + new Vector3(0f, relativeHeigth + (0.4f * i), 0f);
                gainedItems[i].transform.position = Vector3.Lerp(gainedItems[i].transform.position, newPos, Time.deltaTime * Speed);
            }
        }

    }

    [PunRPC]
    public void addItem(GameObject gameObject)
    {
        gainedItems[gainedItemCount] = tracing;
        gainedItemCount += 1;
    }

    [PunRPC]
    public void LoseItem()
    {
        for (int i = 0; i < gainedItemMax; i++)
        {
            if (gainedItems[i] != null && !touchSled.touchSled)
                ItemSpawner.instance.UseSpawn();

            Destroy(gainedItems[i]);
            gainedItems[i] = null;

            gainedItemCount = 0;
        }
    }
}
