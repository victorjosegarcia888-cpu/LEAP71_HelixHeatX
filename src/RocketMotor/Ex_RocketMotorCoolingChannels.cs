//
// SPDX-License-Identifier: CC0-1.0
//
// This example code file is released to the public under Creative Commons CC0.
// See https://creativecommons.org/publicdomain/zero/1.0/legalcode
//
// To the extent possible under law, LEAP 71 has waived all copyright and
// related or neighboring rights to this PicoGK Example Code.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS
// OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//


using PicoGK;
using System.Numerics;


namespace Leap71
{
    using ShapeKernel;

    namespace RocketMotorExamples
    {
        class RocketMotorCoolingChannels
        {
            public static void Task()
            {
                try
                {
                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorCoolingChannels_00"));

                    Lattice oChannels = new Lattice();
                    int nChannelCount = 120;
                    int nSteps = 100;
                    float fHelixAngleDeg = 35.0f;
                    float fHelixAngleRad = fHelixAngleDeg * MathF.PI / 180.0f;
                    float fChannelRadius = 200.0f;
                    float fChannelLength = 880.0f;
                    float fChannelWidth = 2.0f;

                    for (int i = 0; i < nChannelCount; i++)
                    {
                        Vector3 previousPoint = new Vector3();
                        bool hasPreviousPoint = false;
                        float fPhase = 2.0f * MathF.PI * i / nChannelCount;

                        for (int j = 0; j <= nSteps; j++)
                        {
                            float fZ = (float)j / nSteps * fChannelLength;
                            float fPhi = fPhase + fZ * MathF.Tan(fHelixAngleRad) / fChannelRadius;
                            Vector3 point = new Vector3(
                                fChannelRadius * MathF.Cos(fPhi),
                                fChannelRadius * MathF.Sin(fPhi),
                                fZ);

                            if (hasPreviousPoint)
                                oChannels.AddBeam(previousPoint, fChannelWidth, point, fChannelWidth, true);

                            previousPoint = point;
                            hasPreviousPoint = true;
                        }
                    }

                    Voxels oVoxels = new Voxels(oChannels);
                    Sh.PreviewVoxels(oVoxels, Cp.clrToothpaste);
                    Sh.ExportVoxelsToSTLFile(oVoxels, Sh.strGetExportPath(Sh.EExport.STL, "RocketMotorCoolingChannels"));

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorCoolingChannels_01"));
                }
                catch (Exception e)
                {
                    Library.Log($"Failed run example: \n{e.Message}"); ;
                }
            }
        }
    }
}
