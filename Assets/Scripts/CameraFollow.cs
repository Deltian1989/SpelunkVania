using UnityEngine;

namespace SpelunkVania
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private float followSpeed = 0.3f;

        [SerializeField]
        private GameObject target;

        private TerrainProperties terrainProperties;

        void Awake()
        {
            terrainProperties = FindObjectOfType<TerrainProperties>();
        }

        void LateUpdate()
        {
            var cameraPosition = Vector3.Lerp(transform.position, target.transform.position, followSpeed * Time.deltaTime);

            var x = Mathf.Clamp(cameraPosition.x, terrainProperties.LeftBound, terrainProperties.RightBound);
            var y = Mathf.Clamp(cameraPosition.y, terrainProperties.LowerBound, terrainProperties.UpperBound);

            transform.position = new Vector3(x, y, transform.position.z);
        }
    }
}

