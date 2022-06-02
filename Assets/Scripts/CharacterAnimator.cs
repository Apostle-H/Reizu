using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator animator;

    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private PlayerCombat playerCombat;

    private void OnEnable()
    {
        playerMover.onStartMovement += Move;
        playerCombat.onPunch += Punch;
    }
    
    private void OnDisable()
    {
        playerMover.onStartMovement -= Move;
        playerCombat.onPunch -= Punch;
    }

    private void Move(Transform player)
    {
        StartCoroutine(Animate(player));
    }

    private IEnumerator Animate(Transform player)
    {
        Vector3 prevPos = player.position;
        yield return new WaitForSeconds(.02f);
        animator.SetFloat("Speed", (player.position - prevPos).magnitude / .02f);
        StartCoroutine(Animate(player));
    }

    private void Punch()
    {
        animator.SetTrigger("Punch");
    }
}
