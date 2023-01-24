using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeGenerator : MonoBehaviour
{
<<<<<<< Updated upstream
    public int width = 10;
    public int length = 30;
    public GameObject wall;
=======
    public int size = 50;
>>>>>>> Stashed changes

    Office office;
    GameObject building;
    int rooms;

    void Start()
    {        
        building = new GameObject("Office");
        generate();
    }

    public void generate()
    {
<<<<<<< Updated upstream
        this.office = new Office(width, length);
        this.rooms = 0;
        foreach (Transform child in building.transform)
        {
            Destroy(child.gameObject);
        }

        int x = 0;
        int rows = 0;
        while (x < office.getWidth())
        {
            int width = Random.Range(3, 5);
            placeRoomsRow(x, x + width);

            rows += 1;
            x += width;
            if (rows % 2 != 0)
            {
                placeCorridorRow(x);

                x += 1;
=======
        grid = new Cell[size, size];
        int boundry = 1;
        Vector2 position = new Vector2(0, 0);
        Vector2 movement = new Vector2(1, 0);
        for (int i = 0; i < size * size; i++)
        {
            position += movement;
            bool xBoundry = (position.x >= size - boundry || position.x < boundry) && movement.x != 0;
            bool yBoundry = (position.y >= size - boundry || position.y < boundry) && movement.y != 0;
            if (xBoundry || yBoundry)
            {
                Debug.Log("Switch")
                float x = movement.x;
                movement.x = movement.y;
                movement.y = -x;
>>>>>>> Stashed changes
            }
            string text = position.x + " " + position.y;
            Debug.Log(text);
        }
<<<<<<< Updated upstream
        placeWalls();
    }

    void placeCorridorRow(int x)
    {
        for (int y = 0; y < office.getLength(); y++)
=======
        /*for (int y = 0; y < size; y++)
>>>>>>> Stashed changes
        {
            office.setCell(x, y, "C");
        }
    }

    void placeRoomsRow(int x1, int x2)
    {

        int startY = 0;
        while (startY < office.getLength())
        {
            int length = Random.Range(2, 4);
            this.rooms += 1;
            int id = 1 + this.rooms;
            for (int y = startY; y <= startY + length; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    office.setCell(x, y, "O" + id);
                }
            }
<<<<<<< Updated upstream
            startY += length + 1;
        }
        return;
=======
        }*/
    }

    void getClosestCorridor(int x, int y)
    {

>>>>>>> Stashed changes
    }

    public void placeWalls()
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
        }*/
    }

    public void placeWall(double x, double z, double rotation)
    {
        Vector3 position = new Vector3((float)(x), 0, (float) (z));
        Quaternion quaternion = Quaternion.AngleAxis((float) rotation, Vector3.up);
        GameObject wallObject = Instantiate(wall, position, quaternion, building.transform);
    }
}
