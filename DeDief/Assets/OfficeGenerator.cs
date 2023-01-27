using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OfficeGenerator : MonoBehaviour
{
    public float Width = 20;
    public float Length = 40;
    public float Height = 2;
    public float MinArea = 3;
    public float MaxHallRate = 0.15F;
    public float MinHallSize = 1;

    private Area House;
    private double TotalHallArea;
    private List<Area> Chunks, Halls, Blocks, Areas;

    public List<RoomType> RoomTypes;
    List<GameObject> Rooms;

    private void Start()
    {
        Generate();
    }

    public void Generate()
    {
        // Remove and reset everyting
        foreach (Transform c in transform) {
            Destroy(c.gameObject);
        }
        Rooms = new List<GameObject>();
        TotalHallArea = 0;
        House = new Area(0, 0, Width, Length);
        Chunks = new List<Area>();
        Halls = new List<Area>();
        Blocks = new List<Area>();
        Areas = new List<Area>();

        // Generate office
        ChunksToBlocks();
        BlocksToAreas();

        foreach (Area area in Areas)
        {
            PlaceArea(area);
        }

        //// TODO:

        // carve halls;
        // where hall faces much older hall:
        // place wall;

        // carve rooms, leaving walls;

        // put every room in queue of unreachable rooms;
        // while this queue is not empty:
        // get next room from queue;
        // if room is touching any number of halls:
        // make door, facing any avaliable hall;
        // put this room in queue of reachable rooms;
        // `continue`;
        // if room is touching any other reachable room:
        // connect this with other;
        // place door, if Random wants so;
        // `continue`;
        // put this room in queue of unreachable rooms;

        // place windows;
    }

    private void ChunksToBlocks()
    {
        Chunks.Add(House);
        while ((Chunks.Count > 0) && (TotalHallArea / (double)House.GetArea() < MaxHallRate))
        {
            Area chunk = Chunks.Max();
            Chunks.Remove(chunk);

            if (chunk.GetArea() > MinArea)
            {
                (Area chunk_a, Area hall, Area chunk_b) = chunk.SplitThree();
                Chunks.Add(chunk_a);
                Chunks.Add(chunk_b);
                Halls.Add(hall);
                TotalHallArea += hall.GetArea();
            }
            else
            {
                Blocks.Add(chunk);
            }
        }
        Blocks.AddRange(Chunks);
        Chunks.Clear();
    }

    private void BlocksToAreas()
    {
        while (Blocks.Count > 0)
        {
            Area block = Blocks.Max();
            Blocks.Remove(block);

            if (WantSplitBlock(block))
            {
                (Area block_a, Area block_b) = block.SplitTwo();
                Blocks.Add(block_a);
                Blocks.Add(block_b);
            }
            else
            {
                Areas.Add(block);
            }
        }
    }

    private bool WantSplitBlock(Area block)
    {
        if (block.GetArea() < 4)
        {
            return false;
        }
        else
        {
            double chance = Random.Range(0, (float)block.GetArea());
            if (chance < 5)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }

    public void PlaceArea(Area area)
    {
        GameObject roomPrefab = getGoodRoom(area);
        GameObject room = Instantiate(roomPrefab, transform);
        BoxCollider collider = room.GetComponent<BoxCollider>();
        collider.size = new Vector3((float)area.GetWidth(), Height, (float)area.GetHeight());
        room.transform.position = new Vector3((float)(area.Left + area.GetWidth() / 2), 0, (float)(area.Top + area.GetHeight() / 2));
        Rooms.Add(room); 
    }

    public void OnDestroy()
    {
        foreach (GameObject room in this.Rooms)
        {
            Destroy(room);
        }
    }

    public GameObject getGoodRoom(Area area)
    {
        RoomType item = RoomTypes.OrderBy(x => x.getScore(area)).FirstOrDefault(); ;
        return item.getRandomRoom();
    }
}

[System.Serializable]
public class RoomType
{
    public string Name;
    public float AvarageSize;
    public GameObject[] Prefabs;

    public GameObject getRandomRoom()
    {
        return Prefabs[Random.Range(0, Prefabs.Length)];
    }

    public float getScore(Area area)
    {
        return Mathf.Abs((float)(area.GetArea() - AvarageSize));
    }
}