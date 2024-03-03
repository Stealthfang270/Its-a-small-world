using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static UnityEvent growBig = new UnityEvent();
    public static UnityEvent growSmall = new UnityEvent();
    public static UnityEvent toggle = new UnityEvent();
    public static UnityEvent playerDeath = new UnityEvent();
    public static UnityEvent<Vector2> setPlayerRespawnLocation = new UnityEvent<Vector2>();
}
