using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectController : MonoBehaviour {
    private List<GameObject> m_objects;

	// Use this for initialization
	void Start () {
        m_objects = new List<GameObject>();
	}
	
    public GameObject NewObject
    {
        set { m_objects.Add(value); }
    }

    public List<GameObject> GetList
    {
        get { return m_objects; }
    }
}
