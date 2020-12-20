using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatMap : MonoBehaviour
{
    int GridSize_X, GridSize_Y;
    int[,] EventCounts;

    public Transform start_pos;
    public Transform end_pos;

    public HeatMapCube HeatMapCubePrefab;

    public Gradient ColorGradient;

    public DataType DataType;

    public int MaxCounts = 10;

    private bool calculated_heatmap = false;

    private void Start()
    {
        GridSize_X = (int)(end_pos.position.x - start_pos.position.x);
        GridSize_Y = (int)(end_pos.position.z - start_pos.position.z);

        EventCounts = new int[GridSize_X, GridSize_Y];
    }

    private void Update()
    {
        if(calculated_heatmap == false && EventHandler.heatmap)
        {
            CountEvents();
            VisualizeEvents();
            calculated_heatmap = true;
        }
    }

    void CountEvents()
    {
        if (DataType == DataType.Position)
        {
            for (int i = 0; i < EventHandler.pos_events.Count; ++i)
            {
                Vector3 event_pos = EventHandler.pos_events[i].position;

                float index_x = start_pos.transform.position.x - event_pos.x;
                float index_y = start_pos.transform.position.z - event_pos.z;

                Debug.Log(index_x);
                Debug.Log(index_y);
                Debug.Log("-------------------------------------");

                EventCounts[(int)Mathf.Abs(index_x), (int)Mathf.Abs(index_y)]++;
            }
        }
        else if (DataType == DataType.PlayerKill)
        {
            for (int i = 0; i < EventHandler.kill_events.Count; ++i)
            {

            }
        }
        else if (DataType == DataType.PlayerDamage)
        {
            for (int i = 0; i < EventHandler.damaged_events.Count; ++i)
            {

            }
        }
        else if (DataType == DataType.Death)
        {
            for (int i = 0; i < EventHandler.death_events.Count; ++i)
            {

            }
        }

    }

    void VisualizeEvents()
    {
        for (int i = 0; i < GridSize_X; ++i)
        {
            for (int j = 0; j < GridSize_Y; ++j)
            {
                if(EventCounts[i,j] > 0)
                {
                    SpawnCube(i, j, EventCounts[i, j]);
                }
            }
        }
    }

    private void SpawnCube(int x, int y, int counts)
    {
        Vector3 pos = new Vector3(x + start_pos.position.x, GetHeight(x + (int)start_pos.position.x, y + (int)start_pos.position.z), y + (int)start_pos.position.z);
        var cube = Instantiate(HeatMapCubePrefab, pos, Quaternion.identity) as HeatMapCube;
        float f = Mathf.Clamp01((float)counts / MaxCounts);
        Color c = ColorGradient.Evaluate(f);
        c.a = 0.85f;
        cube.SetColor(c);
    }

    private float GetHeight(int x, int y)
    {
        Vector3 pos = new Vector3(x, 100, y);
        RaycastHit hit;
        if(Physics.Raycast(pos, Vector3.down, out hit))
        {
            return hit.point.y;
        }
        return 0;
    }


}

public enum DataType
{
    Position,
    PlayerKill,
    PlayerDamage,
    Death
}
