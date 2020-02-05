using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject prefab;
        public int size;
    }

    #region Singleton
    //Shortcut singletonesque

    public static FoodPooler Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    //Dictionary/List<Pool> --> Queue-Pool
    //pools>poolDictionary --> objectPool-Pool
    //dictionary is a dictionary of pools.
    //pools is a list of objects.

    public void Start()
    {
        //create dictionary
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //Populate each pool
        foreach (Pool pool in pools)
        {
            //create the actual queue of GameObjects
            Queue<GameObject> objectPool = new Queue<GameObject>();
            //populate queue up to size
            for (int i = 0; i< pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            //add pool-queue to dictionary
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    public GameObject SpawnFromPool (string tag)
    {
        if(!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }


        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        
        //spawn
        objectToSpawn.SetActive(true);

        IPooledObject poolObj = objectToSpawn.GetComponent<IPooledObject>();
        if (poolObj != null)
        {
            poolObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public GameObject SpawnFromPoolActive(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with tag " + tag + " doesn't exist.");
            return null;
        }

        int loopcount = 0;
        Pool pooltag = null;
        foreach (Pool pool in pools)
        {
            if (pool.tag == tag)
                pooltag = pool;
        }
        if (pooltag != null)
        {
            while (loopcount < pooltag.size)
            {
                GameObject objectToSpawn = poolDictionary[tag].Dequeue();

                //spawn if the object is inactive
                if (!objectToSpawn.activeInHierarchy)
                {
                        objectToSpawn.SetActive(true);

                        IPooledObject poolObj = objectToSpawn.GetComponent<IPooledObject>();
                        if (poolObj != null)
                        {
                            poolObj.OnObjectSpawn();
                        }

                    Food foodscr = objectToSpawn.GetComponent<Food>();
                    Debug.Log("My State is: " + foodscr.foodState);

                    poolDictionary[tag].Enqueue(objectToSpawn);

                    return objectToSpawn;
                }
            }
        }

        return null;
        
    }

    /*
     * In this application, don't need to despawn.
     * Since spawning happens at the same time, the order of active/inactive objects doesn't matter.
     * Also, next time, use a dang stack.
    public void DespawnPool(GameObject obj)
    {

    }
    */

    /*NOTES:
     * When respawning, food keeps its prisoner state.
     * add 2 extra so that food doesn't disappear from player's hands.
     * 
     * didn't necessarily need the despawn, but now with the spawning, might as well cover your bases.
    */
}
