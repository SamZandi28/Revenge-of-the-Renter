using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class GuardBT : BehaviorTree.Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 5f;
    public static float fovRange = 5f;
    public static float attackRange = 1f;

    // Reference to the player transform
    private Transform playerTransform;

    static public bool playerInteractedEvidence = false;

    Animator animator;

    protected override Node SetupTree()
    {
        // Subscribe to the interaction event
        Interact.OnInteract += HandleInteractEvent;
        TakingPictures.OnInteract += HandleInteractEvent;

        Node root = new Selector(new List<Node>
        {
            // Follow the player if they have interacted with objects
            new Sequence(new List<Node>
            {
                new Conditional(() => playerInteractedEvidence), // Check if player interacted
                new TaskGoToTarget(transform), 
            }),
            // Continue patrolling if no interaction occurred
            new TaskPatrol(transform, waypoints, GetComponent<Animator>()),
        });
        
        return root;
    }

    // Event handler for interaction event
    private void HandleInteractEvent(Transform target)
    {
        // Set the player transform reference
        playerTransform = target;
        Debug.Log("Player interaction detected. Approaching player.");

        // Set the flag to indicate player interaction
        playerInteractedEvidence = true;
        StartCoroutine(ResetPlayerInteracted());

        // Debug log to check if playerInteracted flag is being set
        Debug.Log("Player interacted: " + playerInteractedEvidence);

        // Pass the player transform to TaskGoToTarget node
        _root.SetData("target", playerTransform);
    }

    private IEnumerator ResetPlayerInteracted()
    {
        // amount of time the landlord chases you
        yield return new WaitForSecondsRealtime(10);

        // Reset playerInteracted to false
        playerInteractedEvidence = false;
    }

    // Unsubscribe from the interaction event when the object is destroyed
    private void OnDestroy()
    {
        Interact.OnInteract -= HandleInteractEvent;
    }
}

