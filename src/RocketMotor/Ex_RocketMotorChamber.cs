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
        class RocketMotorChamber
        {
            public static void Task()
            {
                try
                {
                    {
                        // basic chamber cylinder
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(-60, 0, 0));
                        BaseCylinder oShape     = new BaseCylinder(oLocalFrame, 200f, 190f);
                        oShape.SetLengthSteps(500);
                        oShape.SetRadius(new SurfaceModulation(fGetChamberRadius));
                        Voxels oVoxels          = oShape.voxConstruct();
                        Sh.PreviewVoxels(oVoxels, Cp.clrBlue);
                    }

                    {
                        // chamber with modulated radius
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(60, 0, 0));
                        BaseCylinder oShape     = new BaseCylinder(oLocalFrame, 200f, 190f);
                        oShape.SetLengthSteps(500);
                        oShape.SetRadius(new SurfaceModulation(fGetChamberRadiusModulated));
                        Voxels oVoxels          = oShape.voxConstruct();
                        Sh.PreviewVoxels(oVoxels, Cp.clrGreen);
                    }
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

            static float fGetChamberRadiusModulated(float fPhi, float fLengthRatio)
            {
                float fZ = fLengthRatio * 200f;
                float fR = 190f - 5f * MathF.Cos(6f * fPhi);
                return fR * 2.0f;
            }
        }
    }
}
