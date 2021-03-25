using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.IOSystemDomain;
using RobotStudioEngine.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotStudioEngine.Common.EventArguments
{
    /// <summary>
    /// Event arguements for the Container Verified callback
    /// </summary>
    public class ControllerCallBackEventArgs : EventArgs
    {
        /// <summary>
        /// The result of the function called
        /// </summary>
        public RobotStudioEngineResult FunctionResultResult;

        /// <summary>
        /// Description of a failed result
        /// </summary>
        public string Output;

        /// <summary>
        /// The controllers found
        /// </summary>
        public ControllerInfoCollection Controllers;

        /// <summary>
        /// The controller found
        /// </summary>
        public Controller Controller;

        /// <summary>
        /// The signals found
        /// </summary>
        public SignalCollection Signals;

        /// <summary>
        /// The signal found
        /// </summary>
        public Signal Signal;
    }
}
