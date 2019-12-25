using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelObject {

    public int id;

    public string prefab_path;

    public PointF position;
    public SizeF size;

    public GameObject obj;

    public List<PanelObject> children;
    public List<InteractionObject> interactivity;

    // List<SpriteObject> sprites = new List<SpriteObject> ();

    public PanelObject (PointF position, SizeF size, GameObject obj) {
        this.id = 10;
        this.prefab_path = "Prefabs/Panels/Generic";
        this.position = position;
        this.size = size;
        this.obj = obj;

        interactivity = new List<InteractionObject> ();
        // PrefabHandler.Instantiate (path, position, size); // something here to build panels onto scene with proper nesting
    }

    public void Add (PanelObject panel) {
        Add (panel, false);
    }
    public void Add (PanelObject panel, bool interactive) {
        children.Add (panel);
        if (interactive) {
            interactivity.Add (panel);
        }
    }
}

public class ShipyardPanel : PanelObject {

    public PointF cursor; // { get; set; };

    public ShipyardPanel (PointF position, SizeF size, GameObject obj) : base (position, size, obj) {
        this.cursor = new PointF (0, 0);

        for (int i = 0; i < size.x; i++) {
            for (int j = 0; j < size.y; j++) {
                /* Add clickable locations for each element in the grid */
                // Add (panel, ref interactivity);
                interactivity.Add (
                    new PointF (i, j),
                    new SizeF (10, 10),
                    "Shipyard " + id + " clicked " + i + " " + j,
                    null
                );
            }
        }
        /* Add close, open, menu, etc? */
        // interactivity.Add ();
    }
}