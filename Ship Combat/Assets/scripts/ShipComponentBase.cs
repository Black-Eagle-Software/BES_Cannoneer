using UnityEngine;

namespace Assets.scripts {
    public class ShipComponentBase : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {
        
        }

        #endregion


        #region Fields

        public string Name;
        public int Health;
        public int RepairCost;
        public ShipComponentType Type;
        public GameObject ParentObject;

        #endregion

    }
}
