using BulletRPG.Gear;
using UnityEngine;
using UnityEngine.InputSystem;

namespace BulletRPG.Temp
{
    public class GameSettings : MonoBehaviour
    {
        [Range(30, 250)]
        [SerializeField] int targetFrameRate;
        public InventoryObject playerInventory;
        PlayerInputActions playerInputActions;
        private void Awake()
        {
            playerInputActions = new PlayerInputActions();
        }
        void Start()
        {
            Application.targetFrameRate = targetFrameRate;
            Debug.Log(Application.companyName);
            playerInputActions.Player.Enable();
            playerInputActions.Player.SaveInventory.performed += SavePlayerInventory;
            playerInputActions.Player.LoadInventory.performed += LoadPlayerInventory;

        }
        public void SavePlayerInventory(InputAction.CallbackContext context)
        {
            playerInventory.Save();
        }
        public void LoadPlayerInventory(InputAction.CallbackContext context)
        {
            playerInventory.Load();
        }

    }
}

