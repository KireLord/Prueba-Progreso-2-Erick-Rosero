using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrap : MonoBehaviour
{
    private void Update()
    {
        // Convert world point to Viewport so it's in 0->1 range.
        Vector3 viewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        // If it's moved out of the viewport wrap to opposite side.
        Vector3 moveAdjustment = Vector3.zero;
        if (viewportPosition.x < 0)
        {
            moveAdjustment.x += 1;
        }
        else if (viewportPosition.x > 1)
        {
            moveAdjustment.x -= 1;
        }
        else if (viewportPosition.y < 0)
        {
            moveAdjustment.y += 1;
        }
        else if (viewportPosition.y > 1)
        {
            moveAdjustment.y -= 1;
        }

        // Convert back into world coordinates before assigning.
        transform.position = Camera.main.ViewportToWorldPoint(viewportPosition + moveAdjustment);
    }
}
