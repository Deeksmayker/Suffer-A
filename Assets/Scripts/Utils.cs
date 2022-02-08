using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public static class Utils
    {
        public static IEnumerator SmoothMoveToTarget(Transform obj, Vector3 target, float speed = 1)
        {
            while (obj.position != target)
            {
                obj.position = Vector3.Lerp(obj.position, target, Time.deltaTime * speed);
                yield return new WaitForFixedUpdate();
            }
        }

        public static IEnumerator MoveToTarget(Transform obj, Vector3 target, float speed = 1)
        {
            while (obj.position != target)
            {
                obj.position = Vector3.MoveTowards(obj.position, target, Time.deltaTime * speed);
                yield return new WaitForFixedUpdate();
            }
        }
    }
}