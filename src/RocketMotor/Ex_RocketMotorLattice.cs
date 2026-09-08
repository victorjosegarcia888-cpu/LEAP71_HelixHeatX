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
    using LatticeLibrary;

    namespace RocketMotorExamples
    {
        class RocketMotorLattice
        {
            public static void Task()
            {
                try
                {
                    {
                        // basic body-centred lattice in bounding box
                        LocalFrame oLocalFrame  = new LocalFrame(new Vector3(0, 0, 0));
                        BaseBox oBounding       = new BaseBox(oLocalFrame, 100f, 100f, 100f);
                        Voxels voxBounding      = oBounding.voxConstruct();
                        Sh.PreviewVoxels(voxBounding, Cp.clrWarning);

                        ICellArray xCellArray = new RegularCellArray(voxBounding, 20, 20, 20);
                        ILatticeType xLatticeType = new BodyCentreLattice();
                        IBeamThickness xBeamThickness = new ConstantBeamThickness(1f);
                        xBeamThickness.SetBoundingVoxels(voxBounding);

                        uint nSubSample = 5;
                        Lattice oLattice = new Lattice();
                        foreach (IUnitCell xCell in xCellArray.aGetUnitCells())
                        {
                            xBeamThickness.UpdateCell(xCell);
                            xLatticeType.AddCell(ref oLattice, xCell, xBeamThickness, nSubSample);
                        }

                        Voxels oVoxels = new Voxels(oLattice);
                        oVoxels = oVoxels.voxBoolIntersect(voxBounding);
                        Sh.PreviewVoxels(oVoxels, Cp.clrLemongrass);
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
