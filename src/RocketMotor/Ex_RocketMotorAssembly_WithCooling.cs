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
        class RocketMotorAssembly_WithCooling
        {
            public static void Task()
            {
                try
                {
                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorAssembly_WithCooling_00"));

                    Voxels voxMotor = new Voxels();

                    {
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(-60, 0, 0));
                        BaseCylinder oChamber   = new BaseCylinder(oLocalFrame, 200f, 190f);
                        oChamber.SetLengthSteps(500);
                        oChamber.SetRadius(new SurfaceModulation(fGetChamberRadius));
                        Voxels voxChamber       = oChamber.voxConstruct();
                        Sh.PreviewVoxels(voxChamber, Cp.clrBlue);
                        voxMotor = voxMotor.voxBoolAdd(voxChamber);
                    }

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorAssembly_WithCooling_01"));

                    {
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(60, 0, 0));
                        BaseCylinder oNozzle    = new BaseCylinder(oLocalFrame, 600f, 99f);
                        oNozzle.SetLengthSteps(500);
                        oNozzle.SetRadius(new SurfaceModulation(fGetNozzleRadius));
                        Voxels voxNozzle        = oNozzle.voxConstruct();
                        Sh.PreviewVoxels(voxNozzle, Cp.clrGreen);
                        voxMotor = voxMotor.voxBoolAdd(voxNozzle);
                    }

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorAssembly_WithCooling_02"));

                    {
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(0, 0, -110));
                        BaseCylinder oInjector  = new BaseCylinder(oLocalFrame, 5f, 190f);
                        Voxels voxInjector      = oInjector.voxConstruct();
                        Sh.PreviewVoxels(voxInjector, Cp.clrYellow);
                        voxMotor = voxMotor.voxBoolAdd(voxInjector);
                    }

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorAssembly_WithCooling_03"));

                    {
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(0, 120, 0));
                        BaseRing oPreburner     = new BaseRing(oLocalFrame, 60f, 10f);
                        Voxels voxPreburner     = oPreburner.voxConstruct();
                        Sh.PreviewVoxels(voxPreburner, Cp.clrFrozen);
                        voxMotor = voxMotor.voxBoolAdd(voxPreburner);
                    }

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorAssembly_WithCooling_04"));

                    Voxels voxCooling = voxGetCoolingChannels();
                    Sh.PreviewVoxels(voxCooling, Cp.clrToothpaste);
                    voxMotor = voxMotor.voxBoolSubtract(voxCooling);

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorAssembly_WithCooling_05"));

                    voxMotor = voxMotor.voxOverOffset(1.0f, 0f);
                    voxMotor = voxMotor.voxSmoothen(0.5f);

                    Sh.PreviewVoxels(voxMotor, Cp.clrRock);
                    Sh.ExportVoxelsToSTLFile(voxMotor, Sh.strGetExportPath(Sh.EExport.STL, "RocketMotorAssembly_WithCooling"));

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorAssembly_WithCooling_06"));
                }
                catch (Exception e)
                {
                    Library.Log($"Failed run example: \n{e.Message}"); ;
                }
            }

            static Voxels voxGetCoolingChannels()
            {
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

                return new Voxels(oChannels);
            }

            static float fGetChamberRadius(float fPhi, float fLengthRatio)
            {
                float fZ = fLengthRatio * 200f;
                float fR = 190f;
                return fR * 2.0f;
            }

            static float fGetNozzleRadius(float fPhi, float fLengthRatio)
            {
                float fZ = fLengthRatio * 600f;
                float fR = 99f + (530f - 99f) * fLengthRatio;
                return fR * 2.0f;
            }
        }
    }
}
