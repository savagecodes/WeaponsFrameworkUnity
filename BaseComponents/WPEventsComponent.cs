using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WPEventsComponent : MonoBehaviour
{
   
  private EventsManager _eventSystem;

  public EventsManager EventSystem => _eventSystem;

  public void Initialize()
  {
    _eventSystem = new EventsManager();
  }
}
