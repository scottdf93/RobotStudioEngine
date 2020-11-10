﻿using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.IOSystemDomain;
using ABB.Robotics.RobotStudio.Controllers;
using RobotStudio.UI.Controller;
using System;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace RobotStudioEngine
{
    public class ControllersEngine
    {
        public enum ControllerEngineResult
        {
            Success,
            Fail
        }

        public ControllerEngineResult controllerEngineResult;

        /// <summary>
        /// Returns a collection of all running controllers.
        /// </summary>
        /// <param name="controllerType">Parse the ABB controller type ABB.Robotics.RobotStudio.Controllers.ControllerType</param>
        /// <param name="controllerType">VC = Virtual Controller</param>
        /// <param name="controllerType">RC = Real Controller</param>
        public ControllerInfoCollection GetAllAvaliableControllers(ControllerType controllerType)
        {
            ControllerInfoCollection controllerInfos = new ControllerInfoCollection();
            NetworkScanner networkScanner;

            try
            {
                networkScanner = new NetworkScanner();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("RobotStudio.Services.RobApi.Desktop"))
                {
                    MessageBox.Show("Failed to run a network scan due to missing dependancies;\n\nRobotStudio.Services.RobApi.Desktop.dll", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                controllerEngineResult = ControllerEngineResult.Fail;

                return controllerInfos;
            }

            networkScanner.Scan();

            if (controllerType == ControllerType.VC)
            {
                controllerInfos.AddRange(networkScanner.Controllers.Where(x => x.IsVirtual));
            }
            else if (controllerType == ControllerType.RC)
            {
                controllerInfos.AddRange(networkScanner.Controllers.Where(x => !x.IsVirtual));
            }
            else
            {
                MessageBox.Show("Argument \"controllerType\" missing from \"GetAllAvaliableControllers\" call.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            controllerEngineResult = ControllerEngineResult.Success;

            return controllerInfos;
        }

        /// <summary>
        /// Returns a controller.
        /// </summary>
        /// <param name="controllerType">Parse the ABB controller type ABB.Robotics.RobotStudio.Controllers.ControllerType</param>
        /// <param name="ipAddress">Get the controller from the IP address</param>
        public Controller GetController(ControllerType controllerType, IPAddress ipAddress)
        {
            Controller controller = null;
            NetworkScanner networkScanner = new NetworkScanner();
            networkScanner.Scan();

            if (controllerType == ControllerType.VC && ipAddress != null)
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => x.IsVirtual && x.IPAddress == ipAddress));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else if (controllerType == ControllerType.RC && ipAddress != null)
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => !x.IsVirtual && x.IPAddress == ipAddress));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else
            {
                if (controllerType != ControllerType.RC && controllerType != ControllerType.VC)
                {
                    MessageBox.Show("Argument \"controllerType\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller ", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (ipAddress == null)
                {
                    MessageBox.Show("Argument \"ipAddress\" missing from \"GetAllAvaliableControllers\" call. IP address is of types;\n\nSystem.Net.IPAddress", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (controllerType != ControllerType.RC && controllerType != ControllerType.VC && ipAddress == null)
                {
                    MessageBox.Show("Argument \"controllerType\" & \"ipAddress\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller\n\nIP address is of types;\n\nSystem.Net.IPAddress", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("No controller found!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            controllerEngineResult = ControllerEngineResult.Fail;

            return controller;

        }

        /// <summary>
        /// Returns a controller.
        /// </summary>
        /// <param name="controllerType">Parse the ABB controller type ABB.Robotics.RobotStudio.Controllers.ControllerType</param>
        /// <param name="macAddress">Get the controller from the MAC address</param>
        public Controller GetController(ControllerType controllerType, PhysicalAddress macAddress)
        {
            Controller controller = null;
            NetworkScanner networkScanner = new NetworkScanner();
            networkScanner.Scan();

            if (controllerType == ControllerType.VC && !string.IsNullOrEmpty(macAddress.ToString()))
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => x.IsVirtual && x.MacAddress == macAddress.ToString()));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else if (controllerType == ControllerType.RC && !string.IsNullOrEmpty(macAddress.ToString()))
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => !x.IsVirtual && x.MacAddress == macAddress.ToString()));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else
            {
                if (controllerType != ControllerType.RC && controllerType != ControllerType.VC)
                {
                    MessageBox.Show("Argument \"controllerType\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller ", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(macAddress.ToString()))
                {
                    MessageBox.Show("Argument \"macAddress\" missing from \"GetAllAvaliableControllers\" call. MAC Address is of types;\n\nSystem.String", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (controllerType != ControllerType.RC && controllerType != ControllerType.VC && !string.IsNullOrEmpty(macAddress.ToString()))
                {
                    MessageBox.Show("Argument \"controllerType\" & \"macAddress\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller\n\nMAC address is of types;\n\nSystem.Net.NetworkInformation.PhysicalAddress", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("No controller found!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            controllerEngineResult = ControllerEngineResult.Fail;

            return controller;
        }

        /// <summary>
        /// Returns a controller.
        /// </summary>
        /// <param name="controllerType">Parse the ABB controller type ABB.Robotics.RobotStudio.Controllers.ControllerType</param>
        /// <param name="guid">Get the controller from the GUID (global unique identifier)</param>
        public Controller GetController(ControllerType controllerType, Guid guid)
        {
            Controller controller = null;
            NetworkScanner networkScanner = new NetworkScanner();
            networkScanner.Scan();

            if (controllerType == ControllerType.VC && guid != null)
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => x.IsVirtual && x.SystemId == guid));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else if (controllerType == ControllerType.RC && guid != null)
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => !x.IsVirtual && x.SystemId == guid));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else
            {
                if (controllerType != ControllerType.RC && controllerType != ControllerType.VC)
                {
                    MessageBox.Show("Argument \"controllerType\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller ", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (guid != null)
                {
                    MessageBox.Show("Argument \"guid\" missing from \"GetAllAvaliableControllers\" call. GUID is of types;\n\nSystem.GUID", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (controllerType != ControllerType.RC && controllerType != ControllerType.VC && guid != null)
                {
                    MessageBox.Show("Argument \"controllerType\" & \"macAddress\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller\n\nGUID is of types;\n\nSystem.GUID", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("No controller found!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            controllerEngineResult = ControllerEngineResult.Fail;

            return controller;
        }

        /// <summary>
        /// Returns a controller.
        /// </summary>
        /// <param name="controllerType">Parse the ABB controller type ABB.Robotics.RobotStudio.Controllers.ControllerType</param>
        /// <param name="controllerName">Get the controller from the Controller Name</param>
        public Controller GetController(ControllerType controllerType, string controllerName)
        {
            Controller controller = null;
            NetworkScanner networkScanner = new NetworkScanner();
            networkScanner.Scan();

            if (controllerType == ControllerType.VC && !string.IsNullOrEmpty(controllerName))
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => x.IsVirtual && x.Name == controllerName));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else if (controllerType == ControllerType.RC && !string.IsNullOrEmpty(controllerName))
            {
                try
                {
                    controller = new Controller(networkScanner.Controllers.First(x => !x.IsVirtual && x.Name == controllerName));

                    controllerEngineResult = ControllerEngineResult.Success;

                    return controller;
                }
                catch
                {
                    controller = null;
                }
            }
            else
            {
                if (controllerType != ControllerType.RC && controllerType != ControllerType.VC)
                {
                    MessageBox.Show("Argument \"controllerType\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller ", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (string.IsNullOrEmpty(controllerName))
                {
                    MessageBox.Show("Argument \"controllerName\" missing from \"GetAllAvaliableControllers\" call. Controller Name is of types;\n\nSystem.String", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else if (controllerType != ControllerType.RC && controllerType != ControllerType.VC && string.IsNullOrEmpty(controllerName))
                {
                    MessageBox.Show("Argument \"controllerType\" & \"macAddress\" missing from \"GetAllAvaliableControllers\" call. Avaliable controllers are of types;\n\nControllerType.RC = Real Controller\nControllerType.VC = Virtual Controller\n\nControllerName is of types;\n\nSystem.String", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("No controller found!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            controllerEngineResult = ControllerEngineResult.Fail;

            return controller;
        }

        /// <summary>
        /// Returns a Signal from a specified controller. Returns signal or null
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="signalName">Get the signal from the signal Name</param>
        public Signal GetSignal(Controller controller, string signalName)
        {
            Signal signal = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetSignal\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signal;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.All).Count == 0)
            {
                MessageBox.Show("No signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signal;
            }

            try
            {
                signal = iOSystem.GetSignal(signalName);

                controllerEngineResult = ControllerEngineResult.Success;

                return signal;
            }
            catch
            {
                MessageBox.Show("The signal \"" + signalName + "\" does not exist.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signal;
            }
        }

        /// <summary>
        /// Returns all Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetAllSignal(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetAllSignal\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.All).Count == 0)
            {
                MessageBox.Show("No signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                controllerEngineResult = ControllerEngineResult.Success;

                return iOSystem.GetSignals(IOFilterTypes.All);
            }
        }

        /// <summary>
        /// Returns all Digital Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetAllDigialSignal(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetAllDigialSignal\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Digital).Count == 0)
            {
                MessageBox.Show("No Digital signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                controllerEngineResult = ControllerEngineResult.Success;

                return iOSystem.GetSignals(IOFilterTypes.Digital);
            }
        }

        /// <summary>
        /// Returns all Analog Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetAllAnalogSignal(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetAllAnalogSignal\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Analog).Count == 0)
            {
                MessageBox.Show("No Analog signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                controllerEngineResult = ControllerEngineResult.Success;

                return iOSystem.GetSignals(IOFilterTypes.Analog);
            }
        }

        /// <summary>
        /// Returns all Group Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetAllGroupSignal(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetAllGroupSignal\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Group).Count == 0)
            {
                MessageBox.Show("No Group signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                controllerEngineResult = ControllerEngineResult.Success;

                return iOSystem.GetSignals(IOFilterTypes.Group);
            }
        }

        /// <summary>
        /// Returns a collection of DI Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetDigitalInputs(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetDigitalInputs\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Digital).Count == 0)
            {
                MessageBox.Show("No Digital signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                try
                {
                    controllerEngineResult = ControllerEngineResult.Success;

                    return (SignalCollection)iOSystem.GetSignals(IOFilterTypes.Digital).Where(x => x.Type == SignalType.DigitalInput);
                }
                catch
                {
                    MessageBox.Show("No DI signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    controllerEngineResult = ControllerEngineResult.Fail;

                    return signals;
                }
            }
        }

        /// <summary>
        /// Returns a collection of DO Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetDigitalOutputs(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetDigitalOutputs\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Digital).Count == 0)
            {
                MessageBox.Show("No Digital signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                try
                {
                    controllerEngineResult = ControllerEngineResult.Success;

                    return (SignalCollection)iOSystem.GetSignals(IOFilterTypes.Digital).Where(x => x.Type == SignalType.DigitalOutput);
                }
                catch
                {
                    MessageBox.Show("No DO signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    controllerEngineResult = ControllerEngineResult.Fail;

                    return signals;
                }
            }
        }

        /// <summary>
        /// Returns a collection of AI Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetAnalogInputs(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetAnalogInputs\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Analog).Count == 0)
            {
                MessageBox.Show("No Analog signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                try
                {
                    controllerEngineResult = ControllerEngineResult.Success;

                    return (SignalCollection)iOSystem.GetSignals(IOFilterTypes.Analog).Where(x => x.Type == SignalType.AnalogInput);
                }
                catch
                {
                    MessageBox.Show("No AI signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    controllerEngineResult = ControllerEngineResult.Fail;

                    return signals;
                }
            }
        }

        /// <summary>
        /// Returns a collection of AO Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetAnalogOutputs(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetAnalogOutputs\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Analog).Count == 0)
            {
                MessageBox.Show("No Analog signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                try
                {
                    controllerEngineResult = ControllerEngineResult.Success;

                    return (SignalCollection)iOSystem.GetSignals(IOFilterTypes.Analog).Where(x => x.Type == SignalType.AnalogOutput);
                }
                catch
                {
                    MessageBox.Show("No AO signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    controllerEngineResult = ControllerEngineResult.Fail;

                    return signals;
                }
            }
        }

        /// <summary>
        /// Returns a collection of GO Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetGroupOutputs(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetGroupOutputs\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Group).Count == 0)
            {
                MessageBox.Show("No Group signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                try
                {
                    controllerEngineResult = ControllerEngineResult.Success;

                    return (SignalCollection)iOSystem.GetSignals(IOFilterTypes.Group).Where(x => x.Type == SignalType.GroupOutput);
                }
                catch
                {
                    MessageBox.Show("No GO signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    controllerEngineResult = ControllerEngineResult.Fail;

                    return signals;
                }
            }
        }

        /// <summary>
        /// Returns a collection of GI Signals from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        public SignalCollection GetGroupInputs(Controller controller)
        {
            SignalCollection signals = null;

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"GetGroupInputs\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }

            IOSystem iOSystem = controller.IOSystem;

            if (iOSystem.GetSignals(IOFilterTypes.Group).Count == 0)
            {
                MessageBox.Show("No Group signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return signals;
            }
            else
            {
                try
                {
                    controllerEngineResult = ControllerEngineResult.Success;

                    return (SignalCollection)iOSystem.GetSignals(IOFilterTypes.Group).Where(x => x.Type == SignalType.GroupInput);
                }
                catch
                {
                    MessageBox.Show("No GI signals found in the controller.", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    controllerEngineResult = ControllerEngineResult.Fail;

                    return signals;
                }
            }
        }

        /// <summary>
        /// Adds a new signal into a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="signal">The new signal to be added to the controller</param>
        private bool AddNewSignal(Controller controller, Signal signal)
        {
            return false;

            //if (controller == null || !controller.Connected)
            //{
            //    MessageBox.Show("Controller parsed to \"AddNewSignal\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    controllerEngineResult = ControllerEngineResult.Fail;

            //    return false;
            //}

            //if (signal == null)
            //{
            //    MessageBox.Show("Signal parsed to \"AddNewSignal\" is null!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    controllerEngineResult = ControllerEngineResult.Fail;

            //    return false;
            //}

            //if (string.IsNullOrEmpty(signal.Name))
            //{
            //    MessageBox.Show("Signal parsed to \"AddNewSignal\" does not have a name value!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    controllerEngineResult = ControllerEngineResult.Fail;

            //    return false;
            //}

            //if (signal.Type != SignalType.AnalogInput && signal.Type != SignalType.AnalogOutput && signal.Type != SignalType.DigitalInput && signal.Type != SignalType.DigitalOutput && signal.Type != SignalType.GroupInput && signal.Type != SignalType.GroupOutput && signal.Type != SignalType.Unknown)
            //{
            //    MessageBox.Show("Signal parsed to \"AddNewSignal\" does not have a signal type assigned!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    controllerEngineResult = ControllerEngineResult.Fail;

            //    return false;
            //}

            //if (string.IsNullOrEmpty(signal.Unit))
            //{
            //    MessageBox.Show("Signal parsed to \"AddNewSignal\" does not have a Unit value!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    controllerEngineResult = ControllerEngineResult.Fail;

            //    return false;
            //}

            //if (string.IsNullOrEmpty(signal.Unit))
            //{
            //    MessageBox.Show("Signal parsed to \"AddNewSignal\" does not have a Unit value!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    controllerEngineResult = ControllerEngineResult.Fail;

            //    return false;
            //}

            //if (!controller.IOSystem.GetUnits().Any(x => x.Name == signal.Unit))
            //{
            //    MessageBox.Show("Unit does not exist in Signal parsed to \"AddNewSignal\"!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            //    controllerEngineResult = ControllerEngineResult.Fail;

            //    return false;
            //}

            //string error = (string)null;

            //try
            //{
            //    string signalType = signal.Type.ToString();

            //    controller.MastershipPolicy = ControllerMastershipPolicy.Automatic;
            //    controller.Logon(UserInfo.DefaultUser);
            //    Mastership.Request(controller);

            //    error = signal.Name;

            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, rw6 ? "Device" : "Unit", (object)this.comboUnit.Text);
            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, rw6 ? "DeviceMap" : "UnitMap", (object)signal.UnitMap);
            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, "Invert", (object)this.checkInvert.Checked);

            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, rw6 ? "Label" : "SignalLabel", (object)this.textLabel.Text);
            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, "Category", (object)this.textCategory.Text);
            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, "SignalType", (object)signalType);
            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, "Default", (object)(float)this.numericDefault.Value);
            //    //controller.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, "Access", (object)this.comboAccessLevel.Text);

            //    //if (!rw6)
            //    //{
            //    //    this._conn.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, "Store", (object)this.checkStore.Checked);
            //    //    this._conn.Configuration.SetAttributeValue("EIO", "EIO_SIGNAL", signal.Name, "SysfailReset", (object)(this.comboFailValue.SelectedIndex == 0));
            //    //}
            //    signal = null;

            //}
            //catch (Exception ex)
            //{

            //}

            //return true;
        }

        /// <summary>
        /// Adds a new signal into a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="signalName">The name of the new signal to be added to the controller</param>
        /// <param name="signalType">The type of signal to be added to the controller</param>
        /// <param name="valueInverted">Specifies if the new signals default value is inverted</param>
        /// <param name="unit">Specifies the name of the IO Unit the new signals defined to inverted</param>
        /// <param name="mapping">Specifies the mapped value (bit) of the new signal in the IO unit</param>
        private bool AddNewSignal(Controller controller, string signalName, SignalType signalType, bool valueInverted, string unit, int mapping)
        {
            return false; // Temporary

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"LoadModule\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Adds a new collection of signals into a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="signals">The new collection of signal to be added to the controller</param>
        private bool AddNewSignals(Controller controller, SignalCollection signals)
        {
            return false; // Temporary

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"LoadModule\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes a signal from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="signal">The signal to be deleted from the controller</param>
        private bool DeleteSignal(Controller controller, Signal signal)
        {
            return false; // Temporary

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"LoadModule\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes a collection signal into a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="signals">The collection of signal to be deleted from the controller</param>
        private bool DeleteSignals(Controller controller, SignalCollection signals)
        {
            return false; // Temporary

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"LoadModule\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads a module into a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="moduleLocation">The directory of module (*.mod)</param>
        public bool LoadModule(Controller controller, string moduleLocation)
        {
            return false; // Temporary

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"LoadModule\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads a program into a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="programLocation">The directory of program (*.prg)</param>
        public bool LoadProgram(Controller controller, string programLocation)
        {
            return false; // Temporary

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"LoadModule\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return false;
            }

            return true;
        }

        /// <summary>
        /// Loads a configuration file into a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="cfgLocation">The directory of configuration file (*.cfg)</param>
        public bool LoadControllerConfiguration(Controller controller, string cfgLocation)
        {
            return false; // Temporary

            if (controller == null || !controller.Connected)
            {
                MessageBox.Show("Controller parsed to \"LoadModule\" is not connected!", "RobotStudioEngine", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                controllerEngineResult = ControllerEngineResult.Fail;

                return false;
            }

            return true;
        }
    }
}