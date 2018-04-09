using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] GameObject snakeEntity;
    [SerializeField] int framesBetweenMoves;
    int frameCounter;

    bool bGrow = false;

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
        if (entities.Count <= 0)
        {
            entities.AddFirst(snakeEntity);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            switch(playerDirection)
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
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
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

    private void LateUpdate()
    {
        if (frameCounter <= 0)
        {
            entities.RemoveLast();
            var newFirst = entities.AddFirst(snakeEntity);
            newFirst.Value.transform.position = entities.First.Value.transform.position;
            Vector3 oldPos = entities.Last.Value.gameObject.transform.position;
            switch (playerDirection)
            {
                case Direction.up:
                    // Take the last block and put it above the first object
                    newFirst.Value.transform.position += Vector3.forward;
                    break;

                case Direction.down:
                    // Take the last block and put it beneath the first object
                    newFirst.Value.transform.position += Vector3.back;
                    break;

                case Direction.left:
                    // Take the last block and put it to the left of the first object
                    newFirst.Value.transform.position += Vector3.left;
                    break;

                case Direction.right:
                    // Take the last block and put it to the right of the first object
                    newFirst.Value.transform.position += Vector3.right;
                    break;
            }
            if(bGrow)
            {
                GameObject newTail = Instantiate(snakeEntity, new Vector3(oldPos.x, 0f, oldPos.z), entities.Last.Value.transform.rotation);
                entities.AddLast(newTail);
                bGrow = false;
            }
            frameCounter = framesBetweenMoves;
        }
        else
        {
            frameCounter--;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Player")
        {
            //Vector3 tailPosition = entities.Last.Value.transform.position;
            //var newTail = entities.AddLast(snakeEntity);
            //newTail.Value.transform.position = tailPosition;
            bGrow = true;
            Destroy(other);
        }
        else
        {
            print("Die");
        }
    }
}
