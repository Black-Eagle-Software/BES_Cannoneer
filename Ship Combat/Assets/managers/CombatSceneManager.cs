using System;
using Assets.factories;
using Assets.scripts;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.managers {
    public class CombatSceneManager : Singleton<CombatSceneManager> {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            this._sf = ShipFactory.Instance;
            this._playerShip = this._sf.CreatePlayerShip( new Vector2( -5f, 0 ), "Man-of-War", true );
            this._target = this._sf.CreateTargetShip( new Vector2( 5f, 0 ), "Schooner", true );
            //this._playerShip.GetComponent<Rigidbody2D>().AddForce( this._playerShip.transform.forward * 500 );
            var pssc = this._playerShip.GetComponent<ShipController>();
            pssc.AddNewTarget( this._target );
            this.RoundShotsRemaining.text = pssc.RoundShotsRemaining.ToString();
            pssc.FiredRoundShot += this.pssc_FiredRoundShot;
            this.SetTarget( "HULL" );
            this._target.GetComponent<ShipController>().AddNewTarget( this._playerShip );
        }

        // Update is called once per frame
        void Update() {

        }

        public void GunCrewsReady() {
            this._playerShip.GetComponent<ShipController>().GunCrewsReady();
            this._target.GetComponent<ShipController>().GunCrewsReady();
        }

        public void FireAtWill() {
            this.StartCoroutine( this._playerShip.GetComponent<ShipController>().FireAtWill() );
            this.StartCoroutine( this._target.GetComponent<ShipController>().FireAtWill() );
        }

        public void FireVolley() {
            this.StartCoroutine( this._playerShip.GetComponent<ShipController>().FireOnceThru() );
        }

        public void FireBroadside() {
            this.StartCoroutine( this._playerShip.GetComponent<ShipController>().FireBroadside() );
            this.StartCoroutine( this._target.GetComponent<ShipController>().FireBroadside() );
        }

        public void SetTarget( string target ) {
            Debug.Log( string.Format( "Targeting layer: {0}", target ) );
            var tType = ( ShipComponentType )Enum.Parse( typeof( ShipComponentType ), target );
            this._playerShip.GetComponent<ShipController>().PreferredTarget = tType;

            this.HullTargetButton.interactable = true;
            this.MastsTargetButton.interactable = true;
            this.RudderTargetButton.interactable = true;
            this.SailsTargetButton.interactable = true;
            switch ( tType ) {
                case ShipComponentType.HULL:
                    this.HullTargetButton.interactable = false;
                    break;
                case ShipComponentType.MAST:
                    this.MastsTargetButton.interactable = false;
                    break;
                case ShipComponentType.RUDDER:
                    this.RudderTargetButton.interactable = false;
                    break;
                case ShipComponentType.SAIL:
                    this.SailsTargetButton.interactable = false;
                    break;
            }
        }

        void pssc_FiredRoundShot( ShipController cntrlr ) {
            this.RoundShotsRemaining.text = cntrlr.RoundShotsRemaining.ToString();
        }

        #endregion


        #region Fields

        private ShipFactory _sf;
        private GameObject _playerShip;
        private GameObject _target;

        public Button HullTargetButton;
        public Button RudderTargetButton;
        public Button MastsTargetButton;
        public Button SailsTargetButton;

        public Text RoundShotsRemaining;

        #endregion

    }
}
