using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpController : MonoBehaviour {

    GameObject snake;
    Food foodScript;
    GameObject foodManager;
    [SerializeField] AudioClip eat;

    private void Awake()
    {
        snake = GameObject.FindGameObjectWithTag("Player");
        print("Search player");
        foodManager = GameObject.Find("FoodController");
        foodScript = foodManager.GetComponent<Food>();
       
        
    }

    private void Update()
    {
        if (transform.position == snake.GetComponent<SnakeController>().Entities.First.Value.transform.position)
        {
            snake.GetComponent<SnakeController>().Expand();
            foodScript.isSpawned = false;
            AudioSource.PlayClipAtPoint(eat,gameObject.transform.position);
            Destroy(gameObject);
        }
        
    }
}
