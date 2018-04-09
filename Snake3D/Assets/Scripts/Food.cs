using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public GameObject FoodPrefab;

    public Transform topBorder;
    public Transform botBorder;
    public Transform leftBorder;
    public Transform rightBorder;

    [SerializeField] float time;
    [SerializeField] float repeatRate;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        InvokeRepeating("Spawn",time,repeatRate);
	}

    void Spawn()
    {
        int z = (int)Random.Range(botBorder.position.z,topBorder.position.z);

        int x = (int)Random.Range(leftBorder.position.x, rightBorder.position.x);

        Instantiate(FoodPrefab, new Vector3(x, 0, z), Quaternion.identity);
    }
}
