using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    protected void TriggerOn(GameObject target)
    {
        string tag = target.tag;

        if (tag == "Door")
        {
            target.GetComponent<DoorControler>().Open();
        }
        else if (tag == "Dispenser")
        {
            target.GetComponent<Dispenser>().Activate();
        }
        else if (tag == "GooDrain")
        {
            target.GetComponent<GooDrain>().Toggle();
        }
    }

    protected void TriggerOff(GameObject target)
    {
        string tag = target.tag;

        if (tag == "Door")
        {
            target.GetComponent<DoorControler>().Close();
        }
        else if (tag == "GooDrain")
        {
            target.GetComponent<GooDrain>().Toggle();
        }
    }
}
