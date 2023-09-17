using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GenerationFunction
{
    public class Room
    {
        public Pair<int, int> startDoorPoint;
        public int startDoorCoord;
        Pair<int, int> centreDoorPoint;
        public Pair<int, int> sizeRoom;
        public Direction direction;
        int countDoor;
        public int z;
        public bool TriggerRoom;

        public bool active;
        public Pair<int[,], GameObject[,]> room;

        public Room(Pair<int, int> DoorPoint, int Layer, Direction Direction, bool Active)
        {
            startDoorPoint = DoorPoint;
            sizeRoom.First = UnityEngine.Random.Range(8, 14);
            sizeRoom.Second = UnityEngine.Random.Range(8, 14);
            startDoorCoord = UnityEngine.Random.Range(2, sizeRoom.Second - 2);
            //sizeRoom.First = 9;
            //sizeRoom.Second = 9;
            direction = InvertDirection(Direction);
            countDoor = UnityEngine.Random.Range(2, 5);
            z = Layer;

            active = Active;
            room.First = new int[sizeRoom.First, sizeRoom.Second];
            room.Second = new GameObject[sizeRoom.First, sizeRoom.Second];

            int Trigger = UnityEngine.Random.Range(0, 4);
            if (Trigger == 0) { TriggerRoom = true; }
            else { TriggerRoom = false; }

            GenerateRoomStartDoor();
        }

        void GenerateRoomStartDoor() //TODO: Генерация оставшихся дверей
        {
            int noPlacedDoor = countDoor;
            Pair<int, int> TriggerCoord = new Pair<int, int>(-1, -1);
            int BottomDoor = -1, LeftDoor = -1, RightDoor = -1;

            while (noPlacedDoor > 1)
            {
                switch(UnityEngine.Random.Range(0, 4))
                {
                    case 0:
                        BottomDoor = UnityEngine.Random.Range(2, sizeRoom.Second - 2);
                        noPlacedDoor -= 1;
                        break;
                    case 1:
                        LeftDoor = UnityEngine.Random.Range(2, sizeRoom.First - 2);
                        noPlacedDoor -= 1;
                        break;
                    case 2:
                        RightDoor = UnityEngine.Random.Range(2, sizeRoom.First - 2);
                        noPlacedDoor -= 1;
                        break;
                }

            }
            if (TriggerRoom)
            {
                TriggerCoord.First = UnityEngine.Random.Range(2, sizeRoom.Second - 2);
                TriggerCoord.Second = UnityEngine.Random.Range(2, sizeRoom.Second - 2);
            }



            for (int i = 0; i < sizeRoom.First; i++)
            {
                for (int j = 0; j < sizeRoom.Second; j++)
                {
                    if ((i == 0) && (j == startDoorCoord))
                    {
                        room.First[i, j] = 3;
                    } 
                    else if (i == 0)
                    {
                        room.First[i, j] = 2;
                    }
                    else if ((j == 0) && (i == LeftDoor))
                    {
                        room.First[i, j] = 3;
                    }
                    else if ((j == sizeRoom.Second - 1) && (i == RightDoor))
                    {
                        room.First[i, j] = 3;
                    }
                    else if ((j == 0) || (j == sizeRoom.Second - 1))
                    {
                        room.First[i, j] = 2;
                    } 
                    else if ((j == TriggerCoord.First) && (i == TriggerCoord.Second))
                    {
                        room.First[i, j] = 4;
                    }
                    else
                    {
                        room.First[i, j] = 1;
                    }

                    if ((i == sizeRoom.First - 1) && (j == BottomDoor))
                    {
                        room.First[i, j] = 3;
                    }
                    else if (i == sizeRoom.First - 1)
                    {
                        room.First[i, j] = 2;
                    }
                }
            }

        }

        Direction InvertDirection(Direction direction)
        {
            switch (direction)
            {
                case Direction.Top:
                    return Direction.Bottom;
                case Direction.Left:
                    return Direction.Right;
                case Direction.Right:
                    return Direction.Left;
                case Direction.Bottom:
                    return Direction.Top;
                default:
                    break;
            }
            return 0;
        }
    }

    //struct Door
    //{
    //    Direction direction;
    //    Pair<int, int> coord;
    //    GameObject door;
    //}

    public struct Pair<T, U>
    {
        public T First { get; set; }
        public U Second { get; set; }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        public override string ToString()
        {
            return $"({First}, {Second})";
        }
    };

    public enum Direction
    {
        Top,
        Left,
        Right,
        Bottom
    }
}
