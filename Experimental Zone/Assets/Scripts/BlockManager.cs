using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private Block[][] World;


    public void Init()
    {
        World = new Block[10][];
        for(int i = 0;i < World.Length;++i)
        {
            World[i] = new Block[10];
        }
    }
    public void Init(int Size)
    {
        World = new Block[Size][];
        for (int i = 0; i < World.Length; ++i)
        {
            World[i] = new Block[Size];
        }
    }
    public void SetBlock(Block Target, Vector2 Coordinate)
    {
        if (Coordinate.x > World.Length || Coordinate.y > World[0].Length) return;
        if (World[(int)Coordinate.x][(int)Coordinate.y] == null) World[(int)Coordinate.x][(int)Coordinate.y] = Target;
        else
        {
            Destroy(World[(int)Coordinate.x][(int)Coordinate.y]);
            World[(int)Coordinate.x][(int)Coordinate.y] = Target;
        }
    }
}
