using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnableObjs : MonoBehaviour
{
    [SerializeField] List<GameObject> Points;
    [SerializeField] List<GameObject> Objects;
    [SerializeField] bool Spawn;

    // Start is called before the first frame update
    void Start()
    {
        if(Spawn)
        {
            SpawnObjs();
        }
    }

    private void SpawnObjs()
    {
        foreach(GameObject i in Objects)
        {
            int Point;
            Point = Random.Range(0, Points.Count);

            i.transform.position = Points[Point].transform.position;
            i.transform.rotation = Points[Point].transform.rotation;

            Destroy(Points[Point]);
            Points.Remove(Points[Point]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
