using ABB.Robotics.Math;
using ABB.Robotics.RobotStudio.Stations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RobotStudioEngine
{
    public class StationEngine
    {
        /// <summary>
        /// Repositions an object between two frames.
        /// </summary>
        /// <param name="fromFrame">The frame reference to reposition from</param>
        /// <param name="toFrame">The frame reference to reposition to</param>
        /// <param name="selectedObjects">The objects to move</param>
        private void AlignObjectTwoFramesTool(Frame fromFrame, Frame toFrame, object[] selectedObjects)
        {
            try
            {
                Matrix4 fromInv = GetObjectTransform((object)fromFrame).GlobalMatrix.Inverse();
                Matrix4 to = GetObjectTransform((object)toFrame).GlobalMatrix;

                int nCounter = 0;

                object[] objArray = selectedObjects;

                for (int index = 0; index < objArray.Length; ++index)
                {
                    object obj = objArray[index];
                    Transform tf = GetObjectTransform(obj);
                    if (tf != null)
                    {
                        Matrix4 newMat = to * (fromInv * tf.GlobalMatrix);
                        tf.GlobalMatrix = newMat;
                        nCounter++;
                        newMat = new Matrix4();
                    }
                    tf = null;
                    obj = null;
                }

                objArray = null;

                if (nCounter > 0)
                {
                    fromInv = new Matrix4();
                    to = new Matrix4();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to reposition between two frame for the following reason;\n\nERROR: " + ex.Message, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Returns an objects transform properties
        /// </summary>
        /// <param name="obj">The objects of which the Transform property is required</param>
        private Transform GetObjectTransform(object obj)
        {
            switch (obj)
            {
                case IHasTransform _:
                    return ((IHasTransform)obj).Transform;
                case RsWorkObject _:
                    return ((RsWorkObject)obj).UserFrame;
                case RsToolData _:
                    return ((RsToolData)obj).Frame;
            }

            return (Transform)null;
        }
    }
}
