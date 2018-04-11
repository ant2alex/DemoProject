using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

    GameObject snake;

    private void Awake()
    {
        snake = GameObject.FindGameObjectWithTag("Player");
        print("Search player");
    }

    private void Update()
    {
        if (transform.position == snake.GetComponent<SnakeController>().Entities.First.Value.transform.position)
        {
            snake.GetComponent<SnakeController>().Expand();
            Destroy(gameObject);
        }
    }
}
