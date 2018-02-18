using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayerTransitioner))]
public class TransitionerEditor : Editor {

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        //DrawDefaultInspector();
        PlayerTransitioner targetClass = (PlayerTransitioner)target;

        if(GUILayout.Button("Reset Transition"))
        {
            targetClass.ResetTransitionLocation();

        }
    }

}
