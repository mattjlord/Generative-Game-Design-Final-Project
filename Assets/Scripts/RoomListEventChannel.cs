using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/RoomList Event Channel")]
public class RoomListEventChannel : ScriptableObject
{
    public UnityAction<List<BasicRoom>> OnEventRaised;

    public void RaiseEvent(List<BasicRoom> val)
    {
        OnEventRaised?.Invoke(val);
    }
}