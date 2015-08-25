using UnityEngine;

namespace Assets.scripts {
    public class CargoHold : ShipComponentBase {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this.Type = ShipComponentType.CARGO_HOLD;
            this.gameObject.layer = LayerMask.NameToLayer( this.Type.ToString() );
        }

        // Update is called once per frame
        void Update() {

        }

        void FixedUpdate() {

        }

        #endregion


        #region Fields



        #endregion

    }
}
