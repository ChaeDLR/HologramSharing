using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

/*/// <summary>
/// pretty much just wraps the callback method from what I understand
/// </summary>
/// <param name="eventData"></param>
delegate void ActionCallback(BaseInputEventData eventData);

/// <summary>
/// Holds custom gameplay intput action method callbacks
/// 
/// These methods will likely need the static modifier removed
/// and an instance created in GameplayInputActionHandler so we can pass references to it
/// </summary>
class ActionCallbackMethods
{
    public static void CreateWire(BaseInputEventData eventData) => eventData.Use();
    public static void ConnectNodes(BaseInputEventData eventData) => eventData.Use();
}*/

/// <summary>
/// Script used to handle gameplay input action events at the Scene level
/// Invokes Unity events when the configured input action starts or ends
/// 
/// IMixedRealityGestureHandler
/// 
/// Actions = [ Select, Menu, Grip Pose, Pointer Pose, Teleport Direction, Trigger, Grip press,
///     Hold Action, Manipulate Action, Navigation Action, Scroll, Mouse Delta, Index Finger Pose,
///     Toggle Diagnostics, Toggle Profiler ]
/// </summary>
public class GameplayGeastureHandler : MonoBehaviour, IMixedRealityGestureHandler<Vector3>
{
    [SerializeField]
    private MixedRealityInputAction holdAction = MixedRealityInputAction.None;

    [SerializeField]
    private MixedRealityInputAction navigationAction = MixedRealityInputAction.None;

    [SerializeField]
    private MixedRealityInputAction manipulationAction = MixedRealityInputAction.None;

    [SerializeField]
    private MixedRealityInputAction tapAction = MixedRealityInputAction.None;

    private MixedRealityInputAction lastAction = MixedRealityInputAction.None;

    public void OnGestureStarted(InputEventData eventData)
    {
        lastAction = eventData.MixedRealityInputAction;
        if (lastAction == holdAction)
        {
            lastAction = eventData.MixedRealityInputAction;
            // if this throws an exception, we do not handle this action
            //LastActionCB = InputActions[eventData.MixedRealityInputAction.Id];
            // OnInputActionStarted.Invoke(eventData);
        } 
    }

    public void OnGestureUpdated(InputEventData eventData) => _ = 0;

    public void OnGestureCanceled(InputEventData eventData) => _ = 0;

    /// <summary>
    /// event data=Vector2 | vector3 | quaternion | MixedRealityPose
    /// </summary>
    /// <param name="eventData"></param>
    public void OnGestureCompleted(InputEventData eventData)
    {
        lastAction = eventData.MixedRealityInputAction;
        if (lastAction == holdAction) return;
    }
}

