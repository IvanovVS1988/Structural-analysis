using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Structural_analysis.Models;
using Structural_analysis.Models.SteelBeams;
using Structural_analysis.ViewModel;

namespace Structural_analysis.Controllers
{
    public class SteelSectionController : Controller
    {
        public IActionResult SteelISection()
        {
            SteelISectionModel model = new SteelISectionModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult SteelISection(SteelISectionModel model)
        {
            StrengthSteelISection results = new StrengthSteelISection(model.width1Top, model.thick1Top, model.width2Top, model.thick2Top, 
            model.width3Top, model.thick3Top, model.width1Bottom, model.thick1Bottom, model.width2Bottom, model.thick2Bottom, 
            model.width3Bottom, model.thick3Bottom, model.wallHeight, model.wallThick, model.steelType, model.Mmax, model.Q1, model.Mmin, 
            model.Q2, model.m);

            model.Ry = results.Ry * results.m;
            model.Rs = results.Rs * results.m;          

            model.SigmaXMaxBottom = results.SigmaXBottom(results.Mmax, results.Q1);
            model.SigmaXMaxTop = results.SigmaXTop(results.Mmax, results.Q1);
            model.SigmaXMinBottom = results.SigmaXBottom(results.Mmin, results.Q2);
            model.SigmaXMinTop = results.SigmaXTop(results.Mmin, results.Q2);

            model.TauM1 = results.TauM(results.Q1);
            model.TauM2 = results.TauM(results.Q2);

            model.kappa1 = results.kappa1();

            model.kappa_1 = results.kappa(results.Q1);
            model.kappa_2 = results.kappa(results.Q2);

            model.Qcritical1 = results.Qcritical(results.Q1);
            model.Qcritical2 = results.Qcritical(results.Q2);

            model.momentOfInertion = results.MomentOfInertionX;
            model.yMassCentre = results.YMassCentre;

            return View(model); 
        }

        public IActionResult SteelISectionOrtPlate()
        {
            SteelISectionOrtPlateModel model = new SteelISectionOrtPlateModel();
            return View(model);
        }
        [HttpPost]
        public IActionResult SteelISectionOrtPlate(SteelISectionOrtPlateModel model)
        {
            StrengthSteelISectionOrtPlate results = new StrengthSteelISectionOrtPlate(model.width1Top, model.thick1Top, model.width2Top, 
                model.thick2Top, model.width3Top, model.thick3Top, model.width1Bottom, model.thick1Bottom, model.width2Bottom, 
                model.thick2Bottom, model.width3Bottom, model.thick3Bottom, model.wallHeight, model.wallThick, model.widthOrtPlateTopLeft, 
                model.thickOrtPlateTopLeft, model.widthOrtPlateTopRight, model.thickOrtPlateTopRight, model.widthOrtPlateBottomLeft, 
                model.thickOrtPlateBottomLeft, model.widthOrtPlateBottomRight, model.thickOrtPlateBottomRight, model.hStringerTop, 
                model.thickStringerTop, model.numberOfTopStringersLeft, model.numberOfTopStringersRight, model.hStringerBottom, 
                model.thickStringerBottom, model.numberOfBottomStringersLeft, model.numberOfBottomStringersRight, model.yOrtPlateTop, 
                model.yOrtPlateBottom, model.steelType, model.Mmax, model.Q1, model.Mmin, model.Q2, model.m);

            model.Ry = results.Ry * results.m;
            model.Rs = results.Rs * results.m;

            model.SigmaXMaxBottom = results.SigmaXBottom(results.Mmax, results.Q1);
            model.SigmaXMaxTop = results.SigmaXTop(results.Mmax, results.Q1);
            model.SigmaXMinBottom = results.SigmaXBottom(results.Mmin, results.Q2);
            model.SigmaXMinTop = results.SigmaXTop(results.Mmin, results.Q2);

            model.TauM1 = results.TauM(results.Q1);
            model.TauM2 = results.TauM(results.Q2);

            model.kappa1 = results.kappa1();

            model.kappa_1 = results.kappa(results.Q1);
            model.kappa_2 = results.kappa(results.Q2);

            model.Qcritical1 = results.Qcritical(results.Q1);
            model.Qcritical2 = results.Qcritical(results.Q2);

            model.momentOfInertion = results.MomentOfInertionX;
            model.yMassCentre = results.YMassCentre;

            return View(model);
        }
        public IActionResult Test()
        {
            return View();
        }
    }
}
