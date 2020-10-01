using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    private Animator playerAnim;
    private Player player;

    [SerializeField]
    private float animationSpeed = 2.0f;
    [SerializeField]
    private float movementCooldown = 0.2f;

    private IEnumerator movementCoroutine;

    void Awake()
    {
        playerAnim = GetComponentInChildren<Animator>();
        playerAnim.speed = animationSpeed;

        player = GetComponent<Player>();
    }

    public void Up(InputAction.CallbackContext context)
    {
        StartMove(transform.forward);
    }
    public void Down(InputAction.CallbackContext context)
    {
        StartMove(-transform.forward);
    }
    public void Left(InputAction.CallbackContext context)
    {
        StartMove(-transform.right);
    }
    public void Right(InputAction.CallbackContext context)
    {
        StartMove(transform.right);
    }

    private void StartMove(Vector3 dir)
    {
        if (movementCoroutine != null) return;

        movementCoroutine = MovePlayer(dir);
        StartCoroutine(movementCoroutine);
    }
    private IEnumerator MovePlayer(Vector3 dir)
    {
        Transform dest = GetDestination(dir);
        if (dest == null)
        {
            Debug.Log("No destination found");
            movementCoroutine = null;
            yield break;
        }

        playerAnim.Play("Jump");

        float animLength = playerAnim.GetCurrentAnimatorStateInfo(0).length;
        Debug.Log(animLength);
        float time = 0.0f;
        Vector3 startPos = transform.position;
        while (time < animLength)
        {
            yield return null;
            time += Time.deltaTime;
            transform.position = Vector3.Lerp(startPos, dest.position, playerAnim.GetCurrentAnimatorStateInfo(0).normalizedTime);
        }
        transform.position = dest.position;

        player.FindTileBelow();
        Debug.Log(player.GetTileBelow().name);

        movementCoroutine = MovementCooldown();
        StartCoroutine(movementCoroutine);
    }
    private IEnumerator MovementCooldown()
    {
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length);
        movementCoroutine = null;
    }

    private Transform GetDestination(Vector3 dir)
    {
        //Moves the player to the specified tile
        Tile tileBelow = player.GetTileBelow();
        if (tileBelow == null)
        {
            Debug.LogError("No tile below, thrown from MovePlayer()");
            return null;
        }
        Tile destTile = tileBelow.GetAdjacentTile(dir);
        if (destTile == null)
        {
            // TODO: Stuff that happens when there's no tile in that direction
            // Stuff like playing error sound, etc
            Debug.Log("No destination tile.");
            return null;
        }
        return destTile.GetPlayerPoint();
    }

}
