using Mapbox.Geocoding;
using UnityEngine.UI;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Mapbox.Utils;

public class AbstractMapController : MonoBehaviour
{
    [SerializeField]
    AbstractMap _map;

    [SerializeField]
    [Geocode]
    string[] _locationStrings;
    Vector2d[] _locations;

    [SerializeField]
    float _spawnScale = 100f;

    [SerializeField]
    GameObject _markerPrefab;

    List<GameObject> _spawnedObjects;

    float zoom;
    public List<string> titles;
    public Interactor Interactor;

    void Start()
    {
        Interactor = GameObject.Find("ScreenCanvas").GetComponent<Interactor>();
        zoom = _map.AbsoluteZoom;
        _locations = new Mapbox.Utils.Vector2d[_locationStrings.Length];
        _spawnedObjects = new List<GameObject>();
        for (int i = 0; i < 4; i++)//< _locationStrings.Length; i++)
        {
            var locationString = _locationStrings[i];
            _locations[i] = Conversions.StringToLatLon(locationString);
            var instance = Instantiate(_markerPrefab);
            instance.name = i.ToString();
            instance.transform.localPosition = _map.GeoToWorldPosition(_locations[i], true);
            instance.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
            instance.transform.GetComponentsInChildren<TextMesh>()[0].text = titles[i];
            _spawnedObjects.Add(instance);
        }
    }
    private void Update()
    {
        int count = _spawnedObjects.Count;
        for (int i = 0; i < 4; i++)//count; i++)
        {
            var spawnedObject = _spawnedObjects[i];
            var location = _locations[i];
            spawnedObject.transform.localPosition = _map.GeoToWorldPosition(location, true);
            spawnedObject.transform.localScale = new Vector3(_spawnScale, _spawnScale, _spawnScale);
        }
        if (Interactor.Stage == "MapZoom") {
            Camera.main.transform.localPosition = new Vector3(0, 0, -200);
            zoom += Time.deltaTime * 1.5f;
            if (zoom >= 15) {
                // _map.UpdateMap(15f); //new Mapbox.Utils.Vector2d(47.43855f, -122.3071241f), 
                Interactor.MapZoomed();
            }
            else {
                _map.UpdateMap(_locations[Interactor.MarkerIndex], zoom);
                Camera.main.orthographicSize = Mathf.Clamp(5*zoom + 10f, 10f, 250f);
            }
        }
    }
    void Awake()
    {
        _map = FindObjectOfType<AbstractMap>();
    } 
    public void Zoom(float zoom) {
        _map.UpdateMap(zoom);
        Camera.main.orthographicSize = Mathf.Clamp(5*zoom + 10f, 10f, 250f);
    }
}
