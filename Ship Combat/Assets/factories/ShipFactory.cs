using System.Linq;
using Assets.managers;
using Assets.scripts;
using UnityEngine;
using Assets.extensions;

namespace Assets.factories {
    public class ShipFactory : Singleton<ShipFactory> {
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

        public GameObject CreateTargetShip( Vector2 origin, string typeName, bool verticalOrientation = false ) {
            var st = this.ShipTypes.FirstOrDefault( t => t.Name == typeName );
            if ( st == null ) {
                return null; //error of some sort :O
            }
            var rot = verticalOrientation ? 270f : 0f;
            var pt = ( GameObject )Instantiate( st.Token, origin, Quaternion.AngleAxis( rot, new Vector3( 0f, 0f, 1f ) ) );
            var sc = pt.GetComponent<ShipController>();
            sc.Type = st;
            //this.SpawnGunCrews( sc, verticalOrientation );
            pt.layer = LayerMask.NameToLayer("playerTargetObject");
            //pt.transform.ChangeLayersRecursively("playerTargetObject");
            return pt;
        }

        public GameObject CreatePlayerShip( Vector2 origin, string typeName, bool verticalOrientation=false ) {
            var st = this.ShipTypes.FirstOrDefault( t => t.Name == typeName );
            if ( st == null ) {
                return null; //error of some sort :O
            }
            var rot = verticalOrientation ? 270f : 0f;
            var pt = ( GameObject )Instantiate( st.Token, origin, Quaternion.AngleAxis( rot, new Vector3( 0f, 0f, 1f ) ) );
            var sc = pt.GetComponent<ShipController>();
            sc.Type = st;
            //this.SpawnGunCrews( sc, verticalOrientation );
            pt.layer = LayerMask.NameToLayer( "playerObject" );
            //pt.transform.ChangeLayersRecursively("playerObject");
            return pt;
        }

        /*public void SpawnGunCrews( ShipController ship, bool useLeftRightPlacements = false ) {
            var sr = ship.GetComponent<SpriteRenderer>();
            var width = sr.bounds.extents.x * 2;
            var height = sr.bounds.extents.y * 2;
            var origin = sr.bounds.center;
            if ( !useLeftRightPlacements ) {
                this.PlaceGunsAlongTopBottom( ship, height, width, origin );
            } else {
                this.PlaceGunsAlongLeftRigth( ship, height, width, origin );
            }
        }*/

       /* private void PlaceGunsAlongTopBottom( ShipController ship, float height, float width, Vector2 origin ) {
            for ( var i = 0; i < ship.Type.DeckCount; i++ ) {
                //want to space out gun crews on either side of the ship (left/right)
                //center of gun crew should be just inside the ship bounds (roughly)
                //add + side guns
                var gunsPerSide = ship.Type.GunsPerDeck / 2f;
                var spacing = ( width * 0.75f ) / gunsPerSide;
                var gcXoffset = gunsPerSide % 2 != 0 ? ( int )( gunsPerSide / 2 ) * spacing : ( ( gunsPerSide - 1 ) / 2 ) * spacing;
                var gcXorigin = origin.x - gcXoffset;
                for ( var j = 0; j < gunsPerSide; j++ ) {
                    var gcX = gcXorigin + ( spacing * j );
                    var gcY = origin.y + ( height * 0.4f );
                    var gc = ( GameObject )Instantiate( this.GunCrewToken, new Vector2( gcX, gcY ), Quaternion.identity );
                    ship.AddGunPosition( gc );
                }
                //add - side guns
                for ( var k = 0; k < gunsPerSide; k++ ) {
                    var gcX = gcXorigin + ( spacing * k );
                    var gcY = origin.y - ( height * 0.4f );
                    var gc = ( GameObject )Instantiate( this.GunCrewToken, new Vector2( gcX, gcY ), Quaternion.AngleAxis( 180f, new Vector3( 0f, 0f, 1f ) ) );
                    ship.AddGunPosition( gc );
                }
            }
        }*/

        /*private void PlaceGunsAlongLeftRigth( ShipController ship, float height, float width, Vector2 origin ) {
            for ( var i = 0; i < ship.Type.DeckCount; i++ ) {
                //want to space out gun crews on either side of the ship (left/right)
                //center of gun crew should be just inside the ship bounds (roughly)
                //add + side guns
                var gunsPerSide = ship.Type.GunsPerDeck / 2f;
                var spacing = ( height * 0.75f ) / gunsPerSide;
                var gcYoffset = gunsPerSide % 2 != 0 ? ( int )( gunsPerSide / 2 ) * spacing : ( ( gunsPerSide - 1 ) / 2 ) * spacing;
                var gcYorigin = origin.y - gcYoffset;
                for ( var j = 0; j < gunsPerSide; j++ ) {
                    var gcY = gcYorigin + ( spacing * j );
                    var gcX = origin.x + ( width * 0.4f );
                    var gc = ( GameObject )Instantiate( this.GunCrewToken, new Vector2( gcX, gcY ), Quaternion.AngleAxis( 270f, new Vector3( 0f, 0f, 1f ) ) );
                    ship.AddGunPosition( gc );
                }
                //add - side guns
                for ( var k = 0; k < gunsPerSide; k++ ) {
                    var gcY = gcYorigin + ( spacing * k );
                    var gcX = origin.x - ( width * 0.4f );
                    var gc = ( GameObject )Instantiate( this.GunCrewToken, new Vector2( gcX, gcY ), Quaternion.AngleAxis( -270f, new Vector3( 0f, 0f, 1f ) ) );
                    ship.AddGunPosition( gc );
                }
            }
        }*/

        #endregion


        #region Fields

        //public GameObject PlayerShipToken;
        //public GameObject GunCrewToken;
        //public GameObject TargetShipToken;
        public ShipType[] ShipTypes;    //eventually have this read in from external file

        #endregion

    }
}
