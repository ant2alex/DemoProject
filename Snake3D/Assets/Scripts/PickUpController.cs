using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

    [SerializeField] GameObject snake;

    //private void Awake()
    //{
    //    GameObject.FindGameObjectWithTag("Player");
    //}

    private void Update()
    {
        if (transform.position == snake.GetComponent<SnakeController>().Entities.First.Value.transform.position)
        {
            print("Expand");
            snake.GetComponent<SnakeController>().Expand();
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    print("Expand");
    //    snake.GetComponent<SnakeController>().Expand();
    //}
}
