using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [SerializeField] Sprite spriteToUse;
    [SerializeField] int framesBetweenMoves;
    int frameCounter;

    LinkedList<Sprite> entities = new LinkedList<Sprite>();
    
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
            entities.AddFirst(spriteToUse);
        }
    }

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        if (frameCounter <= 0)
        {
            entities.RemoveLast();
            switch (playerDirection)
            {
                case Direction.up:
                    // Take the last block and put it above the first object
                    entities.AddFirst(spriteToUse);
                    break;

                case Direction.down:
                    // Take the last block and put it beneath the first object
                    break;

                case Direction.left:
                    // Take the last block and put it to the left of the first object
                    break;

                case Direction.right:
                    // Take the last block and put it to the right of the first object
                    break;
            }
            frameCounter = framesBetweenMoves;
        }
        else
        {
            frameCounter--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player")
        {
            entities.AddLast(spriteToUse);
        }
        else
        {
            print("Die");
        }
    }
}
