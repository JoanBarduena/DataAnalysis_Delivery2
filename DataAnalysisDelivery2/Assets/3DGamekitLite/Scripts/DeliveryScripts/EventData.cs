using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Events
{
    NONE = 0,
    PLAYER_POSITION,
    PLAYER_KILL,
    PLAYER_DAMAGED,
    PLAYER_DEATH
};

public enum DeathTypes
{
    NONE = 0,
    FALL,
    ACID,
    ENEMY
};

[Serializable]
public class EventData
{
    public uint EventID;
    public DateTime TimeStamp;
    public Vector3 position;
    public Events event_type = Events.NONE;

    public EventData()
    {
        EventID = 0;
        TimeStamp = System.DateTime.Now;
        position = Vector3.zero;
    }

    public EventData(uint event_id, DateTime time, Vector3 _position)
    {
        EventID = event_id;
        TimeStamp = time;
        position = _position;
    }

    public string GetSerializedString()
    {
        return EventID + ";" + TimeStamp.ToString();
    }

    public string GetJSON()
    {
        return JsonUtility.ToJson(this, true);
    }
}

// ----------------------- EVENTS -----------------------
[Serializable]
public class PlayerPosition : EventData
{
    public PlayerPosition(uint event_id, DateTime time, Vector3 position) : base(event_id, time, position)
    {
        event_type = Events.PLAYER_POSITION;
    }
}
[Serializable]
public class PlayerKill : EventData
{
    public PlayerKill(string enemy_killed, Vector3 position, uint event_id, DateTime time) : base(event_id, time, position)
    {
        event_type = Events.PLAYER_KILL;
        this.enemy_killed = enemy_killed;
    }

    public string enemy_killed;
}
[Serializable]
public class PlayerDamaged : EventData
{
    public PlayerDamaged(string enemy_type, Vector3 position, uint event_id, DateTime time) : base(event_id, time, position)
    {
        event_type = Events.PLAYER_DAMAGED;
        this.enemy_type = enemy_type;
    }

    public string enemy_type;
}
[Serializable]
public class PlayerDeath : EventData
{
    public PlayerDeath(DeathTypes type, Vector3 position, uint event_id, DateTime time) : base(event_id, time, position)
    {
        event_type = Events.PLAYER_DEATH;
        death = type;
    }

    public DeathTypes death;
}




