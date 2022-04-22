using System.Collections;
using UnityEngine;

namespace DefaultNamespace
{
    public class Utils
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

        public static IEnumerator StopTimeForWhile(float time)
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(time);
            Time.timeScale = 1;
        }

        public static void StopTime()
        {
            Time.timeScale = 0;
        }
        
        public static void ResumeTime()
        {
            Time.timeScale = 1;
        }

        public static IEnumerator SlowTime(float time, float coefficient)
        {
            Time.timeScale = coefficient;
            yield return new WaitForSeconds(time);
            Time.timeScale = 1;
        }
    }
}