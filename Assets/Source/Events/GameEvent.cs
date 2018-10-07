using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[CreateAssetMenu(menuName ="Game Events/Generic Event")] public class GameEvent : ScriptableObject
{
    [NonSerialized] private List<Action> _subscribers = new List<Action>( );

    public void Trigger( )
    {
        for(int i = _subscribers.Count - 1; i >= 0; i--)
            _subscribers[i]( );
    }

    public void Subscribe( Action newSubscriber )
    {
        if(!_subscribers.Contains( newSubscriber ))
            _subscribers.Add( newSubscriber );
    }

    public void UnSubscribe( Action unsubscriber )
    {
        if(_subscribers.Contains( unsubscriber ))
            _subscribers.Remove( unsubscriber );
    }
}
