using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent (typeof (InteractionController))]
public class CanvasController : MonoBehaviour {

    InteractionController interaction_controller;
    List<PanelObject> panels = new List<PanelObject> ();

    void Start () {
        interaction_controller = this.GetComponent<InteractionController> ();

        var shipyard_panel = new ShipyardPanel (
            new PointF (0, 0),
            new SizeF (40, 40),

        );
        panels.Add (shipyard_panel);
    }

    void Update () {

    }

    // public void AddPanel(PointF position, SizeF size) {
    //     AddPanel(new PanelObject(position, size));
    // }
    // public void AddPanel(PanelObject panel) {
    //     panels.Add(panel);
    // }
}