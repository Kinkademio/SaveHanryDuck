using GenerationFunction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public GenerationManager generationManager;
    public Direction direction;
    public bool firstDoor = false;
    int RoomNumInMemory = -1;

    //public bool bforward = false;
    //public bool bright = false;
    //public bool bleft = false;
    //public bool bdown = false;

    // Start is called before the first frame update
    void Start()
    {
        generationManager = GameObject.Find("Main Camera").GetComponent<GenerationManager>();

        //RoomNumInMemory = generationManager.SpawnRoom(new Pair<int, int>(0, 5), Direction.Top, 0);
        //generationManager.rooms.Add(new Room(new Pair<int, int>(0, 5), 0, Direction.Top, true));
        //generationManager.SpawnRoom(rooms[0], rooms[0].startDoorPoint, rooms[0].startDoorCoord, rooms[0].direction);
        // взять переменную скорости
        //script.Money = script.Money + 10;

        //if ((-x == startDoor.First) && (y == startDoor.Second))
        //{
        //    room.room.Second[i, j].GetComponent<CircleCollider2D>().enabled = false;
        //}
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Duck")
        {
            if (!firstDoor)
            {
                if (RoomNumInMemory == -1)
                {
                    int x = (int)this.transform.position.x; int y = (int)this.transform.position.y;
                    RoomNumInMemory = generationManager.SpawnRoom(new Pair<int, int>(x, y), direction, 1);
                    this.GetComponent<BoxCollider2D>().enabled = false;
                    //Destroy(this.gameObject.GetComponent<BoxCollider2D>());
                }
                else
                {
                    generationManager.ActiveRoom(generationManager.rooms[RoomNumInMemory]);
                }
            } 
            else
            {
                bool Close = false;
                switch (direction)
                {
                    case Direction.Top:
                        if (collision.transform.position.y < this.transform.position.y)
                        {
                            Close = true;
                        }
                        break;
                    case Direction.Left:
                        if (collision.transform.position.x > this.transform.position.x)
                        {
                            Close = true;
                        }
                        break;
                    case Direction.Right:
                        if (collision.transform.position.x < this.transform.position.x)
                        {
                            Close = true;
                        }
                        break;
                    case Direction.Bottom:
                        if (collision.transform.position.y > this.transform.position.y)
                        {
                            Close = true;
                        }
                        break;
                    default:
                        break;
                }

                if (Close) { this.gameObject.GetComponent<BoxCollider2D>().enabled = true; }
                else { this.gameObject.GetComponent<BoxCollider2D>().enabled = false; }
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Duck")
        {
            if (!firstDoor)
            {
                bool Destroy = false;

                switch (direction)
                {
                    case Direction.Top:
                        if (collision.transform.position.y > this.transform.position.y)
                        {
                            Destroy = true;
                        }
                        break;
                    case Direction.Left:
                        if (collision.transform.position.x < this.transform.position.x)
                        {
                            Destroy = true;
                        }
                        break;
                    case Direction.Right:
                        if (collision.transform.position.x > this.transform.position.x)
                        {
                            Destroy = true;
                        }
                        break;
                    case Direction.Bottom:
                        if (collision.transform.position.y < this.transform.position.y)
                        {
                            Destroy = true;
                        }
                        break;
                    default:
                        break;
                }

                if (Destroy == true)
                {
                    generationManager.DestroyAllWithout(RoomNumInMemory);
                    //this.gameObject.GetComponent<BoxCollider2D>().enabled = true;
                    //this.gameObject.AddComponent<BoxCollider2D>();
                    //AddComponent<BoxCollider2D>()
                }
                else
                {
                    generationManager.DisActiveRoom(generationManager.rooms[RoomNumInMemory]);
                }
            }
        }
    }

    // Update is called once per frame
    //void Update()
    //{

    //}
}
