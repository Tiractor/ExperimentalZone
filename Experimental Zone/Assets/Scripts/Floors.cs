using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Floor
{
    public int FloorNumber;      // Номер этажа
    public Tilemap FloorTilemap; // Ссылка на тайлмап пола
    public GameObject[] Props;   // Ссылки на пропсы этажа
    public GameObject[] Gateways; // Ссылки на переходы
    // Определите здесь другие элементы, если они нужны
}

[System.Serializable]
public class Map : TaskExecutor<Map>
{
    public Floor[] floors;

}