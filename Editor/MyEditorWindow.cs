using UnityEngine;
using UnityEditor;

public class MyEditorWindow : EditorWindow
{
    private Rect objectRect = new Rect(20, 20, 100, 50); // Example object rectangle
    private bool clickedOnce = false;
    private float clickTimeThreshold = 0.3f; // Adjust as needed
    private float clickTimer = 0f;
    private bool isDragging = false;
    private Vector2 dragOffset = Vector2.zero;

    [MenuItem("Window/My Editor Window")]
    public static void ShowWindow()
    {
        GetWindow<MyEditorWindow>("My Editor Window");
    }

    private void OnGUI()
    {
        // Draw a box representing the object
        GUI.Box(objectRect, "Object");

        // Handle events
        Event e = Event.current;
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0 && objectRect.Contains(e.mousePosition))
                {
                    if (clickedOnce && Time.realtimeSinceStartup - clickTimer < clickTimeThreshold)
                    {
                        Debug.Log("Double clicked on object");
                        // Implement your double click behavior here
                        clickedOnce = false;
                    }
                    else
                    {
                        Debug.Log("clicked on object");

                        clickedOnce = true;
                        clickTimer = Time.realtimeSinceStartup;
                    }

                    isDragging = true;
                    dragOffset = e.mousePosition - objectRect.position;
                    GUI.changed = true; // To ensure repaint
                }
                break;

            case EventType.MouseDrag:
                if (isDragging)
                {
                    objectRect.position = e.mousePosition - dragOffset;
                    GUI.changed = true; // To ensure repaint
                    Debug.Log("MouseDrag on object");

                }
                break;

            case EventType.MouseUp:
                if (e.button == 0 && isDragging)
                {
                    isDragging = false;
                    GUI.changed = true; // To ensure repaint
                }
                break;
        }
    }
}
