using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SavageCodes.Frameworks.Weapons
{
    public class WPFireSocketsComponent : MonoBehaviour
    {
        [SerializeField] private Transform[] _fireSockets;
        private int _currentSocketIndex;

        public Transform GetSocket(int index)
        {
            return _fireSockets[index];
        }

        public void RegisterFireSockets(WPFireSocket[] sockets)
        {
            _fireSockets = sockets.Select(x => x.transform).ToArray();
        }

        public Transform[] GetAllSockets()
        {
            return _fireSockets;
        }

        public Transform GetSocket()
        {
            CycleCannons();
            return _fireSockets[_currentSocketIndex];
        }

        void CycleCannons()
        {
            _currentSocketIndex++;
            _currentSocketIndex = _currentSocketIndex == _fireSockets.Length ? 0 : _currentSocketIndex;
        }
    }
}
