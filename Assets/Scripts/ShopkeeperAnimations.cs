using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopkeeperAnimations : MonoBehaviour
{
    // randomly decide between 5 different animation behaviors.
    // 1. idle
    // 2. Looking left
    // 3. Looking right
    // 4. Looking left then right
    // 5. Looking right then left
    // The idle should also be played a random amount of times between 1-5 times.

    private Animator animator;
    private int idleCount = 0;
    private int idleMax = 5;
    private int idleMin = 1;

    // The animations should only be played one after the other, so we need to keep track of which animation is currently playing.
    private int currentAnimation = 0;

    
    private void FixedUpdate() {
        // Wait for the PlayAnimation() function to finish before we start playing the next animation.
        if (currentAnimation == 0) {
            // Randomly decide which animation to play.
            currentAnimation = Random.Range(1, 5);
        }

        // Play the animation.
        PlayAnimation(currentAnimation);

    }

    private void Start() {
        animator = GetComponent<Animator>();
    }

    private IEnumerator PlayAnimation(int currentAnimation)
    {
        switch (currentAnimation)
        {
            case 1:
                animator.SetTrigger("Idle");
                break;
            case 2:
                animator.SetTrigger("LookLeft");
                break;
            case 3:
                animator.SetTrigger("LookRight");
                break;
            case 4:
                animator.SetTrigger("LookLeftRight");
                break;
            case 5:
                animator.SetTrigger("LookRightLeft");
                break;
        }

        // All the animations are the same length (200 frames / 24 fps).
        yield return new WaitForSeconds(8.333f);
    }

}
