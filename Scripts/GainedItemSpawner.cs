using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GainedItemSpawner : MonoBehaviourPun
{
    public Transform target;//player
    private float relativeHeigth = 1.3f; //  y값
    private float zDistance = -1.0f;// z값
    private float xDistance = 1.0f; // x값
    float Speed = 7f;  //속도

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
    public void gainedItemSpawn(GameObject gameObject) //PresentBox 스크립트에 사용
    {
        tracing = Instantiate(gameObject, transform.position, Quaternion.identity);//위아래 두문장 순서 바꿔줌!

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
