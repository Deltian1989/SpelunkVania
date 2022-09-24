using UnityEngine;

namespace SpelunkVania
{
    public class ParticleDestroyer : MonoBehaviour
    {
        void Start()
        {
            Destroy(this.gameObject, 1f);
        }
    }
}
