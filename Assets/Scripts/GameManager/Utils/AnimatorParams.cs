using UnityEngine;

namespace Utils
{
    public static class AnimatorParams
    {
        public static int Horizontal = Animator.StringToHash("Horizontal");
        public static int Vertical = Animator.StringToHash("Vertical");
        public static int Movement = Animator.StringToHash("Movement");
        public static int AnimSpeed = Animator.StringToHash("AnimSpeed");

        // Enemy battle parameter
        public static int Decision = Animator.StringToHash("Decision");
        public static int Life = Animator.StringToHash("Life");
        public static int Aggro = Animator.StringToHash("Aggro");
        public static int Trap = Animator.StringToHash("Trap");
        public static int Attack = Animator.StringToHash("Attack");
        public static int EnrageAttack = Animator.StringToHash("EnrageAttack");
        public static int HyperAttack = Animator.StringToHash("HyperAttack");

        // Projectile execution
        public static int Execute = Animator.StringToHash("Execute");
    }
}