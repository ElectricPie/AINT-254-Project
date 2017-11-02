using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    string tag;

    protected void TriggerOn(GameObject target)
    {
        //Debug.Log("Triggered");
        tag = target.tag;

        //Debug.Log(tag);
        if (tag == "Door")
        {
            //Debug.Log("Opening");
            target.GetComponent<DoorControler>().Open();
        }
        else if (tag == "Dispenser")
        {
            target.GetComponent<Dispenser>().Activate();
        }
    }

    protected void TriggerOff(GameObject target)
    {
        tag = target.tag;

        if (tag == "Door")
        {
            target.GetComponent<DoorControler>().Close();
        }
    }
}
