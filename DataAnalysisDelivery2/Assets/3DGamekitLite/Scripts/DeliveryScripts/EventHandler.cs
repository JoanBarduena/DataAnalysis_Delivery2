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
    string Damaged_filename = "DamagedEvents";
    string Death_filename = "DeathEvents";
    string Kill_filename = "KillEvents";

    // Start is called before the first frame update
    void Start()
    {
        pos_events = new List<PlayerPosition>();
        kill_events = new List<PlayerKill>();
        damaged_events = new List<PlayerDamaged>();
        death_events = new List<PlayerDeath>();

        if(collecting_data == false)
        {
            heatmap = true;
            ReadPosEventsData();
            ReadDamagedEventsData();
            ReadDeathEventsData();
            ReadKillEventsData();
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

            if(damaged_events.Count > 0)
            {
                WriteDamagedEventsData();
            }

            if (death_events.Count > 0)
            {
                WriteDeathEventsData();
            }

            if (kill_events.Count > 0)
            {
                WriteKillEventsData();
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

    public void NewDamagedEvent(GameObject player)
    {
        PlayerDamaged new_damage = new PlayerDamaged("EnemyAttack", player.transform.position, events_count, System.DateTime.Now);
        damaged_events.Add(new_damage);
        events_count++;
    }
    public void NewDeathEvent(GameObject player)
    {
        PlayerDeath new_death = new PlayerDeath(DeathTypes.ACID, player.transform.position, events_count, System.DateTime.Now);
        death_events.Add(new_death);
        events_count++;
        NewDamagedEvent(gameObject);
    }
    public void NewKillEvent(GameObject enemy)
    {
        PlayerKill new_kill = new PlayerKill(enemy.name.ToString(), transform.position, events_count, System.DateTime.Now);
        kill_events.Add(new_kill);
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

    public void WriteDamagedEventsData()
    {
        string events = "";

        for (int i = 0; i < damaged_events.Count; ++i)
        {
            events += damaged_events[i].GetJSON() + "\n";
        }

        Writer.Write(Damaged_filename, events);
    }

    public void WriteDeathEventsData()
    {
        string events = "";

        for (int i = 0; i < death_events.Count; ++i)
        {
            events += death_events[i].GetJSON() + "\n";
        }

        Writer.Write(Death_filename, events);
    }

    public void WriteKillEventsData()
    {
        string events = "";

        for (int i = 0; i < kill_events.Count; ++i)
        {
            events += kill_events[i].GetJSON() + "\n";
        }

        Writer.Write(Kill_filename, events);
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

    public void ReadDamagedEventsData()
    {
        string[] data = Reader.Read(Damaged_filename);

        if (data == null)
            return;

        for (int row = 0; row < data.Length - 1; ++row)
        {
            PlayerDamaged new_damaged = new PlayerDamaged();

            JsonUtility.FromJsonOverwrite(data[row], new_damaged);
            damaged_events.Add(new_damaged);
            events_count++;
        }
    }

    public void ReadDeathEventsData()
    {
        string[] data = Reader.Read(Death_filename);

        if (data == null)
            return;

        for (int row = 0; row < data.Length - 1; ++row)
        {
            PlayerDeath new_death = new PlayerDeath();

            JsonUtility.FromJsonOverwrite(data[row], new_death);
            death_events.Add(new_death);
            events_count++;
        }
    }

    public void ReadKillEventsData()
    {
        string[] data = Reader.Read(Kill_filename);

        if (data == null)
            return;

        for (int row = 0; row < data.Length - 1; ++row)
        {
            PlayerKill new_kill = new PlayerKill();

            JsonUtility.FromJsonOverwrite(data[row], new_kill);
            kill_events.Add(new_kill);
            events_count++;
        }
    }
}
