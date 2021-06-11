using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        public GameObject LeftGate;
        public GameObject RightGate;

        public void CloseRightGate()
        {
            RightGate.SetActive(true);
        }
        
        public void CloseLeftGate()
        {
            LeftGate.SetActive(true);
        }
        
        public void OpenRightGate()
        {
            RightGate.SetActive(false);
        }
        
        public void OpenLeftGate()
        {
            LeftGate.SetActive(false);
        }
        
    }
}