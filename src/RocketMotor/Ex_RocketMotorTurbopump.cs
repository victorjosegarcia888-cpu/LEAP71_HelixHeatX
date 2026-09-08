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
        class RocketMotorTurbopump
        {
            public static void Task()
            {
                try
                {
                    {
                        // turbopump hub
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(-30, 0, 0));
                        BaseCylinder oHub       = new BaseCylinder(oLocalFrame, 50f, 26.8f);
                        Voxels oVoxels          = oHub.voxConstruct();
                        Sh.PreviewVoxels(oVoxels, Cp.clrBlue);
                    }

                    {
                        // turbopump blades
                        Lattice oLattice = new Lattice();
                        int nSteps = 8;
                        float fAngleRad = 83.0f * MathF.PI / 180.0f;

                        for (int i = 0; i <= nSteps; i++)
                        {
                            float fT = (float)i / nSteps;
                            float fR = 26.8f + fT * (67.0f - 26.8f);
                            float fZ = fT * 67.0f;
                            float fAngle = fAngleRad + fT * 0.1f;

                            Vector3 vecStart = new Vector3(0, 0, fZ);
                            Vector3 vecEnd = new Vector3(fR * MathF.Cos(fAngle), fR * MathF.Sin(fAngle), fZ);
                            oLattice.AddBeam(vecStart, 1.52f, vecEnd, 1.52f, false);
                        }

                        Voxels oVoxels = new Voxels(oLattice);
                        Sh.PreviewVoxels(oVoxels, Cp.clrGreen);
                    }
                }
                catch (Exception e)
                {
                    Library.Log($"Failed run example: \n{e.Message}"); ;
                }
            }
        }
    }
}
