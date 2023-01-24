using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using System.Linq;

public class OfficeGenerator : MonoBehaviour
{
    public int MinSplittableArea = 3;
    public float MaxHallRate = 0.15F;

    public int width = 20;
    public int height = 40;

    private Area House;
    private double TotalHallArea;
    private List<Area> Chunks, Halls, Blocks, Rooms;

    public void Generate()
    {
        foreach (Office c in this.GetComponents<Office>()) {
            Destroy(c);
        }

        TotalHallArea = 0;
        House = new Area(0, 0, width, height);
        Chunks = new List<Area>();
        Halls = new List<Area>();
        Blocks = new List<Area>();
        Rooms = new List<Area>();

        ChunksToBlocks();
        BlocksToRooms();

        GameObject office = new GameObject("Office");
        Office officeScript = office.gameObject.AddComponent<Office>();
        officeScript.setup();
        foreach (Area room in Rooms)
        {
            officeScript.AddRoom(room);
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
        while ((Chunks.Count > 0) && (TotalHallArea / (double) House.GetArea() < MaxHallRate))
        {
            Area chunk = Chunks.Max();
            Chunks.Remove(chunk);

            if (chunk.GetArea() > MinSplittableArea)
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

    private void BlocksToRooms()
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
                Rooms.Add(block);
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



    /*public void placeWalls()
    {
<<<<<<< Updated upstream
        for (int z = 0; z < office.getLength(); z++)
=======
        if (!Application.isPlaying) return;
        /*for (int y = 0; y < size; y++)
>>>>>>> Stashed changes
        {
            placeWall(0, z + 0.5, 0);
        }
        for (int x = 0; x < office.getWidth(); x++)
        {
            placeWall(x + 0.5, 0, 90);
            for (int z = 0; z < office.getLength(); z++)
            {
                if (office.getCell(x, z) != office.getCell(x+1, z))
                {
                    placeWall(x + 1, z + 0.5, 0);
                }
                if (office.getCell(x, z) != office.getCell(x, z + 1))
                {
                    placeWall(x + 0.5, z + 1, 90);
                }
            }
        }
    }

    public void placeWall(double x, double z, double rotation)
    {
        Vector3 position = new Vector3((float)(x), 0, (float) (z));
        Quaternion quaternion = Quaternion.AngleAxis((float) rotation, Vector3.up);
        GameObject wallObject = Instantiate(wall, position, quaternion, building.transform);
    }*/
}
