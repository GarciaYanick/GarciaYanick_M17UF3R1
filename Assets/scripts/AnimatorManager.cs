using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{

    Animator animator;
    int horizontal;
    int vertical;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal");
        vertical = Animator.StringToHash("Vertical");
    }

    public void UpdateAnimatorValues(float horizontalMov, float verticalMov, bool isSprinting)
    {
        //Animation snapping
        float snappedHorizontal;
        float snappedVertical;

        #region Snapped Horizontal
        if (horizontalMov > 0 && horizontalMov < 0.55f) 
        {
            snappedHorizontal = 0.5f;
        }
        else if (horizontalMov > 0.55f)
        {
            snappedHorizontal = 1;
        }
        else if (horizontalMov < 0 && horizontalMov > -0.55f)
        {
            snappedHorizontal = -0.5f;
        }
        else if (horizontalMov < -0.55f)
        {
            snappedHorizontal = -1;
        }
        else
        {
            snappedHorizontal = 0;
        }
        #endregion
        #region Snapped Vertical
        if (verticalMov > 0 && verticalMov < 0.55f)
        {
            snappedVertical = 0.5f;
        }
        else if (verticalMov > 0.55f)
        {
            snappedVertical = 1;
        }
        else if (verticalMov < 0 && verticalMov > -0.55f)
        {
            snappedVertical = -0.5f;
        }
        else if (verticalMov < -0.55f)
        {
            snappedVertical = -1;
        }
        else
        {
            snappedVertical = 0;
        }
        #endregion

        if (isSprinting)
        {
            snappedHorizontal = horizontalMov;
            snappedVertical = 2;
        }

        animator.SetFloat(horizontal, snappedHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(vertical, snappedVertical, 1.0f, Time.deltaTime);
    }
}
