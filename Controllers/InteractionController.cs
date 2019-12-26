using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionController : MonoBehaviour {

    List<Interaction> interactables = new List<Interaction> ();

    Vector3 click_position = new Vector3 ();
    Vector3 world_position = new Vector3 ();

    Camera camera;

    Task<string> data { get; set; }
    Interaction current_interaction;

    void Start () {
        camera = Camera.main;
    }
    void Update () {
        if (Input.GetKey ("backspace")) {
            SceneManager.LoadScene ("Overview");
        }

        if (Input.GetMouseButtonDown (0)) {
            click_position = Input.mousePosition;

            world_position = camera.ScreenToWorldPoint (Input.mousePosition);
            print (click_position + " " + world_position);

            foreach (var interactable in interactables) {
                if (interactable.Contains (world_position)) {
                    // if (PointHandler.GetDistance (new PointF (interactable.point.x, interactable.point.y), new PointF (world_position.x, world_position.y)) < interactable.radius) {
                    print ("HIT: " + interactable.trigger);

                    if (interactable.trigger.Contains ("Planet")) {

                        data = Referencer.database.Visit (
                            int.Parse (interactable.trigger.Split (' ') [1]), 0, 1
                        );
                        // current_interaction = interactable;

                        break;
                    } else if (interactable.trigger.Contains ("Asteroid")) {

                        data = Referencer.database.Mine (
                            int.Parse (interactable.trigger.Split (' ') [1]), 0, 1
                        );
                        // current_interaction = interactable;

                        break;
                    } else {
                        Referencer.system_id = int.Parse (interactable.trigger);
                        SceneManager.LoadScene ("System");
                        break;
                    }

                }
            }
        }
        if (data == null) {

        } else {
            if (data.IsCompleted) {
                Debug.Log (data.Result);
                // if (data.Result.Contains ("Not")) {
                //     Destroy (current_interaction.obj);
                // }
            } else {
                Debug.Log (data.Status);
            }
        }

    }
    public void Add (PointF point, float radius, string trigger, GameObject obj) {
        Add (new Interaction (point, new SizeF (radius, radius), trigger, obj));
    }
    public void Add (float x, float y, float radius, string trigger, GameObject obj) {
        Add (new Interaction (new PointF (x, y), new SizeF (radius, radius), trigger, obj));
    }
    public void Add (Panel panel) {
        Add (new Interaction (panel.position, panel.size, "yes", null));
    }
    public void Add (Interaction interactable) {
        interactables.Add (interactable);
    }
}

public class Interaction {
    public PointF point;
    public SizeF size;
    public string trigger;
    public GameObject obj;
    public Interaction (PointF point, SizeF size, string trigger, GameObject obj) {
        this.point = point;
        this.size = size;
        this.trigger = trigger;
        this.obj = obj;
    }
    public bool Contains (PointF target) {
        return target.x > point.x - size.x / 2 &&
            target.x < point.x + size.x / 2 &&
            target.y > point.y - size.y / 2 &&
            target.y < point.y + size.y / 2;
    }
}