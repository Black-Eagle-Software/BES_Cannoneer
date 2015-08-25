using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.factories;
using UnityEngine;

namespace Assets.scripts {
    public class ShipController : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties

        public ShipComponentType PreferredTarget {
            get { return this._prefTarget; }
            set {
                this._prefTarget = value;
                foreach ( var gunPosition in this.GunPositions ) {
                    gunPosition.GetComponent<GunCrew>().TargetType = value;
                }
            }
        }

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

        public void GunCrewsReady() {
            foreach ( var gunPosition in this.GunPositions ) {
                if ( this.RoundShotsRemaining <= 0 ) {
                    break;
                }
                gunPosition.GetComponent<GunCrew>().SignalReady();
            }
        }

        public IEnumerator FireOnceThru() {
            //should go thru all gun positions and set to ready
            //then fire, then decrement remaining shots
            yield return new WaitForFixedUpdate();
            foreach ( var gunPosition in this.GunPositions ) {
                if ( this.RoundShotsRemaining <= 0 ) {
                    break;
                }
                var gc = gunPosition.GetComponent<GunCrew>();
                if ( !gc.IsFacingTarget ) {
                    break;
                }
                this.StartCoroutine( this.FireOffShot( gc, ShotType.ROUND ) );
            }
        }

        private IEnumerator FireOffShot( GunCrew gCrew, ShotType sType ) {
            if ( !gCrew.IsReady ) {
                gCrew.SignalReady();
                yield return new WaitForSeconds( gCrew.ReloadTime );
            }
            this.FireShot( gCrew, sType );
        }

        public IEnumerator FireAtWill() {
            //this should get rewritten to pull logic out of the guncrew
            yield return new WaitForFixedUpdate();
            this._shouldFireAtWill = !this._shouldFireAtWill;
            foreach ( var gunPosition in this.GunPositions ) {
                if (this.RoundShotsRemaining <= 0) {
                    this._shouldFireAtWill = !this._shouldFireAtWill;
                    break;
                }
                this.StartCoroutine( gunPosition.GetComponent<GunCrew>().FireAtWill( this._shouldFireAtWill, ShotType.ROUND ) );
            }
            /*foreach ( var gunPosition in this.GunPositions ) {
                if ( this.RoundShotsRemaining <= 0 ) {
                    this._shouldFireAtWill = !this._shouldFireAtWill;
                    break;
                }
                do {
                    yield return new WaitForFixedUpdate();
                    var gc = gunPosition.GetComponent<GunCrew>();
                    if ( !gc.IsReady ) {
                        gc.SignalReady();
                    }
                    yield return new WaitForFixedUpdate();
                    this.FireShot( gc, ShotType.ROUND );
                } while ( this._shouldFireAtWill );
            }*/
            /*do {
                foreach ( var gunPosition in this.GunPositions ) {
                    if ( this.RoundShotsRemaining <= 0 ) {
                        this._shouldFireAtWill = !this._shouldFireAtWill;
                        break;
                    }
                    yield return new WaitForFixedUpdate();
                    var gc = gunPosition.GetComponent<GunCrew>();
                    if ( !gc.IsFacingTarget ) {
                        break;
                    }
                    this.StartCoroutine(this.FireOffShot(gc, ShotType.ROUND));
                }
            } while ( this._shouldFireAtWill );*/
        }

        public IEnumerator FireBroadside() {
            yield return new WaitForFixedUpdate();  //so the physics system doesn't get confused when we spawn a bunch of new bodies
            if ( this.GunPositions.Any( gp => !gp.GetComponent<GunCrew>().IsReady && gp.GetComponent<GunCrew>().IsFacingTarget ) ) {
                var maxReload = this.GunPositions.Select( gp => gp.GetComponent<GunCrew>().ReloadTime ).Max();
                //tell all the gun crews to get ready
                this.GunCrewsReady();
                yield return new WaitForSeconds( maxReload + 0.5f );
            }
            //...then have all of them fire at once
            foreach ( var gunPosition in this.GunPositions ) {
                if ( this.RoundShotsRemaining <= 0 ) {
                    break;
                }
                gunPosition.GetComponent<GunCrew>().FireShot( ShotType.ROUND );
            }
            this.GunCrewsReady();
        }

        public void AddGunPosition( GameObject gp ) {
            this.GunPositions.Add( gp );
            gp.transform.parent = this.transform;
        }

        public void AddNewTarget( GameObject target ) {
            this.Target = target;
            var targVect = new Vector2( this.Target.transform.position.x - this.transform.position.x,
                this.Target.transform.position.y - this.transform.position.y );
            foreach ( var gunPosition in this.GunPositions ) {
                var gcTargVect = new Vector2( this.Target.transform.position.x - gunPosition.transform.position.x,
                this.Target.transform.position.y - gunPosition.transform.position.y );
                var gc = gunPosition.GetComponent<GunCrew>();
                gc.IsFacingTarget = gcTargVect.magnitude < targVect.magnitude;
            }
        }

        void FireShot( GunCrew gCrew, ShotType sType ) {
            gCrew.FireShot( sType );
            this.RoundShotsRemaining--;
            this.OnFiredRoundShot( this );
        }

        protected virtual void OnFiredRoundShot( ShipController cntrlr ) {
            var handler = this.FiredRoundShot;
            if ( handler != null ) {
                handler( cntrlr );
            }
        }

        #endregion


        #region Fields

        public delegate void FireRoundShot( ShipController cntrlr );
        public event FireRoundShot FiredRoundShot;

        private bool _shouldFireAtWill;
        private ShipComponentType _prefTarget;

        public ShipType Type;
        public List<GameObject> GunPositions;
        public GameObject Target;

        public int RoundShotsRemaining = 100;
        public int GrapeShotsRemaining = 40;
        public int ChainShotsRemaining = 25;

        #endregion


    }
}
