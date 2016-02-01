using UnityEngine;
using System.Collections;

public class Rain : MonoBehaviour
{
    public  Transform[] clouds;

    private Bounds cloudBounds;

	void Start ()
    {
        cloudBounds = GetComponent<BoxCollider2D>().bounds;
        GenCloudFormation();
	}

    void Update()
    {

    }
	
	void GenCloudFormation()
    {
        foreach(var cloud in clouds)
        {
            float x = Random.Range(cloudBounds.min.x, cloudBounds.max.x);
            float y = Random.Range(cloudBounds.min.y, cloudBounds.max.y);
            Vector3 pos = new Vector3(x, y, 0);
            cloud.transform.position = pos;
        }
	}
}
