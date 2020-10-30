using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.IOSystemDomain;
using ABB.Robotics.RobotStudio.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotStudioEngine
{
    public class ControllersEngine
    {
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

                }
                catch
                {
                    controller = null;
                }
                controller = new Controller(networkScanner.Controllers.First(x => !x.IsVirtual && x.IPAddress == ipAddress));
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

            return controller;
        }

        /// <summary>
        /// Returns a Signal from a specified controller.
        /// </summary>
        /// <param name="controller">Parse the ABB controller ABB.Robotics.Controllers.Controller. VC or RC</param>
        /// <param name="signalName">Get the signal from the signal Name</param>
        public Signal GetSignal(Controller controller, string signalName)
        {
            Signal signal = null;

            return signal;
        }
    }
}