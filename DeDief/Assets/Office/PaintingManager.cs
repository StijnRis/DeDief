using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PaintingManager : MonoBehaviour
{
    [Range(0,100)]
    public int amount = 20;

    private string paintingPath = Application.streamingAssetsPath;
    List<Texture2D> paintingTextures = new List<Texture2D>();

    // Start is called before the first frame update
    void Start()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.streamingAssetsPath);
        var files = dir.GetFiles("painting_*.jpg");

        foreach(FileInfo file in files)
        {
            string path = file.FullName;
            var bytes = System.IO.File.ReadAllBytes(path);
            var tex = new Texture2D(1, 1);
            tex.LoadImage(bytes);

            paintingTextures.Add(tex);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D getRandomPaintingTexture()
    {
        
        int randomIndex = Random.Range(0, paintingTextures.Count);
        Texture2D randomTexture = paintingTextures[randomIndex];

        return randomTexture;
    }
}
