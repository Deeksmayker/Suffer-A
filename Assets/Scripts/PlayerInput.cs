using UnityEngine;
using UnityEngine.Events;


public class PlayerInput : MonoBehaviour
{
    public static float HorizontalRaw { get; private set; }
    public static float VerticalRaw { get; private set; }
    public static UnityEvent OnJump;

    private void Update()
    {
        HorizontalRaw = Input.GetAxis("Horizontal");
        VerticalRaw = Input.GetAxis("Vertical");
    }
}
