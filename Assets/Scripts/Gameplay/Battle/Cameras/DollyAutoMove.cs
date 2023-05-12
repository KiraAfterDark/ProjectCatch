using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

namespace ProjectCatch
{
    public class DollyAutoMove : MonoBehaviour
    {
        [SerializeField]
        private float speed = 0.25f;

        private enum WrapStyle
        {
            None = 0,
            Repeat = 1,
            Yoyo = 2,
        }

        [SerializeField]
        private new CinemachineVirtualCamera camera;

        private CinemachineTrackedDolly dolly;

        private void Awake()
        {
            dolly = camera.GetCinemachineComponent<CinemachineTrackedDolly>();
        }

        private void Update()
        {
            if (dolly)
            {
                dolly.m_PathPosition += speed * Time.deltaTime;
                dolly.m_PathPosition %= 1;
            }
        }
    }
}
