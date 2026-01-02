using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PickupAppear : MonoBehaviour
{
    public GameObject[] pushables;
    private List<PushableTall> _scripts = new();
    public GameObject pickup;
    private bool _levelComplete; 
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (pushables == null) return;
        foreach (GameObject pushable in pushables)
        {
            var script = pushable.GetComponent<PushableTall>();
            if(script != null)
                _scripts.Add(script);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_levelComplete) return;
        
        
        if (_scripts.Count > 0 && _scripts.All(p => p.isFinished))
        {
            pickup.SetActive(true);
            _levelComplete = true;
        }
    }
}
