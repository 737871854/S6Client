using UnityEngine;
using System.Collections;

public class Runway : MonoBehaviour
{
    public Transform DestGo = null;
    public Transform OriginGO = null;

    public Vector3 Destion
    {
        get
        {
            if(DestGo != null)
            {
                return DestGo.position;
            }
            return Vector3.one;
        }
    }

    public Vector3 Origin
    {
        get
        {
            if (OriginGO != null)
            {
                return OriginGO.position;
            }
            return Vector3.one;
        }
    }
}
