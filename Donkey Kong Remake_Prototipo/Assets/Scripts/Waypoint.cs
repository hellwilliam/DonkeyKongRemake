using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public List<Waypoint> nextWaypoints;
    public int lastChoice = -1;
}