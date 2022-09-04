using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] areaPrefabs;
    [SerializeField]
    private int spawnAreaCountAtStart = 10;
    [SerializeField]
    private float zDistance = 6;
    private int areaIndex = 0;

    [SerializeField]
    private Transform playerTransform;

    private void Awake()
    {
        for (int i = 0; i < spawnAreaCountAtStart; ++ i)
        {
            if (i <= 1)
            {
                SpawnArea(false);
            }
            else
            {
                SpawnArea();
            }
        }
    }

    public void SpawnArea(bool isRandom = true)
    {
        GameObject clone = null;

        if (isRandom == false)
        {
            clone = Instantiate(areaPrefabs[0]);
        }
        else
        {
            int index = Random.Range(1, areaPrefabs.Length);
            clone = Instantiate(areaPrefabs[index]);
        }

        clone.transform.position = new Vector3(0, 0, areaIndex * zDistance);
        clone.GetComponent<Area>().Setup(this, playerTransform);

        areaIndex ++;
    }
}
