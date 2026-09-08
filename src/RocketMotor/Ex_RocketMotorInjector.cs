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
        class RocketMotorInjector
        {
            public static void Task()
            {
                try
                {
                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorInjector_00"));

                    {
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(0, 0, 0));
                        BaseCylinder oPlate     = new BaseCylinder(oLocalFrame, 5f, 190f);
                        Voxels oVoxels          = oPlate.voxConstruct();
                        Sh.PreviewVoxels(oVoxels, Cp.clrBlue);
                        Sh.ExportVoxelsToSTLFile(oVoxels, Sh.strGetExportPath(Sh.EExport.STL, "RocketMotorInjector_Plate"));
                    }

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorInjector_01"));

                    {
                        Lattice oLattice = new Lattice();
                        int nCount = 24;
                        float fRadius = 117.5f;
                        float fAngleStep = 2.0f * MathF.PI / nCount;

                        for (int i = 0; i < nCount; i++)
                        {
                            float fAngle = i * fAngleStep;
                            Vector3 vecPos = new Vector3(fRadius * MathF.Cos(fAngle), fRadius * MathF.Sin(fAngle), 0);
                            oLattice.AddSphere(vecPos, 3f);
                        }

                        Voxels oVoxels = new Voxels(oLattice);
                        Sh.PreviewVoxels(oVoxels, Cp.clrGreen);
                        Sh.ExportVoxelsToSTLFile(oVoxels, Sh.strGetExportPath(Sh.EExport.STL, "RocketMotorInjector_Elements"));
                    }

                    Library.oViewer().RequestScreenShot(Sh.strGetExportPath(Sh.EExport.TGA, "RocketMotorInjector_02"));
                }
                catch (Exception e)
                {
                    Library.Log($"Failed run example: \n{e.Message}"); ;
                }
            }
        }
    }
}
