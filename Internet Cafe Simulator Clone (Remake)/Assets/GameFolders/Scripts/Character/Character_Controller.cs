using UnityEngine;
using Sirenix.OdinInspector;

[RequireComponent(typeof(Character))]
public class Character_Controller : MonoBehaviour
{
    #region Serialized Variables

    [FoldoutGroup("Variables")]

    [SerializeField] private Character _character;

    [FoldoutGroup("Variables/KeyCodes"), SerializeField] private KeyCode _sprintKey = KeyCode.LeftShift;
    [FoldoutGroup("Variables/KeyCodes"), SerializeField] private KeyCode _jumpKey = KeyCode.Space;

    private Vector3 _moveDirection = Vector3.zero;

    #endregion

    #region Private Variables

    private float _rotateX;

    private Camera _camera;

    #endregion

    private void Awake()
    {
        _camera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    private void Update()
    {
        MoveLogic();
        RotateLogic();
    }
    private void MoveLogic()
    {
        bool isSprint = Input.GetKey(_sprintKey);

        _character.ReturnHorizontal(isSprint, out float horizontal);
        _character.ReturnVertical(isSprint, out float vertical);

        float _moveDirectionY = _moveDirection.y;

        _moveDirection = transform.forward * (horizontal * Input.GetAxis("Vertical")) + transform.right * (vertical * Input.GetAxis("Horizontal"));

        JumpLogic(_moveDirectionY);
      
        _character.Move(_moveDirection);
    }
    private void JumpLogic(float _moveDirectionY)
    {
        if (Input.GetKey(_jumpKey) & _character.GetCharacterController().isGrounded) Jump(); else _moveDirection.y = _moveDirectionY;
        if (!_character.GetCharacterController().isGrounded) _moveDirection.y -= _character.GetGravity() * Time.deltaTime;
    }
    private void Jump() => _moveDirection.y = _character.GetJumpSpeed();

    private void RotateLogic() => transform.rotation *= GetRotation();

    private Quaternion GetRotation()
    {
        _rotateX += -Input.GetAxis("Mouse Y") * _character.GetLookSpeed();
        _rotateX = Mathf.Clamp(_rotateX, -_character.GetLookXLimit(), _character.GetLookXLimit());
        _camera.transform.localRotation = Quaternion.Euler(_rotateX, 0, 0); //Camera Controller açýp SetRotation a bu valueyi gireblrsn herkes kendi iþini yapmýþ olur;

        return Quaternion.Euler(0, Input.GetAxis("Mouse X") * _character.GetLookSpeed(), 0);
    }
}