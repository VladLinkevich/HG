using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMenager : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D)) { Messenger.Broadcast(GameEvent.RUNRIGHT); }
        else if (Input.GetKeyUp(KeyCode.D)) { Messenger.Broadcast(GameEvent.STOPRIGHT); }

        if (Input.GetKeyDown(KeyCode.A)) { Messenger.Broadcast(GameEvent.RUNLEFT); }
        else if (Input.GetKeyUp(KeyCode.A)) { Messenger.Broadcast(GameEvent.STOPLEFT); }

        if (Input.GetKeyDown(KeyCode.S)) { Messenger.Broadcast(GameEvent.FALL); }
        if (Input.GetKeyDown(KeyCode.W)) { Messenger.Broadcast(GameEvent.JUMP); }
        if (Input.GetKeyDown(KeyCode.LeftShift)) { Messenger.Broadcast(GameEvent.SHOOTMESSENGER); }
        

    }
}
