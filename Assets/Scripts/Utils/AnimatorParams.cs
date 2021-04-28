using UnityEngine;

namespace Utils
{
    public static class AnimatorParams
    {
        public static int Horizontal = Animator.StringToHash("Horizontal");
        public static int Vertical = Animator.StringToHash("Vertical");
        public static int State = Animator.StringToHash("State");
        public static int AnimSpeed = Animator.StringToHash("AnimSpeed");
    }
}