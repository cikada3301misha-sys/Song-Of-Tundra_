using UnityEngine;
using UnityEngine.InputSystem;

public class LockOnManager : MonoBehaviour
{
    [Header("Настройки поиска")]
    public float maxRadius = 20f;
    public LayerMask enemyLayer;
    public string enemyTag = "Enemy";

    [Header("Ссылки")]
    public Transform cameraPivot;
    public Transform playerModel;
    
    [Header("Параметры вращения")]
    public float rotationSpeed = 100f;

    private Transform currentTarget;
    private bool isCapsToggled = false;
    private bool isShiftHeld = false;

    
    public bool IsLocked => isCapsToggled || isShiftHeld;

    void LateUpdate()
    {
        HandleInput();

        if (IsLocked)
        {
            if (currentTarget == null) FindTarget();
            if (currentTarget != null) LookAtTarget();
        }
        else
        {
            if(currentTarget != null){
                currentTarget.GetComponent<TambourineRhythm>().Unfocus();
            }
            currentTarget = null;
        }
    }

    private void HandleInput()
    {
        var kb = Keyboard.current;
        if (kb == null) return;

        if (kb.capsLockKey.wasPressedThisFrame)
        {
            isCapsToggled = !isCapsToggled;
        }

        isShiftHeld = kb.leftShiftKey.isPressed;
    }

    private void FindTarget()
    {
        Collider[] enemies = Physics.OverlapSphere(transform.position, maxRadius, enemyLayer);
        float minDistance = Mathf.Infinity;

        foreach (var col in enemies)
        {
            if (col.CompareTag(enemyTag))
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < minDistance)
                {
                    minDistance = dist;
                    currentTarget = col.transform;
                }
            }
        }
        currentTarget.gameObject.GetComponent<TambourineRhythm>().Focus();
    }

    private void LookAtTarget()
    {
        Vector3 directionToEnemy = currentTarget.position - transform.position;

        Vector3 playerDir = new Vector3(directionToEnemy.x, 0, directionToEnemy.z);
        if (playerDir != Vector3.zero)
        {
            Quaternion playerLookRot = Quaternion.LookRotation(playerDir);
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, playerLookRot, Time.deltaTime * rotationSpeed);
        }

        Vector3 cameraDir = (currentTarget.position + Vector3.up * 1.5f) - cameraPivot.position;
        if (cameraDir != Vector3.zero)
        {
            Quaternion cameraLookRot = Quaternion.LookRotation(cameraDir);
            cameraPivot.rotation = Quaternion.Slerp(cameraPivot.rotation, cameraLookRot, Time.deltaTime * rotationSpeed);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxRadius);
    }
}