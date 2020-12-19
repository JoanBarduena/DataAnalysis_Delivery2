using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventHandler : MonoBehaviour
{

    List<PlayerPosition> pos_events;
    List<PlayerKill> kill_events;
    List<PlayerDamaged> damaged_events;
    List<PlayerDeath> death_events;

    public GameObject Player;

    uint events_count = 0;
    float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        pos_events = new List<PlayerPosition>();
        kill_events = new List<PlayerKill>();
        damaged_events = new List<PlayerDamaged>();
        death_events = new List<PlayerDeath>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= 3)
        {
            NewPositionEvent(Player);
            timer = 0;
        } 
    }

    //Events Creation
    public void NewPositionEvent(GameObject player)
    {
        PlayerPosition new_pos = new PlayerPosition(player.transform.position, events_count, System.DateTime.Now);
        pos_events.Add(new_pos);
        events_count++;
    }
}
