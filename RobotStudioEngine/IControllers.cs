using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ABB.Robotics.Controllers;
using ABB.Robotics.Controllers.Discovery;
using ABB.Robotics.Controllers.IOSystemDomain;
using ABB.Robotics.RobotStudio.Controllers;

namespace RobotStudioEngine
{
    interface IControllers
    {
        event EventHandler ControllerEngineCallBack;

        ControllerInfoCollection GetAllAvaliableControllers(ControllerType controllerType);

        Controller GetController(ControllerType controllerType, IPAddress ipAddress);

        Controller GetController(ControllerType controllerType, PhysicalAddress macAddress);

        Controller GetController(ControllerType controllerType, Guid guid);

        Controller GetController(ControllerType controllerType, string controllerName);

        Signal GetSignal(Controller controller, string signalName);

        SignalCollection GetAllSignal(Controller controller);

        SignalCollection GetAllDigialSignal(Controller controller);

        SignalCollection GetAllAnalogSignal(Controller controller);

        SignalCollection GetAllGroupSignal(Controller controller);

        SignalCollection GetDigitalInputs(Controller controller);

        SignalCollection GetDigitalOutputs(Controller controller);

        SignalCollection GetAnalogInputs(Controller controller);

        SignalCollection GetAnalogOutputs(Controller controller);

        SignalCollection GetGroupOutputs(Controller controller);

        SignalCollection GetGroupInputs(Controller controller);

        bool AddNewSignal(Controller controller, Signal signal);

        bool AddNewSignal(Controller controller, string signalName, SignalType signalType, bool valueInverted, string unit, int mapping);
        
        bool AddNewSignals(Controller controller, SignalCollection signals);

        bool DeleteSignal(Controller controller, Signal signal);

        bool DeleteSignals(Controller controller, SignalCollection signals);

        bool LoadModule(Controller controller, string moduleLocation);

        bool LoadProgram(Controller controller, string programLocation);

        bool LoadControllerConfiguration(Controller controller, string cfgLocation);
    }
}
