using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using UnityEngine;
using System;
using System.Collections;

public class AbstractMapController : MonoBehaviour
{
    AbstractMap _map;
    float zoom;
    void Awake()
    {
        _map = FindObjectOfType<AbstractMap>();
    }    
    public Interactor Interactor;
    void Start() {
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        zoom = _map.AbsoluteZoom;
    }

    void Update()
    {
        if (Input.GetKeyDown("r")) 
        {
            int zoom = _map.AbsoluteZoom;
            _map.UpdateMap(new Mapbox.Utils.Vector2d(47.438549f, -122.3071241f), zoom);
        } 
    }
    void FixedUpdate() {
        if (Interactor.Stage == "MapZoom") {
            Camera.main.transform.position = new Vector3(0, 200, 0);
            zoom += .05f;
            if (zoom >= 15) {
                _map.UpdateMap(new Mapbox.Utils.Vector2d(47.438549f, -122.3071241f), 15f);
                Interactor.Stage = "MapZoomed";
            }
            else {
                _map.UpdateMap(new Mapbox.Utils.Vector2d(47.438549f, -122.3071241f), 2*((int)(zoom + .5f) / 2));
                Camera.main.orthographicSize = Mathf.Clamp(250/15*((int)(zoom + .5f) / 2), 6f, 250f);
            }
        }
    }
}
