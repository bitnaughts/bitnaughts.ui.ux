using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour {

    List<InteractionObject> interactables = new List<InteractionObject>();

	Vector3 click_position = new Vector3();
    Vector3 world_position = new Vector3();

    Camera camera;

	void Start () {
        camera = Camera.main;
	}
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            click_position = Input.mousePosition;

            world_position = camera.ScreenToWorldPoint(Input.mousePosition);
            print (click_position + " " + world_position);

            foreach (var interactable in interactables) {
                if (PointHandler.GetDistance (new PointF(interactable.x, interactable.y), new PointF(world_position.x, world_position.y)) < interactable.radius) {
                    print ("HIT: " + interactable.trigger);
                    Referencer.system_id = int.Parse(interactable.trigger);
                    SceneManager.LoadScene("System");
                    break;
                }
            }
        }

    }
    public void Add (float x, float y, float radius, string trigger) {
        Add(new InteractionObject(x, y, radius, trigger));
    }
    public void Add (InteractionObject interactable) {
        interactables.Add(interactable);
    }
}

public class InteractionObject {
    public float x, y;
    public float radius;
    public string trigger;
    public InteractionObject(float x, float y, float radius, string trigger) {
        this.x = x;
        this.y = y;
        this.radius = radius;
        this.trigger = trigger;
    }
}