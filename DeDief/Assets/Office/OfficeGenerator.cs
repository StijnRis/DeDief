using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class OfficeGenerator : MonoBehaviour
{
    public BoxCollider Box;
    [Range(0.0f, 1.0f)]
    public float MaxHallRate = 0.15f;
    public float HallSize = 1.2f;
    public GameObject Corridor;
    public GameObject Light;
    public List<RoomType> RoomTypes;

    private Area House;
    private float TotalHallArea;
    private float TotalRoomArea;
    private List<Area> Chunks, Halls, Blocks, UnreachableAreas, Areas;
    private List<GameObject> Rooms;

    private IDictionary<RoomType, float> Coverage;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        RoomTypes = RoomTypes.OrderBy(x => -x.MinLength).ToList();
        // Remove and reset everyting
        foreach (Transform c in transform)
        {
            Destroy(c.gameObject);
        }
        Rooms = new List<GameObject>();
        TotalHallArea = 0;
        House = new Area(-Box.size.x / 2, -Box.size.z / 2, Box.size.x / 2, Box.size.z / 2);
        Chunks = new List<Area>();
        Halls = new List<Area>();
        Blocks = new List<Area>();
        UnreachableAreas = new List<Area>();
        Areas = new List<Area>();

        // Generate office
        ChunksToBlocks();
        BlocksToAreas();
        AddDoors();

        foreach (Area area in Areas)
        {
            PlaceRoom(area);
        }
        foreach (Area hall in Halls)
        {
            PlaceArea(hall, Corridor);
        }
    }

    private void ChunksToBlocks()
    {
        Chunks.Add(House);
        while ((Chunks.Count > 0) && (TotalHallArea / (double) House.GetArea() < MaxHallRate))
        {
            Area chunk = Chunks.Max();
            Chunks.Remove(chunk);


            (Area chunk_a, Area hall, Area chunk_b) = chunk.SplitThree(HallSize);
            Chunks.Add(chunk_a);
            Chunks.Add(chunk_b);
            Halls.Add(hall);
            TotalHallArea += (float) hall.GetArea();
        }
        Blocks.AddRange(Chunks);
        Chunks.Clear();
    }

    private void BlocksToAreas()
    {
        TotalRoomArea = Box.size.x * Box.size.z - TotalHallArea;
        Coverage = new Dictionary<RoomType, float>();
        foreach (RoomType roomType in RoomTypes)
        {
            Coverage.Add(roomType, 0);
        }


        while (Blocks.Count > 0)
        {
            Area block = Blocks.Max();
            Blocks.Remove(block);
            (bool split, RoomType roomType) = WantSplitBlock(block);

            if (split)
            {
                (Area block_a, Area block_b) = block.SplitTwo();
                Blocks.Add(block_a);
                Blocks.Add(block_b);
            }
            else
            {
                Debug.Log("Place "+block.getSizeString() + " for "+roomType.RoomPrefab.name);
                Coverage[roomType] += (float) block.GetArea();
                UnreachableAreas.Add(block);
                block.roomType = roomType;
            }
        }
    }

    private (bool, RoomType) WantSplitBlock(Area block)
    {
        List<RoomType> options = new List<RoomType>();
        bool toBig = false;
        foreach (var coverage in Coverage)
        {
            bool minFits = block.GetWidth() >= coverage.Key.MinLength && block.GetLength() >= coverage.Key.MinLength;
            bool maxFits = block.GetWidth() <= coverage.Key.MaxLength && block.GetLength() <= coverage.Key.MaxLength;
            if (minFits && maxFits)
            {
                options.Add(coverage.Key);
            } else if (minFits)
            {
                toBig = true;
            }
        }

        if (options.Count == 0)
        {
            if (toBig)
            {
                return (true, null);
            }
            return (false, null);
        }

        foreach (RoomType roomType in options) { 
            float percentage = Coverage[roomType] / TotalRoomArea;
            
            if (percentage < roomType.MinPercentageOccurrences)
            {
                float minLength = Mathf.Max(1, roomType.MinLength);
                float maxLength = Mathf.Max((float) block.GetWidth(), (float) block.GetLength());
                if (maxLength > minLength * 2)
                {
                    float change = Random.Range(0, maxLength);
                    if (change <= minLength)
                    {
                        return (false, roomType);
                    }
                    else
                    {
                        return (true, null);
                    }
                }
                else
                {
                    return (false, roomType);
                }
            }
        }

        Debug.Log(block.getSizeString());
        if (toBig)
        {
            return (true, null);
        } else
        {
            return (false, options[0]);
        }

        /*foreach (RoomType roomType in options)
        {
            Coverage[roomType] *= 0.5f;
        }*/

        /*return (false, options[0]);*/
    }

    public void AddDoors()
    {
        while (UnreachableAreas.Count > 0)
        {
            Area area = UnreachableAreas.First();
            UnreachableAreas.Remove(area);
            bool connected = false;
            foreach (Area hall in Halls)
            {
                if (hall.IsTouching(area))
                {
                    Area.createDoorBetween(area, hall);
                    connected = true;
                    break;
                }
            }
            if (!connected)
            {
                foreach (Area areaTest in Areas)
                {
                    if (areaTest.IsTouching(area))
                    {
                        Area.createDoorBetween(area, areaTest);
                        connected = true;
                        break;
                    }
                }
            }
            
            if (!connected)
            {
                UnreachableAreas.Add(area);
            } else
            {
                Areas.Add(area);
            }
        }
    }

    public void PlaceRoom(Area area)
    {
        GameObject roomPrefab = area.roomType.RoomPrefab;
        PlaceArea(area, roomPrefab);
    }

    public void PlaceArea(Area area, GameObject roomPrefab)
    {
        GameObject room = Instantiate(roomPrefab, transform);
        Vector3 offset = new Vector3((float)(area.Left + area.GetWidth() / 2), 0, (float)(area.Top + area.GetLength() / 2));

        Quaternion rotation = new Quaternion();
        Quaternion rotationDoor = new Quaternion();
        if (area.Doors.Count > 0)
        {
            Area bestDoor = area.Doors[0];
            Vector3 middleDoor = ((new Vector3(bestDoor.Left, 0, bestDoor.Top) - offset) + (new Vector3(bestDoor.Right, 0, bestDoor.Bottom) - offset)) / 2;
            rotation.SetLookRotation(middleDoor);
            rotation = Quaternion.Euler(rotation.x, Mathf.Round(rotation.eulerAngles.y / 90) * 90f, rotation.z);
            rotationDoor = Quaternion.Euler(-rotation.eulerAngles);
        }
        

        BoxCollider box = room.GetComponent<BoxCollider>();
        foreach (Area doorArea in area.Doors) { 
            Door doorObject = room.AddComponent<Door>();
            doorObject.setPosition(rotationDoor * (new Vector3(doorArea.Left, 0, doorArea.Top) - offset), rotationDoor * (new Vector3(doorArea.Right, 0, doorArea.Bottom) - offset));
        }
        
        Vector3 size = rotation * new Vector3((float) area.GetWidth(), Box.size.y, (float) area.GetLength());
        size = new Vector3(Mathf.Abs(size.x), Mathf.Abs(size.y), Mathf.Abs(size.z));
        box.size = size;
        room.transform.SetParent(transform);
        room.transform.localPosition = new Vector3((float)(area.Left + area.GetWidth() / 2), 0, (float)(area.Top + area.GetLength() / 2));
        room.transform.localRotation = rotation;
        room.GetComponent<RoomGenerator>().Light = Light;

        Rooms.Add(room); 
    }

    public void OnDestroy()
    {
        foreach (GameObject room in this.Rooms)
        {
            Destroy(room);
        }
    }

    /*public RoomType getGoodRoomType(Area area)
    {
        foreach (RoomType roomType in RoomTypes)
        {
            if (roomType.isPossible(area))
            {
                return roomType;
            }
        }
        throw new System.Exception("No valid room found for "+area.GetLength() +"x"+area.GetWidth());
        *//*RoomType item = RoomTypes.OrderBy(x => x.getScore(area)).FirstOrDefault();
        return item;*//*
    }*/
}

[System.Serializable]
public class RoomType
{
    public float MinLength;
    public float MaxLength;
    [Range(0.0f, 1.0f)]
    public float MinPercentageOccurrences;
    public GameObject RoomPrefab;

    public bool isPossible(Area area)
    {
        if ((MinLength <= area.GetWidth() && area.GetWidth() <= MaxLength) && (MinLength <= area.GetLength() && area.GetLength() <= MaxLength))
        {
            return true;
        }
        return false;
    }

    /*public float getScore(Area area)
    {
        if (MinLength > area.GetWidth() || MinLength > area.GetLength())
        {
            return float.MaxValue;
        }
        return Mathf.Abs((float)(area.GetArea() - AverageSize)) - MinLength;
    }*/
}