using UnityEngine;

namespace Assets.scripts {
    public class AutoKillParticleSystem : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this._ps = this.GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update() {
            if ( this._ps && !this._ps.IsAlive() ) {
                Destroy( this.gameObject );
            }
        }

        #endregion


        #region Fields

        private ParticleSystem _ps;

        #endregion
    }
}
