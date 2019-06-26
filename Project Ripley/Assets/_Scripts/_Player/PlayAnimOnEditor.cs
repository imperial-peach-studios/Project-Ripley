using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;

[System.Serializable]
public class AnimatorState
{
    public string name;

    public string layerName = "";
    public string stateName = "";
    public string parameterName = "";

    public int layerOrder = 0;
    public int stateOrder = 0;
    public int parameterIndex = 0;
    public int parameterValue = 0;

    public int perspectiveValue = 0;
    public int directionValue = 0;

    public Color enableColor = Color.green;
    public Color disableColor = Color.red;

    public float minVal = 0, maxVal = 1;
}

[System.Serializable]
public class Point
{
    public Vector2 pos = Vector2.zero;
    public Vector2 size = Vector2.zero;
    public float t = 0;
    //public float point;
    public float a = 0;
}

[ExecuteInEditMode]
public class PlayAnimOnEditor : MonoBehaviour
{
    public Animator anim;
    [SerializeField] string stateName;
    //[SerializeField] Directions directions;
    [Range(9, 12)]
    [SerializeField] int attackID;
    [Header("0.5f Is Half Way On The Animation")]
    [Range(0, 1)]
    public float normalizedTime;
    public int currentIndex = 0;

    //Point Variables:
    public List<Point> positionList = new List<Point>();
    public List<Vector2> positionPoints = new List<Vector2>();
    public List<float> timers = new List<float>();
    public List<float> points = new List<float>();
    public Vector2 currentPosition;
    public Vector2 currentSize;
    public float currentAngle;

    Vector2 targetDestination;
    Vector2 targetOrigin;

    //Editor Variables:
    //public string currentLayer, currentState, currentParameter;
    public int currentPerspective = 0;
    public int currentDirection = 0;
    public int currentParameterIndex = 0;
    public string[] perspectives = { "Top Down", "Platformer" };
    public string[] directions = { "Up", "Right", "Left", "Down" };
    public string[] topDownDirections = { "Up", "Right", "Left", "Down" };
    public float minLimit = 0, maxLimit = 1;
    public List<AnimatorState> animatorStates = new List<AnimatorState>();

    [Space]
    [SerializeField] LayerMask collideWithLayer;
    public List<PlayerAttackSmall> attackColls = new List<PlayerAttackSmall>();

    public Vector2 direction;

    void Start()
    {
        
    }

    void Update()
    {
        if (anim != null)
        {
            if (!Application.isPlaying)
            {
                InEditMode();
            }
            else
            {
                InPlayMode();
            }
        }
        else
        {
            anim = GetComponent<Animator>();
        }
    }

    void InEditMode()
    {
        //if(currentDirection >= directions.Length)
        //{
        //    currentDirection = 0;
        //}
        
        if (currentIndex >= 0)
        {
            AnimatorState s = animatorStates[currentIndex];

            if (topDownDirections[s.directionValue] == "Up")
            {
                direction = Vector2.up;
            }
            else if (topDownDirections[s.directionValue] == "Right")
            {
                direction = Vector2.right;
            }
            else if (topDownDirections[s.directionValue] == "Left")
            {
                direction = Vector2.left;
            }
            else if (topDownDirections[s.directionValue] == "Down")
            {
                direction = Vector2.down;
            }

            anim.SetFloat("Horizontal", direction.x);
            anim.SetFloat("Vertical", direction.y);
            anim.SetFloat(animatorStates[currentIndex].parameterName, animatorStates[currentIndex].parameterValue);
            setAnimationFrame(animatorStates[currentIndex].stateName, animatorStates[currentIndex].layerOrder, normalizedTime);
        }
    }
    
    void InPlayMode()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Click Attacks"))
        {
            //direction = MovementDatabase.GetAnimInput();

            float getItemAttack = anim.GetFloat("ItemAttackID");

            for (int i = 0; i < attackColls.Count; i++)
            {
                PlayerAttackSmall aC = attackColls[i];
                if (aC.GetAnimationID() == getItemAttack)
                {
                    SmallCollisions cL = aC.GetComboList()[0];

                    float range = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
                    if (cL.GetCollisionEnable() < range && cL.GetCollisionDisable() > range)
                    {
                        cL.SetCanDamage(true);
                    }
                    else
                    {
                        cL.SetCanDamage(false);
                    }

                    if (range >= cL.GetCollisionDisable())
                    {
                        cL.SetCanDamage(false);
                    }

                    if (cL.GetCanDamage())
                    {
                        Collider2D[] colls = Physics2D.OverlapBoxAll((Vector2)transform.position + cL.GetOffset(direction), cL.GetSize(), 0f, collideWithLayer);

                        foreach (Collider2D c in colls)
                        {

                            Debug.Log("MASSIVE DAMAGE");
                        }
                    }
                }
            }
        }
    }

    public int AddAnimatorState()
    {
        animatorStates.Add(new AnimatorState());
        return animatorStates.Count - 1;
    }
    public int RemoveAnimatorState(int index)
    {
        animatorStates.RemoveAt(index);
        return animatorStates.Count - 1;
    }

    public void setAnimationFrame(string animationName, int layer, float normalizedAnimationTime)
    {
        if (anim != null)
        {

        }
        anim.speed = 0f;
        anim.Play(animationName, layer, normalizedAnimationTime);
        anim.Update(Time.deltaTime);
        //Debug.Log("HEJ");
    }

    public int Add(Vector2 position, Vector2 size, float angle)
    {
        positionList.Add(new Point());
        int count = positionList.Count - 1;
        positionList[count].pos = position;
        positionList[count].a = angle;
        positionList[count].size = size;

        //positionPoints.Add(position);
        //points.Add(0);
        //timers.Add(0);

        return count; //positionPoints.Count - 1
    }

    public void Remove(int index)
    {
        positionList.RemoveAt(index);

        //positionPoints.RemoveAt(index);
        //timers.RemoveAt(index);
        //points.RemoveAt(index);

        //getPositionOnce = true;
    }

    public void Change(int previousIndex, int currentIndex)
    {
        Point newPoint = positionList[previousIndex];
        positionList[previousIndex] = positionList[currentIndex];
        positionList[currentIndex] = newPoint;
    }

    void OnDrawGizmos()
    {
        //PlayerAttackSmall small = attackColls[currentIndex];
        //float range = anim.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //if (small.enableCollision < range && small.disableCollision > range)
        //{
        //    Gizmos.color = small.enabledColor;
        //}
        //else
        //{
        //    Gizmos.color = small.disabledColor;
        //}
        //Gizmos.DrawWireCube((Vector2)transform.position + small.Offset(), small.Size());



        //Gizmos.DrawWireCube((Vector2)transform.position + small., cL.GetSize(direction));


        //if (anim != null)
        //{
        //    if (!Application.isPlaying)
        //    {
        //        for (int i = 0; i < attackColls.Count; i++)
        //        {
        //            PlayerAttackSmall aC = attackColls[i];

        //            if (!aC.HideCollision() && aC.GetAnimationID() == attackID)
        //            {
        //                Gizmos.color = aC.GetColor();

        //                for (int j = 0; j < aC.GetComboList().Count; j++)
        //                {
        //                    SmallCollisions cL = aC.GetComboList()[j];

        //                    if (j == aC.GetAttackFrame())
        //                    {
        //                        if (directions == Directions.Up && cL.FixOffsetYToSizeY() || directions == Directions.Down && cL.FixOffsetYToSizeY())
        //                        {
        //                            //aC.SetOffset(new Vector2(0f, aC.GetOffset().y));
        //                            cL.SetOffset(new Vector2(0f, cL.GetOffset().y), direction);
        //                        }
        //                        else if (directions == Directions.Right && cL.FixOffsetXToSizeX())
        //                        {
        //                            //aC.SetOffset(new Vector2(aC.GetSize().x - (aC.GetSize().x / 2), aC.GetOffset().y));
        //                            cL.SetOffset(new Vector2(cL.GetSize().x - (cL.GetSize().x / 2), cL.GetOffset().y), direction);
        //                        }
        //                        else if (directions == Directions.Left && cL.FixOffsetXToSizeX())
        //                        {
        //                            //aC.SetOffset(new Vector2(-aC.GetSize().x + (aC.GetSize().x / 2), aC.GetOffset().y));
        //                            cL.SetOffset(new Vector2(-cL.GetSize().x + (cL.GetSize().x / 2), cL.GetOffset().y));
        //                        }

        //                        Gizmos.DrawWireCube((Vector2)transform.position + cL.GetOffset(direction), cL.GetSize(direction));
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    //    else
        //    //    {
        //    //        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Click Attacks"))
        //    //        {
        //    //            direction = MovementDatabase.GetAnimInput();

        //    //            float getItemAttack = anim.GetFloat("ItemAttackID");

        //    //            for (int i = 0; i < attackColls.Count; i++)
        //    //            {
        //    //                PlayerAttackSmall aC = attackColls[i];
        //    //                if (aC.GetAnimationID() == getItemAttack)
        //    //                {
        //    //                    SmallCollisions cL = aC.GetComboList()[0];

        //    //                    if (cL.GetCanDamage())
        //    //                    {
        //    //                        Gizmos.color = Color.green;
        //    //                    }
        //    //                    else
        //    //                    {
        //    //                        Gizmos.color = Color.red;
        //    //                    }

        //    //                    Gizmos.DrawWireCube((Vector2)transform.position + cL.GetOffset(direction), cL.GetSize(direction));
        //    //                }
        //    //            }
        //    //        }
        //    //    }
        //    //}
        //}
    }
}