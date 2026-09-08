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
        class RocketMotorAssembly
        {
            public static void Task()
            {
                try
                {
                    Voxels voxMotor = new Voxels();

                    // chamber
                    LocalFrame oChamberFrame = new LocalFrame(new Vector3(-60, 0, 0));
                    BaseCylinder oChamber = new BaseCylinder(oChamberFrame, 200f, 190f);
                    oChamber.SetLengthSteps(500);
                    oChamber.SetRadius(new SurfaceModulation(fGetChamberRadius));
                    Voxels voxChamber = oChamber.voxConstruct();
                    Sh.PreviewVoxels(voxChamber, Cp.clrBlue);

                    // nozzle
                    LocalFrame oNozzleFrame = new LocalFrame(new Vector3(60, 0, 0));
                    BaseCylinder oNozzle = new BaseCylinder(oNozzleFrame, 600f, 99f);
                    oNozzle.SetLengthSteps(500);
                    oNozzle.SetRadius(new SurfaceModulation(fGetNozzleRadius));
                    Voxels voxNozzle = oNozzle.voxConstruct();
                    Sh.PreviewVoxels(voxNozzle, Cp.clrGreen);

                    voxMotor = voxMotor.voxBoolAdd(voxChamber);
                    voxMotor = voxMotor.voxBoolAdd(voxNozzle);

                    // injector
                    LocalFrame oInjectorFrame = new LocalFrame(new Vector3(0, 0, -110));
                    BaseCylinder oInjector = new BaseCylinder(oInjectorFrame, 5f, 190f);
                    Voxels voxInjector = oInjector.voxConstruct();
                    Sh.PreviewVoxels(voxInjector, Cp.clrYellow);
                    voxMotor = voxMotor.voxBoolAdd(voxInjector);

                    // preburner
                    LocalFrame oPreburnerFrame = new LocalFrame(new Vector3(0, 120, 0));
                    BaseRing oPreburner = new BaseRing(oPreburnerFrame, 60f, 10f);
                    Voxels voxPreburner = oPreburner.voxConstruct();
                    Sh.PreviewVoxels(voxPreburner, Cp.clrFrozen);
                    voxMotor = voxMotor.voxBoolAdd(voxPreburner);

                    // final preview
                    Sh.PreviewVoxels(voxMotor, Cp.clrRock);
                }
                catch (Exception e)
                {
                    Library.Log($"Failed run example: \n{e.Message}"); ;
                }
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
