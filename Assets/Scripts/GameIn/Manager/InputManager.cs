using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Vector2 swipeStartPos;
    private Vector2 swipeEndPos;
    private bool isSwiping = false;
    private float swipeThreshold = 1; // Threshold value for detecting a swipe
    private Camera camera => Camera.main;

    private void Update()
    {
        // Check for mouse button press to start a swipe
        if (Input.GetMouseButtonDown(0))
        {
            swipeStartPos = Input.mousePosition;
            isSwiping = true;
        }

        // Check for mouse button release after a swipe
        if (Input.GetMouseButtonUp(0) && isSwiping)
        {
            swipeEndPos = Input.mousePosition;
            DetectSwipe(); // Detect the direction of the swipe
            isSwiping = false;
        }
    }

    // Detect the direction of the swipe based on start and end positions
    private void DetectSwipe()
    {
        Vector2 swipeDelta = swipeEndPos - swipeStartPos;
        int[] swipeDirection = { 0, 0 }; // Array to store swipe directions: [x, y]

        if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
        {
            // Horizontal swipe detected
            if (swipeDelta.x > swipeThreshold)
                swipeDirection[0] = -1; // Swipe to the right
            else if (swipeDelta.x < -swipeThreshold)
                swipeDirection[0] = 1; // Swipe to the left
        }
        else
        {
            // Vertical swipe detected
            if (swipeDelta.y > swipeThreshold)
                swipeDirection[1] = 1; // Swipe upwards
            else if (swipeDelta.y < -swipeThreshold)
                swipeDirection[1] = -1; // Swipe downwards
        }

        // Find the clicked block under the swipe and send a message with swipe information
        Block block = FindClickedBlock();
        if (block)
        {
            ObserverManager.Send<SwipeMessage, BoardController>(SwipeMessage.Create(block, swipeDirection));
        }
    }

    // Find the clicked block using raycasting
    private Block FindClickedBlock()
    {
        Vector2 mousePosition = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            Block block = hit.transform.GetComponent<Block>();
            if (block != null)
            {
                return block;
            }
        }

        return null; // No block was clicked
    }
}
