using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy)), CanEditMultipleObjects]
public class GermPropertyHolder : Editor
{
    public SerializedProperty
        germType_Prop,
        wobbleParameter_Prop,
        randomMovementParameterX_Prop,
        randomMovementParameterY_Prop,
        aggressive_Prop,
        redSpeed_Prop,
        greenMovement_Prop,
        greenMoveSpeed_Prop,
        scaleLimit_Prop,
        rotationSpeed_Prop,
        positionChangeInterval_Prop,
        germHypertrophy_Prop,
        blueSpeed_Prop,
        germHypoTrophy_Prop;
        

    private void OnEnable()
    {
        germType_Prop = serializedObject.FindProperty("germType");
        wobbleParameter_Prop = serializedObject.FindProperty("wobbleParameter");
        randomMovementParameterX_Prop = serializedObject.FindProperty("randomMovementParameterX");
        randomMovementParameterY_Prop = serializedObject.FindProperty("randomMovementParameterY");
        aggressive_Prop = serializedObject.FindProperty("aggressive");
        redSpeed_Prop = serializedObject.FindProperty("redMoveSpeed");
        greenMovement_Prop = serializedObject.FindProperty("greenMovement");
        greenMoveSpeed_Prop = serializedObject.FindProperty("greenMoveSpeed");
        scaleLimit_Prop = serializedObject.FindProperty("scaleLimit");
        rotationSpeed_Prop = serializedObject.FindProperty("rotationSpeed");
        positionChangeInterval_Prop = serializedObject.FindProperty("positionChangeInterval");
        germHypertrophy_Prop = serializedObject.FindProperty("germHypertrophy");
        germHypoTrophy_Prop = serializedObject.FindProperty("germHypotrophy");
        blueSpeed_Prop = serializedObject.FindProperty("blueSpeed");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(germType_Prop);
        Enemy.Microbes germType = (Enemy.Microbes)germType_Prop.enumValueIndex;
        switch (germType)
        {
            case Enemy.Microbes.RedMicrobe:
                EditorGUILayout.PropertyField(aggressive_Prop, new GUIContent("Aggressive"));
                EditorGUILayout.PropertyField(redSpeed_Prop, new GUIContent("Speed"));
                EditorGUILayout.PropertyField(germHypertrophy_Prop, new GUIContent("Germ Hypertrophy"));
                EditorGUILayout.LabelField("");
                StandardParameters();
                break;

            case Enemy.Microbes.PassiveMicrobe:
                StandardParameters();
                break;

            case Enemy.Microbes.GreenMicrobe:
                EditorGUILayout.PropertyField(greenMovement_Prop, new GUIContent("Movement Parameters"));
                EditorGUILayout.PropertyField(scaleLimit_Prop, new GUIContent("Scale Limit"));
                EditorGUILayout.PropertyField(rotationSpeed_Prop, new GUIContent("Rotation Speed"));
                EditorGUILayout.PropertyField(greenMoveSpeed_Prop, new GUIContent("Speed"));
                EditorGUILayout.PropertyField(positionChangeInterval_Prop, new GUIContent("Position Change Interval"));
                EditorGUILayout.LabelField("");
                StandardParameters();
                break;
            case Enemy.Microbes.Antidote:
                EditorGUILayout.PropertyField(blueSpeed_Prop, new GUIContent("Speed"));
                EditorGUILayout.PropertyField(germHypoTrophy_Prop, new GUIContent("germ Hypotrophy"));
                EditorGUILayout.LabelField("");
                StandardParameters();
                break;
        }
        serializedObject.ApplyModifiedProperties();

    }
    // Update is called once per frame
    void StandardParameters()
    {
        EditorGUILayout.LabelField("Random Movement", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(wobbleParameter_Prop, new GUIContent("Wobble Parameters"));
        EditorGUILayout.PropertyField(randomMovementParameterX_Prop, new GUIContent("Random Movement X"));
        EditorGUILayout.PropertyField(randomMovementParameterY_Prop, new GUIContent("Random Movement Y"));
        EditorGUILayout.EndFoldoutHeaderGroup();
    }
}
