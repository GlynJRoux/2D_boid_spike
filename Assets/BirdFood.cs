
using UnityEngine;

public class BirdFood : MonoBehaviour
{
    public int energy;
    public GameObject lastBird = null;

    // Use this for initialization
    void Start()
    {
        energy = UnityEngine.Random.Range(1, 20);
    }

    // Update is called once per frame
    void Update()
    {
        if (energy == 0) Destroy(this.gameObject);

    }



}



