using UnityEngine;
using UnityEngine.Tilemaps;

[System.Serializable]
public class Floor
{
    public int FloorNumber;      // ����� �����
    public Tilemap FloorTilemap; // ������ �� ������� ����
    public GameObject[] Props;   // ������ �� ������ �����
    public GameObject[] Gateways; // ������ �� ��������
    // ���������� ����� ������ ��������, ���� ��� �����
}

[System.Serializable]
public class Map : TaskExecutor<Map>
{
    public Floor[] floors;

}