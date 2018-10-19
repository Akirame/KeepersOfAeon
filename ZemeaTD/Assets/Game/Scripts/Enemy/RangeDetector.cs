using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeDetector : MonoBehaviour
{
    public delegate void DetectorAction(RangeDetector d);
    public DetectorAction RampartOnRange;
    public DetectorAction RampartOffRange;
    private Rampart rampart;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rampart")
        {
            rampart = collision.GetComponent<Rampart>();
            RampartOnRange(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rampart")
        {
            rampart = null;
            RampartOffRange(this);
        }
    }
    public Rampart GetRampart()
    {
        return rampart;
    }
}

