﻿using System.Collections;
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
        entities.AddFirst(gameObject);
        frameCounter = framesBetweenMoves;
    }

    private void Update()
    {
        ReadInput();
    }

    private void ReadInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
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
            // Add a new entity as a head
            GameObject newHead = Instantiate(snakeEntity, entities.First.Value.transform.position, entities.First.Value.transform.rotation);
            entities.AddFirst(newHead);

            // Remove the last tail entity
            Destroy(entities.Last.Value.gameObject);
            entities.RemoveLast();

            Vector3 oldPos = entities.Last.Value.gameObject.transform.position;
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
            if (bGrow)
            {
                GameObject newTail = Instantiate(snakeEntity, new Vector3(oldPos.x - 1, 0f, oldPos.z), entities.Last.Value.transform.rotation);
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
            Destroy(other.gameObject);
        }
        else
        {
            print("Die");
        }
    }
}
