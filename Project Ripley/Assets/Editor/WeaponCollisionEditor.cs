using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(PlayAnimOnEditor))]
public class WeaponCollisionEditor : Editor
{
    List<string> allAnimatorLayerNames = new List<string>();
    List<string> allAnimatorStateNames = new List<string>();
    List<string> allAnimatorParameterNames = new List<string>();
    List<string> totalAnimatorStateNames = new List<string>();
    int totalAnimatorStates = 0;

    private float timeSlider;
    private int currentButton = 0;
    private Vector2 scrollPositionPos;
    private bool changeButton = false;
    private int previousButton = 0;
    public void SelectButton(int buttonIndex) => currentButton = buttonIndex;

    //string newName = "";
    Vector2 boxOffset;
    Vector2 boxSize;
    Vector2 boxOffsetUp, boxOffsetRight, boxOffsetLeft, boxOffsetDown;
    Vector2 boxSizeUp, boxSizeRight, boxSizeLeft, boxSizeDown;
    float minLimit = 0, maxLimit = 1;
   
    public override void OnInspectorGUI()
    {
        PlayAnimOnEditor pA = (PlayAnimOnEditor)target;

        pA.currentIndex = EditorGUILayout.IntField(pA.currentIndex, GUILayout.Width(80), GUILayout.Height(20));
        pA.normalizedTime = EditorGUILayout.Slider(pA.normalizedTime, 0f, 1f);

        GUILayout.BeginHorizontal();
        totalAnimatorStates = EditorGUILayout.IntField("All Animator States", totalAnimatorStates);
        AddAndRemoveButtons(pA);
        GUILayout.EndHorizontal();

        pA.currentIndex = EditorGUILayout.Popup(pA.currentIndex, totalAnimatorStateNames.ToArray(), GUILayout.Width(100), GUILayout.Height(20));

        if (pA.animatorStates.Count > 0)
        {
            GetInfoFromAnimator(pA);

            AnimatorProperties(pA);
            PerspectiveProperty(pA);
            //Perspectives();
            Point(pA);
            EnableAndDisableCollision(pA);
            SaveAndLoad();
        }
    }

    void OnSceneGUI()
    {
        PlayAnimOnEditor aC = (PlayAnimOnEditor)target;

        for (int i = 0; i < aC.positionList.Count; i++)
        {
            Color oldColor = Handles.color;
            if (i == currentButton)
            {
                Handles.color = Color.gray;
                Handles.RectangleHandleCap(0, aC.positionList[i].pos, Quaternion.Euler(0f, 0f, aC.positionList[i].a), aC.positionList[i].size.x, EventType.Repaint);
            }
            Handles.color = oldColor;
        }

        Handles.RectangleHandleCap(0, aC.currentPosition, Quaternion.Euler(0f, 0f, aC.currentAngle), aC.currentSize.x, EventType.Repaint);

        Handles.color = Color.green;
    }

    void AddAndRemoveButtons(PlayAnimOnEditor pA)
    {
        AddButton();
        RemoveButton();

        void AddButton()
        {
            if (GUILayout.Button("Add"))
            {
                totalAnimatorStates = pA.AddAnimatorState();
                totalAnimatorStateNames.Add("New State " + (totalAnimatorStateNames.Count + 1));

                pA.currentIndex = totalAnimatorStates;
            }
        }
        void RemoveButton()
        {
            if (GUILayout.Button("Remove"))
            {
                if (pA.currentIndex >= 0)
                {
                    totalAnimatorStates = pA.RemoveAnimatorState(pA.currentIndex);
                    totalAnimatorStateNames.RemoveAt(pA.currentIndex);

                    if (pA.currentIndex == (totalAnimatorStates + 1))
                    {
                        pA.currentIndex -= 1;
                    }
                    else if (pA.currentIndex > (totalAnimatorStates + 1))
                    {
                        pA.currentIndex = totalAnimatorStates;
                    }
                }
            }
        }
    }

    void AnimatorProperties(PlayAnimOnEditor pA)
    {
        AnimatorState s = pA.animatorStates[pA.currentIndex];

        GUILayout.BeginHorizontal();
        DisplayAnimatorLayerLabel();
        DisplayAnimatorStateLabel();
        DisplayAnimatorParameterLabel();
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        DisplayAnimatorLayerPopup();
        DisplayAnimatorStatePopup();
        GUILayout.BeginVertical();
        DisplayAnimatorParameterPopup();
        s.parameterValue = EditorGUILayout.IntSlider(s.parameterValue, 0, 20, GUILayout.Width(200), GUILayout.Height(20));
        GUILayout.EndVertical();
        GUILayout.EndHorizontal();

        void DisplayAnimatorLayerLabel()
        {
            EditorGUILayout.LabelField("Layer Name", GUILayout.Width(100), GUILayout.Height(20));
        }
        void DisplayAnimatorStateLabel()
        {
            EditorGUILayout.LabelField("State Name", GUILayout.Width(100), GUILayout.Height(20));
        }
        void DisplayAnimatorParameterLabel()
        {
            EditorGUILayout.LabelField("Parameter Name", GUILayout.Width(100), GUILayout.Height(20));
        }

        void DisplayAnimatorLayerPopup()
        {
            s.layerOrder = EditorGUILayout.Popup(s.layerOrder, allAnimatorLayerNames.ToArray(), GUILayout.Width(100), GUILayout.Height(20));
        }
        void DisplayAnimatorStatePopup()
        {
            s.stateOrder = EditorGUILayout.Popup(s.stateOrder, allAnimatorStateNames.ToArray(), GUILayout.Width(100), GUILayout.Height(20));
            s.stateName = allAnimatorStateNames[s.stateOrder];
        }
        void DisplayAnimatorParameterPopup()
        {
            s.parameterIndex = EditorGUILayout.Popup(s.parameterIndex, allAnimatorParameterNames.ToArray(), GUILayout.Width(100), GUILayout.Height(20));
            if(s.parameterIndex >= (allAnimatorParameterNames.Count - 1))
            {
                s.parameterName = allAnimatorParameterNames[s.parameterIndex];
            }
        }
    }

    void PerspectiveProperty(PlayAnimOnEditor pA)
    {
        AnimatorState s = pA.animatorStates[pA.currentIndex];

        EditorGUILayout.LabelField("Perspective");

        GUILayout.BeginHorizontal();
        s.perspectiveValue = EditorGUILayout.Popup(s.perspectiveValue, pA.perspectives, GUILayout.Width(100), GUILayout.Height(20));

        if (pA.perspectives[s.perspectiveValue] == "Top Down")
        {
            s.directionValue = EditorGUILayout.Popup(s.directionValue, pA.topDownDirections, GUILayout.Width(100), GUILayout.Height(20));
        }
        GUILayout.EndHorizontal();
    }

    void ChangeName()
    {
        //GUILayout.BeginHorizontal();
        //newName = EditorGUILayout.TextField("Change Name", newName);
        //if(GUILayout.Button("Change"))
        //{
        //    for (int i = 0; i < listNames.Count; i++)
        //    {
        //        if (i == currentIndex)
        //        {
        //            listNames[i] = newName;
        //            newName = "";
        //        }
        //    }
        //}
        //GUILayout.EndHorizontal();
    }

    void Perspectives(PlayAnimOnEditor pA)
    {
        AnimatorState a = pA.animatorStates[pA.currentIndex];

        if (pA.perspectives[pA.currentPerspective] == "Top Down")
        {
            switch (pA.directions[pA.currentDirection])
            {
                case "Up":
                    boxOffsetUp = EditorGUILayout.Vector2Field("Box Offset Up", boxOffsetUp);
                    boxSizeUp = EditorGUILayout.Vector2Field("Box Size Up", boxSizeUp);
                    break;
                case "Right":
                    boxOffsetRight = EditorGUILayout.Vector2Field("Box Offset Right", boxOffsetRight);
                    boxSizeRight = EditorGUILayout.Vector2Field("Box Size Right", boxSizeRight);
                    break;
                case "Left":
                    boxOffsetLeft = EditorGUILayout.Vector2Field("Box Offset Left", boxOffsetLeft);
                    boxSizeLeft = EditorGUILayout.Vector2Field("Box Size Left", boxSizeLeft);
                    break;
                case "Down":
                    boxOffsetDown = EditorGUILayout.Vector2Field("Box Offset Down", boxOffsetDown);
                    boxSizeDown = EditorGUILayout.Vector2Field("Box Size Down", boxSizeDown);
                    break;
            }
        }
        else
        {
            boxOffset = EditorGUILayout.Vector2Field("Box Offset", boxOffset);
            boxSize = EditorGUILayout.Vector2Field("Box Size", boxSize);
        }
    }

    void EnableAndDisableCollision(PlayAnimOnEditor pA)
    {
        float width = 60;
        float height = 20;
        AnimatorState s = pA.animatorStates[pA.currentIndex];

        EditorGUILayout.BeginHorizontal();

        EditorGUILayout.LabelField("Enable Collsion");
        EditorGUILayout.LabelField("Disable Collsion");
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();

        EnableCollisionField();

        DisableCollisionField();

        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        // minVal = EditorGUILayout.FloatField(minVal, GUILayout.Width(50), GUILayout.Height(20));
        s.minVal = EditorGUILayout.FloatField(s.minVal, GUILayout.Width(50), GUILayout.Height(20));
        s.maxVal = EditorGUILayout.FloatField(s.maxVal, GUILayout.Width(50), GUILayout.Height(20));
        //maxVal = EditorGUILayout.FloatField(maxVal, GUILayout.Width(50), GUILayout.Height(20));
        EditorGUILayout.EndHorizontal();

        if (pA.normalizedTime > s.minVal && pA.normalizedTime < s.maxVal)
            GUI.backgroundColor = s.enableColor;
        else
            GUI.backgroundColor = s.disableColor;

        EditorGUILayout.MinMaxSlider(ref s.minVal, ref s.maxVal, minLimit, maxLimit);

        GUI.backgroundColor = Color.white;
        void EnableCollisionField()
        {
            s.enableColor = EditorGUILayout.ColorField(s.enableColor, GUILayout.Width(width), GUILayout.Height(height));
            CustomSpace(215, 20);
            //enableCollision = EditorGUILayout.Slider(enableCollision, 0f, 1f);
        }
        void DisableCollisionField()
        {
            s.disableColor = EditorGUILayout.ColorField(s.disableColor, GUILayout.Width(width), GUILayout.Height(height));
            //disableCollision = EditorGUILayout.Slider(disableCollision, 0f, 1f);
        }
    }

    void SaveAndLoad()
    {
        GUILayout.BeginHorizontal();
        SaveButton();
        LoadButton();
        GUILayout.EndHorizontal();

        void SaveButton()
        {
            if (GUILayout.Button("Save Data"))
            {

            }
        }
        void LoadButton()
        {
            if (GUILayout.Button("Load Data"))
            {

            }
        }
    }

    void GetInfoFromAnimator(PlayAnimOnEditor pA)
    {
        Animator anim = pA.anim;

        UnityEditor.Animations.AnimatorController ac = anim.runtimeAnimatorController as UnityEditor.Animations.AnimatorController;

        //Number of layers:
        int layerCount = ac.layers.Length;

        // Names of each layer:
        for (int layer = 0; layer < layerCount; layer++)
        {
            if (!allAnimatorLayerNames.Contains(ac.layers[layer].name))
            {
                allAnimatorLayerNames.Add(ac.layers[layer].name);
            }
            //Debug.Log(string.Format("Layer {0}: {1}", layer, ac.layers[layer].name));
        }

        if (allAnimatorLayerNames.Count > ac.layers.Length)
        {
            int result = allAnimatorLayerNames.Count - ac.layers.Length;

            for (int i = 0; i < result; i++)
            {
                allAnimatorLayerNames.RemoveAt(i);
            }
        }

        // States on layer 0:
        UnityEditor.Animations.AnimatorStateMachine sm = ac.layers[0].stateMachine;
        for (int i = 0; i < sm.states.Length; i++)
        {
            if (!allAnimatorStateNames.Contains(sm.states[i].state.name))
            {
                allAnimatorStateNames.Add(sm.states[i].state.name);
            }
            //Debug.Log(string.Format("State: {0}", sm.states[i].state.name));
        }

        if (allAnimatorStateNames.Count > sm.states.Length)
        {
            int result = allAnimatorStateNames.Count - sm.states.Length;

            for (int i = 0; i < result; i++)
            {
                allAnimatorStateNames.RemoveAt(i);
            }
        }

        for (int i = 0; i < anim.parameterCount; i++)
        {
            if (!allAnimatorParameterNames.Contains(anim.parameters[i].name))
            {
                allAnimatorParameterNames.Add(anim.parameters[i].name);
            }
        }

        if (allAnimatorParameterNames.Count > anim.parameterCount)
        {
            int result = allAnimatorParameterNames.Count - anim.parameterCount;

            for (int i = 0; i < result; i++)
            {
                allAnimatorParameterNames.RemoveAt(i);
            }
        }
    }
    
    private void DrawButtons(PlayAnimOnEditor aC)
    {
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Add Point"))
        {
            //SelectButton(aC.Add(aC.transform.position, aC.transform.localScale, aC.transform.localEulerAngles.z));
            SelectButton(aC.Add(aC.currentPosition, aC.currentSize, aC.currentAngle));
        }
        if (GUILayout.Button("Remove Point"))
        {
            aC.Remove(currentButton);

            if (currentButton >= 1)
                currentButton -= 1;
            else
                currentButton = 0;

            if (changeButton)
                changeButton = !changeButton;
        }
        if (GUILayout.Button("Change Point"))
        {
            changeButton = !changeButton;
            previousButton = currentButton;
        }
        EditorGUILayout.EndHorizontal();
    }

    private void DrawPositions(PlayAnimOnEditor aC) //, Animator anim
    {
        bool hasPositions = aC.positionList.Count > 0;

        if (EditorGUILayout.BeginFadeGroup(hasPositions ? 1 : 0))
            DrawPositionsInternal();

        EditorGUILayout.EndFadeGroup();

        void DrawPositionsInternal()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.BeginVertical();

            DrawTimerSlider(currentButton);
            DrawPositionField(currentButton);
            DrawSizeField(currentButton);
            DrawAngleSlider(currentButton);

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            DrawCurrentPositionField();
            DrawCurrentSizeField();
            DrawCurrentAngleSlider();

            EditorGUILayout.EndVertical();

            scrollPositionPos = EditorGUILayout.BeginScrollView(scrollPositionPos, GUILayout.Width(120), GUILayout.Height(150));

            EditorGUILayout.BeginVertical();
            for (int i = 0; i < aC.positionList.Count; i++)
            {
                DrawPositionButtons(i);
            }
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndHorizontal();
        }

        void DrawPositionField(int i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("P Position", GUILayout.Width(60), GUILayout.Height(20));

            var positionFieldValue = EditorGUILayout.Vector2Field(
                "",
                aC.positionList[i].pos,
                GUILayout.Width(250), GUILayout.Height(20)
            );

            // aC.positionPoints[i] = positionFieldValue;
            aC.positionList[i].pos = positionFieldValue;
            EditorGUILayout.EndHorizontal();
        }
        void DrawSizeField(int i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("P Size", GUILayout.Width(60), GUILayout.Height(20));

            var sizeFieldValue = EditorGUILayout.Vector2Field(
                "",
                aC.positionList[i].size,
                GUILayout.Width(250), GUILayout.Height(20)
            );

            // aC.positionPoints[i] = positionFieldValue;
            aC.positionList[i].size = sizeFieldValue;
            EditorGUILayout.EndHorizontal();
        }
        void DrawTimerSlider(int i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Keyframe", GUILayout.Width(72), GUILayout.Height(20));

            var timeSliderValue = EditorGUILayout.FloatField(
                aC.positionList[i].t,
                GUILayout.Width(45), GUILayout.Height(15)
                );

            //aC.timers[i] = timeSliderValue;
            aC.positionList[i].t = timeSliderValue;
            EditorGUILayout.EndHorizontal();
        }
        void DrawAngleSlider(int i)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("P Angle", GUILayout.Width(72), GUILayout.Height(20));

            var timeSliderValue = EditorGUILayout.FloatField(
                aC.positionList[i].a,
                GUILayout.Width(45), GUILayout.Height(15)
                );

            //aC.timers[i] = timeSliderValue;
            aC.positionList[i].a = timeSliderValue;
            EditorGUILayout.EndHorizontal();
        }
        void DrawPositionButtons(int i)
        {
            //SetColor
            if (currentButton == i)
                GUI.backgroundColor = Color.green;
            //else
            //    GUI.backgroundColor = Color.white;

            //if(currentButton == i)
            //{
            //    if(changeButton)
            //    {
            //        GUI.backgroundColor = Color.yellow;
            //    }
            //    else
            //    {

            //    }
            //}
            if (previousButton == i)
                if (changeButton)
                    GUI.backgroundColor = Color.yellow;


            //DrawButtons
            //decide name for button
            string btnName = $"Position {i}";
            if (i == 0)
                btnName = "Start Position";

            //draw button
            if (GUILayout.Button(btnName, GUILayout.Width(95), GUILayout.Height(20)))
            {
                currentButton = i;

                if (changeButton == true)
                {
                    aC.Change(previousButton, currentButton);
                    previousButton = currentButton;
                    changeButton = false;
                }
            }
            GUI.backgroundColor = Color.white;
        }

        void DrawCurrentPositionField()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("C Position", GUILayout.Width(60), GUILayout.Height(20));

            var positionFieldValue = EditorGUILayout.Vector2Field(
                "",
                aC.currentPosition,
                GUILayout.Width(250), GUILayout.Height(20)
            );

            // aC.positionPoints[i] = positionFieldValue;
            aC.currentPosition = positionFieldValue;
            EditorGUILayout.EndHorizontal();
        }
        void DrawCurrentSizeField()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("C Size", GUILayout.Width(60), GUILayout.Height(20));

            var sizeFieldValue = EditorGUILayout.Vector2Field(
                "",
                aC.currentSize,
                GUILayout.Width(250), GUILayout.Height(20)
            );

            // aC.positionPoints[i] = positionFieldValue;
            aC.currentSize = sizeFieldValue;
            EditorGUILayout.EndHorizontal();
        }
        void DrawCurrentAngleSlider()
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("C Angle", GUILayout.Width(72), GUILayout.Height(20));

            var timeSliderValue = EditorGUILayout.FloatField(
                aC.currentAngle,
                GUILayout.Width(45), GUILayout.Height(15)
                );

            //aC.timers[i] = timeSliderValue;
            aC.currentAngle = timeSliderValue;
            EditorGUILayout.EndHorizontal();
        }
    }

    private void Lerp(PlayAnimOnEditor aC)
    {
        if (aC.positionList.Count <= 1)
            return;

        Vector2 a = Vector2.zero;
        Vector2 b = Vector2.zero;
        Vector2 sizeA = Vector2.zero;
        Vector2 sizeB = Vector2.zero;
        float aAngle = 0;
        float bAngle = 0;

        float currentTime = timeSlider;
        float currentTimeDelta = 0;
        for (int i = 0; i < aC.positionList.Count - 1; i++)
        {
            a = (Vector2)aC.gameObject.transform.position + aC.positionList[i].pos;
            b = (Vector2)aC.gameObject.transform.position + aC.positionList[i + 1].pos;

            sizeA = aC.positionList[i].size;
            sizeB = aC.positionList[i + 1].size;

            aAngle = aC.positionList[i].a;
            bAngle = aC.positionList[i + 1].a;

            currentTimeDelta = aC.positionList[i + 1].t - aC.positionList[i].t;

            if (currentTime < currentTimeDelta || i == aC.positionList.Count - 2)
            {
                break;
            }

            currentTime -= currentTimeDelta;
        }

        if (currentTimeDelta != 0)
        {
            Vector2 targetPos = Vector2.Lerp(a, b, aC.normalizedTime / currentTimeDelta);
            aC.currentPosition = targetPos;

            float rotZ = Mathf.Lerp(aAngle, bAngle, aC.normalizedTime / currentTimeDelta);
            aC.currentAngle = rotZ;
            aC.currentSize = Vector2.Lerp(sizeA, sizeB, aC.normalizedTime / currentTimeDelta);
        }
    }

    void Point(PlayAnimOnEditor aC)
    {
        //timeSlider = EditorGUILayout.Slider("Time", timeSlider, 0f, 1f); //anim.GetCurrentAnimatorStateInfo(0).length

        DrawButtons(aC);
        DrawPositions(aC); //, anim
        Lerp(aC);
    }

    void CustomSpace(float width, float height)
    {
        GUILayout.Label("", GUILayout.Width(width), GUILayout.Height(height));
    }
}