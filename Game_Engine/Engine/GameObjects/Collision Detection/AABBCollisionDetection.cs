using System;
using System.Collections.Generic;

namespace Game_Engine.Engine.GameObjects
{
    public class AABBCollisionDetection<P, A>
        where P : BaseGameObject
        where A : BaseGameObject
    {
        private List<P> _passiveObjects;

        public AABBCollisionDetection(List<P> passiveObjects)
        {
            _passiveObjects = passiveObjects;
        }

        public void DetectCollision(A activeObject, Action<P, A> collisionhandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                if (DetectCollision(passiveObject, activeObject))
                {
                    collisionhandler(passiveObject, activeObject);
                }
            }
        }

        public void DetectCollision(Action<P, P> collisionhandler)
        {
            for (int i = 0; i < _passiveObjects.Count - 1; i++)
            {
                if (DetectCollsion(_passiveObjects[i],_passiveObjects[i + 1]))
                {
                    collisionhandler(_passiveObjects[i], _passiveObjects[i + 1]);
                }
            }
        }

        public void DetectCircleCollision(List<A> activeObjects, Action<P,A> collisionHandler)
        {
            foreach (var passiveObject in _passiveObjects)
            {
                foreach (var activeObject in activeObjects)
                {
                    if (DetectCircleCollision(passiveObject, activeObject))
                    {
                        collisionHandler(passiveObject, activeObject);
                    }
                }
            }
        }

        private bool DetectCircleCollision(P passiveObject, A activeObject)
        {
            foreach (var bb in activeObject.BoundingBoxes)
            {
                foreach (var cc in passiveObject.CircleColiders)
                {
                    if (cc.Intersects(bb.Rectangle))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DetectCollision(P passiveObject, A activeObject)
        {
            foreach (var bb in passiveObject.BoundingBoxes)
            {
                foreach (var otherBB in activeObject.BoundingBoxes)
                {
                    if (bb.ColdidesWidth(otherBB))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool DetectCollsion(P passiveObject1, P passiveObject2)
        {
            foreach (var bb in passiveObject1.BoundingBoxes)
            {
                foreach (var otherBB in passiveObject2.BoundingBoxes)
                {
                    if (bb.ColdidesWidth(otherBB))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
