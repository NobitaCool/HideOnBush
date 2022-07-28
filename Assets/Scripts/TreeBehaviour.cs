using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MonoBehaviour
{
    #region Variable
        #region private
        // Cây
        [SerializeField] private GameObject tree;

        // Gốc
        [SerializeField] private GameObject stump;

        // Hiệu ứng
        [SerializeField] private ParticleSystem effect;

        // Tái sinh chưa?
        [SerializeField] private bool isReborn = false;

        // Player tag name
        private const string PLAYER_TAG = "Player";
        #endregion
    #endregion

    #region Function
        void OnTriggerEnter2D(Collider2D other)
        {
            isReborn = !isReborn;
            // Condition: !player do nothing
            if (!other.gameObject.CompareTag(PLAYER_TAG)) return;

            // Spawn cây 
            // Instantiate(effect, transform.position, Quaternion.identity);
            tree.SetActive(isReborn);

            // Xóa gốc 
            stump.SetActive(!isReborn);
        }
    #endregion

}