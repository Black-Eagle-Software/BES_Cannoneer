using System.Collections;
using UnityEngine;

namespace Assets.scripts {
    public class ProjectileBase : MonoBehaviour {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        // Use this for initialization
        protected virtual void Start() {
            
        }

        // Update is called once per frame
        protected virtual void Update() {

        }

        protected virtual void FixedUpdate() {

        }

        protected virtual void OnCollisionEnter2D( Collision2D col ) {
            //need to filter out anything on our ship
            if (col.gameObject.GetComponent<ShipComponentBase>().ParentObject==this.ParentObject) {
                return;
            }
            this.CollisionTarget = col;
            this.Position = this.transform.position;
            this.SpawnExplosion();
            Destroy( this.gameObject );
        }

        protected virtual void SpawnExplosion() {
            var e = ( GameObject )Instantiate( this.Explosion, this.Position, Quaternion.identity );
        }

        public void InitProjectile(Vector2 velocity, GameObject parent, ShipComponentType target=ShipComponentType.HULL) {
            var rgdb2D = this.GetComponent<Rigidbody2D>();
            rgdb2D.velocity += velocity;
            this.ParentObject = parent;
            this.TargetType = target;
            this.gameObject.layer = LayerMask.NameToLayer(this.TargetType.ToString());
            this.GetComponent<SpriteRenderer>().sortingLayerName = this.TargetType.ToString();
        }

        #endregion


        #region Fields
        
        protected Vector2 Position;
        protected Collision2D CollisionTarget;

        public GameObject ParentObject;
        public GameObject Explosion;

        public ShipComponentType TargetType;

        #endregion

    }
}
