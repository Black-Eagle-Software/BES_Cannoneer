using System;
using System.Collections;
using Assets.factories;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Assets.scripts {
    public class GunCrew : ShipComponentBase {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        void Start() {
            var sr = this.GetComponent<SpriteRenderer>();
            if ( sr != null ) {
                sr.sortingLayerName = this.Deck.ToString();
            }
            this.ReloadTime = Random.Range( 3f, 7f );
            this.RepairCost = 75;
            this.Type = ShipComponentType.GUN_CREW;
            this.gameObject.layer = LayerMask.NameToLayer("HULL");
        }

        // Update is called once per frame
        void Update() {
            if ( !this.IsReady && this.GetComponent<SpriteRenderer>().color != Color.red ) {
                this.GetComponent<SpriteRenderer>().color = Color.red;
            } else if ( this.IsReady && this.GetComponent<SpriteRenderer>().color != Color.green ) {
                this.GetComponent<SpriteRenderer>().color = Color.green;
            }
        }

        void FixedUpdate() {

        }

        public void SignalReady() {
            if ( this._isReloading || !this.IsFacingTarget ) return;
            this.StartCoroutine( this.Reload() );
        }

        public IEnumerator FireAtWill( bool shouldFireAtWill, ShotType sType ) {
            this.ShouldFireAtWill = shouldFireAtWill;
            do {
                yield return new WaitForFixedUpdate();
                if ( !this.IsReady ) {
                    this.SignalReady();
                }
                yield return new WaitForFixedUpdate();
                this.FireShot( sType );
            } while ( this.ShouldFireAtWill );
        }

        public void FireShot( ShotType shot, int powderCharge = 5 ) {
            if ( !this.IsReady || this._isFiring ) return;
            this.IsReady = false;
            this._isFiring = true;
            var ossVect = new Vector2( this.ShotSpawnLocation.transform.position.x - this.transform.position.x,
                this.ShotSpawnLocation.transform.position.y - this.transform.position.y );
            switch ( shot ) {
                case ShotType.CHAIN:
                    break;
                case ShotType.GRAPE:
                    break;
                case ShotType.ROUND:
                    this.FireRoundShot( ossVect.normalized * powderCharge );
                    break;
                default:
                    throw new ArgumentOutOfRangeException( "shot" );
            }
            this._isFiring = false;
        }

        IEnumerator Reload() {
            this._isReloading = true;
            yield return new WaitForSeconds( this.ReloadTime );
            this.IsReady = true;
            this._isReloading = false;
        }

        void FireRoundShot( Vector2 velocity ) {
            var ps = ( GameObject )Instantiate( this.PowderSmoke, this.ShotSpawnLocation.transform.position, Quaternion.identity );
            var rs = ( GameObject )Instantiate( this.RoundShot, this.ShotSpawnLocation.transform.position, Quaternion.identity );
            rs.GetComponent<ProjectileBase>().InitProjectile( velocity, this.transform.parent.gameObject.transform.parent.gameObject, this.TargetType );
            var audSrc = this.GetComponent<AudioSource>();
            audSrc.PlayOneShot( this.FireSound );
        }

        #endregion


        #region Fields

        private bool _isFiring;
        private bool _isReloading;

        public GameObject ShotSpawnLocation;
        public GunCrewDeckAssignment Deck;
        public GameObject RoundShot;
        public GameObject PowderSmoke;
        public bool IsReady;
        public float ReloadTime;
        public bool ShouldFireAtWill;
        public bool IsFacingTarget;
        public AudioClip FireSound;
        public ShipComponentType TargetType;

        #endregion
    }
}
