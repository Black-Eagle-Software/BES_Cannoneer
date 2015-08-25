using UnityEngine;

namespace Assets.extensions {
    public static class LayersExtensions {
        #region Constructors



        #endregion


        #region Properties



        #endregion


        #region Methods

        //taken from http://answers.unity3d.com/questions/168084/change-layer-of-child.html
        public static void ChangeLayersRecursively( this Transform trans, string name ) {
            trans.gameObject.layer = LayerMask.NameToLayer( name );
            foreach ( Transform child in trans ) {
                child.ChangeLayersRecursively( name );
            }
        }

        #endregion


        #region Fields



        #endregion

    }
}
