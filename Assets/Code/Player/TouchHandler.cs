using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private Vector2 offset;
    private bool swipeHandled;
    private bool tapHandled;
    private bool postponePointerUp;
    
    public void OnDrag(PointerEventData eventData)
    {
        if (swipeHandled) {
            return;
        }
        
        offset += eventData.delta;
        
        if (offset.magnitude > 20)
        {
            swipeHandled = true;
            
            if (offset.normalized.y < -0.7f)
            {
                OnSwipeDown();
            }
            
            offset = Vector2.zero;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        offset = Vector2.zero;
        swipeHandled = false;
        tapHandled = false;
        
        OnPointerDown();
        StartCoroutine(PostponeTap());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (swipeHandled) {
            return;
        }

        if (tapHandled)
        {
            postponePointerUp = false;
            OnPointerUp();
        }
        else
        {
            postponePointerUp = true;
        }
    }

    private IEnumerator PostponeTap()
    {
        var delay = 100f; 

        var rootConfig = Locator.Find<RootConfig>();
        if (rootConfig != null)
        {
            delay = rootConfig.InputConfig.TapDetectionTime;
        }

        yield return new WaitForSeconds(delay);
        if (!swipeHandled)
        {
            tapHandled = true;
            OnTap();
            if (postponePointerUp)
            {
                postponePointerUp = false;
                OnPointerUp();
            }
        }
    }
    
    // The method called with short delay to let some time for swipe detection
    // If swipe is initiated, OnTap won't be called
    protected virtual void OnTap()
    {}

    // The method called as soon as pointer is down
    // Tap, Swipe or PointerUp does not influence this call 
    protected virtual void OnPointerDown()
    {}

    // The method is always called after OnTap() (so that there is always a delay)
    protected virtual void OnPointerUp()
    {}
    
    protected virtual void OnSwipeDown()
    {}
}