using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectHelper : MonoBehaviour
{
    static public GameObject FindChildWithTag(GameObject g, string tag)
    {
        foreach (Transform t in g.transform)
        {
            if (t.tag == tag)
                return t.gameObject;
        }

        return null;
    }
}
