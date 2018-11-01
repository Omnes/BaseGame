using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

namespace BaseGame.Assets.Scripts
{
    public static class Util
    {

        private static float GetComparableDistance(Vector3 v1, Vector3 v2){
            return (v1-v2).sqrMagnitude;
        }

        public static GameObject FindClosestGameObjectWithTag(Vector3 position, string tag){
            var potentialTargets = GameObject.FindGameObjectsWithTag(tag);
            return FindClosestGameObject(position, potentialTargets);
        }

        public static GameObject FindClosestGameObject(Vector3 position, IEnumerable<GameObject> potentialTargets){
            return potentialTargets
                .Select(g => new Tuple<float, GameObject>(GetComparableDistance(g.transform.position, position), g))
                .OrderBy(t => t.Item1)
                .Select(t => t.Item2)
                .FirstOrDefault();
	    }
    }
}