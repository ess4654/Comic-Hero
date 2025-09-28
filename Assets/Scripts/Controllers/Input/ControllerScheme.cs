using System;
using System.Collections.Generic;
using UnityEngine;

namespace ComicHero
{
    /// <summary>
    ///     Container of controller inputs.
    /// </summary>
    [Serializable]
    public class ControllerScheme
    {
        /// <summary>
        ///     Types of input handled.
        /// </summary>
        [Serializable] public class InputKeys
        {
            public KeyCode moveLeft = KeyCode.A;
            public KeyCode moveRight = KeyCode.D;
            public KeyCode jumpDown = KeyCode.S;
            public KeyCode jump = KeyCode.Space;
            public KeyCode fire = KeyCode.Tab;
        }

        /// <summary>
        ///     Set or get the controls for the schema.
        /// </summary>
        public InputKeys[] Controls
        {
            get => controlSchemes;
            set => controlSchemes = value;
        }
        [SerializeField] private InputKeys[] controlSchemes;

        /// <summary>
        ///     Did the player jump this frame?
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>True if the player's jump key was pressed</returns>
        public bool Jumped(int playerIndex) =>
            Input.GetKeyDown(controlSchemes[playerIndex].jump);

        /// <summary>
        ///     Is left held down?
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>True if the player's left key was pressed</returns>
        public bool Left(int playerIndex) =>
            Input.GetKey(controlSchemes[playerIndex].moveLeft);

        /// <summary>
        ///     Is right held down?
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>True if the player's right key was pressed</returns>
        public bool Right(int playerIndex) =>
            Input.GetKey(controlSchemes[playerIndex].moveRight);

        /// <summary>
        ///     Is the down key pressed?
        /// </summary>
        /// <param name="playerIndex">Index of the player.</param>
        /// <returns>True if the player's down key was pressed.</returns>
        public bool Down(int playerIndex) =>
            Input.GetKeyDown(controlSchemes[playerIndex].jumpDown);
    }
}