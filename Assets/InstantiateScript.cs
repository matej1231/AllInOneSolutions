using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateScript : MonoBehaviour
{
    int numberOfInstantiatedUnits = 2;
    float waitingTimeBetweenSpawning = 1f;

    public GameObject redBlock, blueBlock;
    public Transform objectSpawnerPos;

    #region ObjectPooling
    public static InstantiateScript instance;

    public Queue<GameObject> pooledGameObjectQueue = new Queue<GameObject>();
    #endregion

    bool firstSpawn = true;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        if (firstSpawn)
        {
            InstantiateNewGameObjects();
            firstSpawn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PoolOutGameObject();
        }
    }

    private void InstantiateNewGameObjects()
    {
        GameObject objectToPool;
        for (int i = 0; i < numberOfInstantiatedUnits; i++)
        {
            if (i % 2 == 0)
            {
                objectToPool = Instantiate(redBlock, objectSpawnerPos.position, Quaternion.identity);
                objectToPool.SetActive(false);
                pooledGameObjectQueue.Enqueue(objectToPool);
            }
            else
            {
                objectToPool = Instantiate(blueBlock, objectSpawnerPos.position, Quaternion.identity);
                objectToPool.SetActive(false);
                pooledGameObjectQueue.Enqueue(objectToPool);
            }
        }
    }

    public void StoreGameObject(GameObject storingGameObject)
    {
        pooledGameObjectQueue.Enqueue(storingGameObject);
        storingGameObject.SetActive(false);
    }

    IEnumerator SpawnTimer()
    {
        this.gameObject.transform.localPosition += new Vector3(0, (float)-0.5, 0);
        yield return new WaitForSeconds(waitingTimeBetweenSpawning);
        this.gameObject.transform.localPosition += new Vector3(0, (float)0.5, 0);
    }

    public void PoolOutGameObject()
    {
        GameObject gameObjectToPoolOut;

        if (pooledGameObjectQueue.Count != 0)
        {
            gameObjectToPoolOut = pooledGameObjectQueue.Dequeue();
            gameObjectToPoolOut.SetActive(true);
            gameObjectToPoolOut.transform.position = objectSpawnerPos.transform.position;
            StartCoroutine(SpawnTimer());
        }
        else
        {
            return;
        }
    }
}
