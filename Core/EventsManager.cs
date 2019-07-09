using System.Collections.Generic;
using System;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class EventsManager
    {
        public string iD;

        public delegate void EventReceiver(params object[] parameterContainer);

        private Dictionary<string, EventReceiver> _events;
        private bool _logWarnings = false;


        public void EnableLogWarnings()
        {
            _logWarnings = true;
        }

        public void SubscribeToEvent(string eventType, EventReceiver listener)
        {
            if (_events == null)
                _events = new Dictionary<string, EventReceiver>();

            if (!_events.ContainsKey(eventType))
                _events.Add(eventType, null);

            _events[eventType] += listener;
        }

        public void UnsubscribeToEvent(string eventType, EventReceiver listener)
        {
            if (_events != null)
            {
                if (_events.ContainsKey(eventType))
                    _events[eventType] -= listener;
            }
        }

        public void TriggerEvent(string eventType)
        {
            TriggerEvent(eventType, null);
        }

        public void TriggerEvent(string eventType, params object[] parametersWrapper)
        {
            if (_events == null)
            {
                if(_logWarnings) 
                    UnityEngine.Debug.LogWarning("No events subscribed");
                
                return;
            }

            if (_events.ContainsKey(eventType))
            {
                if (_events[eventType] != null)
                    _events[eventType](parametersWrapper);
            }
        }

        public void DisponeAllEvents()
        {
            if(_events != null)
                _events.Clear();
        }
    }
}