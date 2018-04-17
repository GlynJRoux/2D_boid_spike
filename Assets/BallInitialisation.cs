
using UnityEngine;

public class BallInitialisation : MonoBehaviour
{

    public GameObject[] objects;
    public GameObject circlePrefab;
    public int randomobjectCounter;
	[Range(1,200)]
	public int spawnRange = 95;

    // Use this for initialization
    void Start()
    {
        randomobjectCounter = Random.Range(1, 20);

        objects = new GameObject[randomobjectCounter];
        for (int i = 0; i < randomobjectCounter; i++)
        {
			Vector3 circlePosition = new Vector3(Random.Range(-spawnRange,+spawnRange),
				Random.Range(-spawnRange, +spawnRange), 0);
            objects[i] = Instantiate(circlePrefab, this.transform.position + circlePosition, Quaternion.identity) as GameObject;

        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}