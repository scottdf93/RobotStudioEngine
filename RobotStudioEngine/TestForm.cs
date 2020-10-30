using ABB.Robotics.Controllers;
using ABB.Robotics.RobotStudio.Controllers;
using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;

namespace RobotStudioEngine
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void GetAllControllersButton_Click(object sender, EventArgs e)
        {
            ControllersEngine ControllersEngine = new ControllersEngine();
            ControllerListView.AddObjects(ControllersEngine.GetAllAvaliableControllers(ControllerType.VC));
        }

        private void GetControllerButton_Click(object sender, EventArgs e)
        {
            ControllersEngine ControllersEngine = new ControllersEngine();

            if (!string.IsNullOrEmpty(GuidTextBox.Text) && GuidTextBox.Text != "00000")
            {
                // Get the virtual controller using the GUID
                ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.VC, new Guid(GuidTextBox.Text)));
            }

            // Get the virtual controller using the name
            ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.VC, ControllerNameTextBox.Text));
            
            // Get the virtual controller using the IP address
            ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.VC, IPAddress.Parse(IpAddressTextBox.Text)));
            
            // Get the virtual controller using the MAC address
            ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.VC, PhysicalAddress.Parse(MacAddressTextBox.Text)));

            // Get the real controller using the GUID
            ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.RC, new Guid(GuidTextBox.Text)));
            
            // Get the real controller using the name
            ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.RC, ControllerNameTextBox.Text));
            
            // Get the real controller using the IP address
            ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.RC, IPAddress.Parse(IpAddressTextBox.Text)));
            
            // Get the real controller using the MAC address
            ControllerListView.AddObject(ControllersEngine.GetController(ControllerType.RC, PhysicalAddress.Parse(MacAddressTextBox.Text)));
        }
    }
}
