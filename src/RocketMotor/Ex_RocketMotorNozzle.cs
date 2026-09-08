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
        class RocketMotorNozzle
        {
            public static void Task()
            {
                try
                {
                    {
                        // basic nozzle profile
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(-60, 0, 0));
                        BaseCylinder oShape     = new BaseCylinder(oLocalFrame, 600f, 99f);
                        oShape.SetLengthSteps(500);
                        oShape.SetRadius(new SurfaceModulation(fGetNozzleRadius));
                        Voxels oVoxels          = oShape.voxConstruct();
                        Sh.PreviewVoxels(oVoxels, Cp.clrBlue);
                    }

                    {
                        // nozzle with wall thickness
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(60, 0, 0));
                        BaseCylinder oInner     = new BaseCylinder(oLocalFrame, 600f, 99f);
                        oInner.SetLengthSteps(500);
                        oInner.SetRadius(new SurfaceModulation(fGetNozzleRadius));
                        Voxels voxInner         = oInner.voxConstruct();

                        BaseCylinder oOuter    = new BaseCylinder(oLocalFrame, 600f, 532f);
                        oOuter.SetLengthSteps(500);
                        oOuter.SetRadius(new SurfaceModulation(fGetNozzleOuterRadius));
                        Voxels voxOuter        = oOuter.voxConstruct();

                        Voxels voxNozzle       = voxOuter.voxBoolSubtract(voxInner);
                        Sh.PreviewVoxels(voxNozzle, Cp.clrGreen);
                    }
                }
                catch (Exception e)
                {
                    Library.Log($"Failed run example: \n{e.Message}"); ;
                }
            }

            static float fGetNozzleRadius(float fPhi, float fLengthRatio)
            {
                float fZ = fLengthRatio * 600f;
                float fR = 99f + (530f - 99f) * fLengthRatio;
                return fR * 2.0f;
            }

            static float fGetNozzleOuterRadius(float fPhi, float fLengthRatio)
            {
                float fZ = fLengthRatio * 600f;
                float fR = 99f + (530f - 99f) * fLengthRatio + 3f;
                return fR * 2.0f;
            }
        }
    }
}
