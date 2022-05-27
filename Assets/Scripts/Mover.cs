using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform player;

    [SerializeField] private SwipeDetector swipeDetector;
    [SerializeField] private Platform currentPlatform;

    private void Awake()
    {
        swipeDetector.OnSwipe += Move;
    }

    private void Move(Side moveSide)
    {
        //SidePlatform nextPlatform = currentPlatform.sidePlatfroms.Single(platfrom => platfrom.side == moveSide);

        //if (nextPlatform == null)
        //    return;

        //currentPlatform = nextPlatform.platform;

        //currentPlatform.MoveTo(player);
        Debug.Log(moveSide);
    }
}
