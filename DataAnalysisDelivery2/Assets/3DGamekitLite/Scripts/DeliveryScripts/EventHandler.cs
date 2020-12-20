using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{

    public static List<PlayerPosition> pos_events;
    public static List<PlayerKill> kill_events;
    public static List<PlayerDamaged> damaged_events;
    public static List<PlayerDeath> death_events;

    public GameObject Player;
    public bool collecting_data = true;
    public static bool heatmap;

    uint events_count = 0;
    float timer = 0;

    //Filenames
    string Position_filename = "PositionEvents";

    // Start is called before the first frame update
    void Start()
    {
        pos_events = new List<PlayerPosition>();
        kill_events = new List<PlayerKill>();
        damaged_events = new List<PlayerDamaged>();
        death_events = new List<PlayerDeath>();

        if (collecting_data == false)
            heatmap = true;

        if(collecting_data == false)
        {
            ReadPosEventsData();
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (collecting_data)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                NewPositionEvent(Player);
                timer = 0;
            }
        }
        
    }

    private void OnApplicationQuit()
    {
        if(collecting_data)
        {
            if (pos_events.Count > 0)
            {
                WritePosEventsData();
            }
        }
        
    }

    //Events Creation -----------------------------------------
    public void NewPositionEvent(GameObject player)
    {
        PlayerPosition new_pos = new PlayerPosition(events_count, System.DateTime.Now, player.transform.position);
        pos_events.Add(new_pos);
        events_count++;
    }

    //Write Events --------------------------------------------
    public void WritePosEventsData()
    {
        string events = "";

        for (int i = 0; i<pos_events.Count; ++i)
        {
            events += pos_events[i].GetJSON() + "\n";
        }

        Writer.Write(Position_filename, events);
    }

    //Read Events ---------------------------------------------
    public void ReadPosEventsData()
    {
        string[] data = Reader.Read(Position_filename);

        if (data == null)
            return;

        for (int row = 0; row < data.Length-1; ++row)
        {
            PlayerPosition new_pos = new PlayerPosition();

            JsonUtility.FromJsonOverwrite(data[row], new_pos);
            pos_events.Add(new_pos);
            events_count++;
        }
    }
}
