using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects] // Don't ruin everyone's day
[CustomEditor (typeof (MonoBehaviour), true)] // Target all MonoBehaviours and descendants
public class MonoBehaviourCustomEditor : UnityEditor.Editor {
    public override void OnInspectorGUI () {
        DrawDefaultInspector (); // Draw the normal inspector

        // Currently this will only work in the Play mode. You'll see why
        if (Application.isPlaying) {
            // Get the type descriptor for the MonoBehaviour we are drawing
            var type = target.GetType ();

            // Iterate over each private or public instance method (no static methods atm)
            foreach (var method in type.GetMethods (BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)) {
                // make sure it is decorated by our custom attribute
                var attributes = method.GetCustomAttributes (typeof (ExposeMethodInEditorAttribute), true);
                if (attributes.Length > 0) {

                    if (GUILayout.Button ("Run: " + method.Name)) {
                        // If the user clicks the button, invoke the method immediately.
                        // There are many ways to do this but I chose to use Invoke which only works in Play Mode.
                        ((MonoBehaviour) target).Invoke (method.Name, 0f);
                    }
                }
            }
        }
    }
}