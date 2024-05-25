using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Tags
{
    NoPrecipitation
}

public class Area : MonoBehaviour
{
    [SerializeField]
    private List<Tags> tags = new List<Tags>();
    static private List<Area> allAreas = new List<Area>();
    private void Awake()
    {
        allAreas.Add(this);
    }

    public bool ContainsTag(Tags tag)
    {
        return tags.Contains(tag);
    }

    public static List<Area> GetAllAreasWithTag( Tags tag)
    {
        List<Area> areasWithTag = new List<Area>();
        foreach (Area area in allAreas)
        {
            if (area.ContainsTag(tag))
            {
                areasWithTag.Add(area);
            }
        }
        return areasWithTag;
    }

}
