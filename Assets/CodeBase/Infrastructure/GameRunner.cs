using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameRunner : MonoBehaviour
    {
        public GameObject GameBootstrap;

        private void Awake()
        {
            if (!FindObjectOfType<GameBootstrap>()) Instantiate(GameBootstrap);
        }
    }
}