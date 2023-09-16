using System;
using System.Collections;
using System.Collections.Generic;
using GenerationFunction;
using UnityEngine;

public class GenerationManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject MainCamera;

    public List<Room> rooms = new List<Room>();

    public GameObject door;
    public GameObject floor;
    public GameObject wall;


    //Room room1;
    //GameObject door = Resources.Load("Door", typeof(GameObject)) as GameObject;
    //GameObject floor = Resources.Load("Floor", typeof(GameObject)) as GameObject;
    //GameObject wall = Resources.Load("Wall", typeof(GameObject)) as GameObject;
    //short roomPosX, roomPosY;
    //int[,] Room = { 
    //    { 0, 2, 2, 2, 3, 2, 2, 2, 0 }, // 0 - пустота, 1 - пол, 2 - стена, 3 - дверь
    //    { 0, 2, 1, 1, 1, 1, 1, 2, 0 },
    //    { 0, 2, 1, 1, 1, 1, 1, 2, 0 },
    //    { 0, 2, 1, 1, 1, 1, 1, 2, 0 },
    //    { 0, 2, 1, 1, 1, 1, 1, 2, 0 },
    //    { 0, 2, 1, 1, 1, 1, 1, 2, 0 },
    //    { 0, 2, 1, 1, 1, 1, 1, 2, 0 },
    //    { 0, 2, 1, 1, 1, 1, 1, 2, 0 },
    //    { 0, 2, 2, 2, 3, 2, 2, 2, 0 }
    //};

    void Start()
    {
        int x = 0; int y = -2;
        SpawnRoom(new Pair<int, int>(x, y), Direction.Top, 0);
        //Player = GameObject.Find("Duck");
        Player.SetActive(true);
        MainCamera = GameObject.Find("Main Camera");
    }

    //    void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.E))
    //    {
    //        RoomGenerate();
    //        SpawnRoom(rooms[0], rooms[0].startDoorPoint, rooms[0].startDoorCoord, rooms[0].direction);
    //        SpawnRoom(rooms[1], rooms[1].startDoorPoint, rooms[1].startDoorCoord, rooms[1].direction);
    //        SpawnRoom(rooms[2], rooms[2].startDoorPoint, rooms[2].startDoorCoord, rooms[2].direction);
    //        SpawnRoom(rooms[3], rooms[3].startDoorPoint, rooms[3].startDoorCoord, rooms[3].direction);
    //    }
    //    if (Input.GetKeyDown(KeyCode.R))
    //    {
    //        if (rooms[0].active)
    //        { 
    //            DisActiveRoom(rooms[0]);
    //        } 
    //        else
    //        {
    //            ActiveRoom(rooms[0]);
    //        }
    //    }
    //}

    //void RoomGenerate()
    //{
    //    //высота от 6 до 9
    //    //ширина от 6 до 9
    //    //Room Temp = new Room(new Pair<int, int>(1, 1), 0, Direction.Top);
    //    int x = 5; int y = -10;

    //    rooms.Add(new Room(new Pair<int, int>(x, y), 0, Direction.Left, true));
    //    rooms.Add(new Room(new Pair<int, int>(x, y), 0, Direction.Right, true));
    //    rooms.Add(new Room(new Pair<int, int>(x, y), 0, Direction.Top, true));
    //    rooms.Add(new Room(new Pair<int, int>(x, y), 0, Direction.Bottom, true));
    //}

    public void DestroyAllWithout(int index)
    {
        for(int i = rooms.Count - 1; i >= 0; i--)
        {
            if (i != index)
            {
                DestroyRoom(i);
            }
        }

        float Dx = rooms[0].room.Second[0, 0].transform.position.x;
        float Dy = rooms[0].room.Second[0, 0].transform.position.y;

        for (int i = 0; i < rooms[0].sizeRoom.First; i++)
        {
            for (int j = 0; j < rooms[0].sizeRoom.Second; j++)
            {
                rooms[0].room.Second[i, j].transform.position = new Vector3
                    (rooms[0].room.Second[i, j].transform.position.x - Dx,
                     rooms[0].room.Second[i, j].transform.position.y - Dy, 0);
            }
        }
        Player.transform.position = new Vector3(Player.transform.position.x - Dx, Player.transform.position.y - Dy, -1);
        MainCamera.transform.position = new Vector3
            (MainCamera.transform.position.x - Dx, 
             MainCamera.transform.position.y - Dy, 
             MainCamera.transform.position.z);
    }

    public int SpawnRoom(Pair<int, int> startDoor, Direction direction, int z)
    {
        Room room = new Room(new Pair<int, int>(startDoor.First, startDoor.Second), z, direction, true);
        int startDoorCoord = room.startDoorCoord;
        rooms.Add(room);
        direction = room.direction;

        if (Direction.Top == direction) {
            for (int i = 0; i < room.sizeRoom.First; i++) {
                for (int j = 0; j < room.sizeRoom.Second; j++) {
                    int x = j + startDoor.First - startDoorCoord;
                    int y = i - startDoor.Second;

                    switch (room.room.First[i, j]) {
                        case 1:
                            room.room.Second[i, j] = Instantiate(floor, new Vector3(x, -y, z), Quaternion.identity);
                            break;
                        case 2:
                            room.room.Second[i, j] = Instantiate(wall, new Vector3(x, -y, z), Quaternion.identity);
                            break;
                        case 3:
                            room.room.Second[i, j] = Instantiate(door, new Vector3(x, -y, z), Quaternion.identity);
                            room.room.Second[i, j].GetComponent<DoorTrigger>().direction = DoorDirection(direction, i, j, room.sizeRoom);

                            if ((x == startDoor.First) && (-y == startDoor.Second))
                            {
                                //room.room.Second[i, j].GetComponent<CircleCollider2D>().enabled = false;
                                room.room.Second[i, j].GetComponent<DoorTrigger>().firstDoor = true;
                            }
                            break;
                    }
                }
            }
        } else if (Direction.Bottom == direction) {
            for (int i = 0; i < room.sizeRoom.First; i++) {
                for (int j = 0; j < room.sizeRoom.Second; j++) {
                    int x = j + startDoor.First - startDoorCoord;
                    int y = i + startDoor.Second;

                    switch (room.room.First[i, j]) {
                        case 1:
                            room.room.Second[i, j] = Instantiate(floor, new Vector3(x, y, z), Quaternion.identity);
                            break;
                        case 2:
                            room.room.Second[i, j] = Instantiate(wall, new Vector3(x, y, z), Quaternion.identity);
                            break;
                        case 3:
                            room.room.Second[i, j] = Instantiate(door, new Vector3(x, y, z), Quaternion.identity);
                            room.room.Second[i, j].GetComponent<DoorTrigger>().direction = DoorDirection(direction, i, j, room.sizeRoom);

                            if ((x == startDoor.First) && (y == startDoor.Second))
                            {
                                //room.room.Second[i, j].GetComponent<CircleCollider2D>().enabled = false;
                                room.room.Second[i, j].GetComponent<DoorTrigger>().firstDoor = true;
                            }
                            break;
                    }
                }
            }
        }
        else if (Direction.Left == direction)
        {
            for (int i = 0; i < room.sizeRoom.First; i++)
            {
                for (int j = 0; j < room.sizeRoom.Second; j++)
                {
                    int x = i + startDoor.First;
                    int y = j + startDoor.Second - startDoorCoord;

                    switch (room.room.First[i, j])
                    {
                        case 1:
                            room.room.Second[i, j] = Instantiate(floor, new Vector3(x, y, z), Quaternion.identity);
                            break;
                        case 2:
                            room.room.Second[i, j] = Instantiate(wall, new Vector3(x, y, z), Quaternion.identity);
                            break;
                        case 3:
                            room.room.Second[i, j] = Instantiate(door, new Vector3(x, y, z), Quaternion.identity);
                            room.room.Second[i, j].GetComponent<DoorTrigger>().direction = DoorDirection(direction, i, j, room.sizeRoom);

                            if ((x == startDoor.First) && (y == startDoor.Second))
                            {
                                //room.room.Second[i, j].GetComponent<CircleCollider2D>().enabled = false;
                                room.room.Second[i, j].GetComponent<DoorTrigger>().firstDoor = true;
                            }
                            break;
                    }
                }
            }
        }
        else if (Direction.Right == direction)
        {
            for (int i = 0; i < room.sizeRoom.First; i++)
            {
                for (int j = 0; j < room.sizeRoom.Second; j++)
                {
                    int x = i - startDoor.First;
                    int y = j + startDoor.Second - startDoorCoord;

                    switch (room.room.First[i, j])
                    {
                        case 1:
                            room.room.Second[i, j] = Instantiate(floor, new Vector3(-x, y, z), Quaternion.identity);
                            break;
                        case 2:
                            room.room.Second[i, j] = Instantiate(wall, new Vector3(-x, y, z), Quaternion.identity);
                            break;
                        case 3:
                            room.room.Second[i, j] = Instantiate(door, new Vector3(-x, y, z), Quaternion.identity);
                            room.room.Second[i, j].GetComponent<DoorTrigger>().direction = DoorDirection(direction, i, j, room.sizeRoom);

                            if ((-x == startDoor.First) && (y == startDoor.Second))
                            {
                                //room.room.Second[i, j].GetComponent<CircleCollider2D>().enabled = false;
                                room.room.Second[i, j].GetComponent<DoorTrigger>().firstDoor = true;
                            }
                            break;
                    }
                }
            }
        }

        return rooms.Count - 1;
    }

    Direction DoorDirection(Direction direction, int i, int j, Pair<int, int> sizeRoom)
    {
        switch (direction) {
            case Direction.Top:
                if(i == 0)
                {
                    return Direction.Top;
                }
                if (i == sizeRoom.First - 1)
                {
                    return Direction.Bottom;
                }
                if (j == 0)
                {
                    return Direction.Left;
                }
                if (j == sizeRoom.Second - 1)
                {
                    return Direction.Right;
                }
                break;
            case Direction.Left:
                if (i == 0)
                {
                    return Direction.Left; // Top
                }
                if (i == sizeRoom.First - 1)
                {
                    return Direction.Right; // Bottom
                }
                if (j == 0)
                {
                    return Direction.Bottom; // Left
                }
                if (j == sizeRoom.Second - 1)
                {
                    return Direction.Top; // Right
                }
                break;
            case Direction.Right:
                if (i == 0)
                {
                    return Direction.Right; // Top
                }
                if (i == sizeRoom.First - 1)
                {
                    return Direction.Left; // Bottom
                }
                if (j == 0)
                {
                    return Direction.Bottom; // Right
                }
                if (j == sizeRoom.Second - 1)
                {
                    return Direction.Top; // Left
                }
                break;
            case Direction.Bottom:
                if (i == 0)
                {
                    return Direction.Bottom;
                }
                if (i == sizeRoom.First - 1)
                {
                    return Direction.Top;
                }
                if (j == 0)
                {
                    return Direction.Left;
                }
                if (j == sizeRoom.Second - 1)
                {
                    return Direction.Right;
                }
                break;
            default:
                break;

        }
        return direction;
    }

    public void DestroyRoom(int index)
    {
        for (int i = 0; i < rooms[index].sizeRoom.First; i++)
        {
            for (int j = 0; j < rooms[index].sizeRoom.Second; j++)
            {
                Destroy(rooms[index].room.Second[i, j]);
            }
        }
        rooms.Remove(rooms[index]);
    }
    public void DisActiveRoom(Room room)
    {
        for (int i = 0; i < room.sizeRoom.First; i++)
        {
            for (int j = 0; j < room.sizeRoom.Second; j++)
            {
                room.room.Second[i, j].SetActive(false);
            }
        }
        room.active = false;
    }
    public void ActiveRoom(Room room)
    {
        for (int i = 0; i < room.sizeRoom.First; i++)
        {
            for (int j = 0; j < room.sizeRoom.Second; j++)
            {
                room.room.Second[i, j].SetActive(true);
            }
        }
        room.active = true;
    }

}

