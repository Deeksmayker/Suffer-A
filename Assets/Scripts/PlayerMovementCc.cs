using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovementCc : MonoBehaviour
{
    private CharacterController _characterController;
    [SerializeField] private float speed;

    private void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        _characterController.Move(new Vector3(0, -1 * Time.deltaTime));
        Debug.Log(_characterController.isGrounded);
    }
}
