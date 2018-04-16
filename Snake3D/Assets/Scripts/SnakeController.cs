using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour {

    public LinkedList<GameObject> Entities
    {
        get
        {
            return entities;
        }
    }

    [SerializeField] GameObject snakeEntity;
    [SerializeField] int framesBetweenMoves;
    int frameCounter;
    GameObject newTail;
    GameObject newHead;
    private Camera cam;

    LinkedList<GameObject> entities = new LinkedList<GameObject>();

    public enum Direction
    {
        left,
        right,
        up,
        down
    };

    Direction playerDirection = Direction.right;

    private void Awake()
    {
        if (transform.childCount <= 0)
        {
            GameObject newPart = Instantiate(snakeEntity, transform.position, transform.rotation);
            newPart.transform.parent = gameObject.transform;
            entities.AddFirst(newPart);
        }
        else
        {
            entities.AddFirst(transform.GetChild(0).gameObject);
        }
        cam = Camera.main;
    }

    void CheckIfEatingSelf()
    {
        foreach(GameObject obj in entities)
        {
            if(entities.First.Value.gameObject != obj && obj.GetComponent<MeshRenderer>().enabled == true)
            {
                if (entities.First.Value.gameObject.transform.position == obj.transform.position)
                {
                    Die();
                }
            }
        }
    }

    void Die()
    {
        print("Die");
    }
    
    private void Update()
    {
        CheckIfEatingSelf();
        ReadInput();
        if (frameCounter <= 0)
        {
            // Add a new entity as a head
            newHead = Instantiate(snakeEntity, entities.First.Value.gameObject.transform.position, transform.rotation);
            newHead.transform.parent = gameObject.transform;
            entities.AddFirst(newHead);

            // Remove the last tail entity
            Destroy(entities.Last.Value.gameObject);
            entities.RemoveLast();

            Vector3 oldPos = entities.Last.Value.gameObject.transform.position;
            //newHead.transform.position += newHead.transform.forward;
            switch (playerDirection)
            {
                case Direction.up:
                    newHead.transform.position += Vector3.forward;
                    break;

                case Direction.down:
                    newHead.transform.position += Vector3.back;
                    break;

                case Direction.left:
                    newHead.transform.position += Vector3.left;
                    break;

                case Direction.right:
                    newHead.transform.position += Vector3.right;
                    break;
            }
            frameCounter = framesBetweenMoves;
        }
        else
        {
            frameCounter--;
        }

        print(newHead.transform.position.x);
        print(newTail.transform.position.x);
        //print(newHead.transform.position.y);
        
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            //entities.First.Value.gameObject.transform.Rotate(new Vector3(0, 90));
            switch (playerDirection)
            {
                case Direction.right:
                    playerDirection = Direction.down;
                    break;

                case Direction.down:
                    playerDirection = Direction.left;
                    break;

                case Direction.left:
                    playerDirection = Direction.up;
                    break;

                case Direction.up:
                    playerDirection = Direction.right;
                    break;
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //entities.First.Value.gameObject.transform.Rotate(new Vector3(0, -90));
            switch (playerDirection)
            {
                case Direction.right:
                    playerDirection = Direction.up;
                    break;

                case Direction.down:
                    playerDirection = Direction.right;
                    break;

                case Direction.left:
                    playerDirection = Direction.down;
                    break;

                case Direction.up:
                    playerDirection = Direction.left;
                    break;
            }
        }
    }

    public void Expand()
    {
        newTail = Instantiate(snakeEntity, entities.First.Value.gameObject.transform.position, transform.rotation);
        newTail.transform.parent = gameObject.transform;
        newTail.GetComponent<MeshRenderer>().enabled = false;
        entities.AddLast(newTail);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag != "Player")
    //    {
    //        print("hit shit");
    //        GameObject newTail = Instantiate(snakeEntity, transform.position, transform.rotation);
    //        newTail.transform.parent = gameObject.transform;
    //        entities.AddLast(newTail);
    //        Destroy(other.gameObject);
    //    }
    //    else
    //    {
    //        print("Die");
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        print("triggered");
    }

}
