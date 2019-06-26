using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackCollisions : MonoBehaviour
{
    [SerializeField] LayerMask collideWithLayer;
    [SerializeField] List<PlayerAttackSmall> attackColls = new List<PlayerAttackSmall>();


    void OnDrawGizmosSelected()
    {
        for (int i = 0; i < attackColls.Count; i++)
        {
            if(!attackColls[i].HideCollision())
            {
                //Gizmos.color = attackColls[i].GetColor();
                //Gizmos.DrawWireCube((Vector2)transform.position + attackColls[i].GetOffset(), (Vector2)attackColls[i].GetSize());
            }
        }
    }
}
