namespace Structural_analysis.ViewModel
{
    public class SteelISectionOrtPlateModel
    {
        public double width1Top { get; set; }
        public double thick1Top { get; set; }
        public double width2Top { get; set; }
        public double thick2Top { get; set; }
        public double width3Top { get; set; }
        public double thick3Top { get; set; }
        public double width1Bottom { get; set; }
        public double thick1Bottom { get; set; }
        public double width2Bottom { get; set; }
        public double thick2Bottom { get; set; }
        public double width3Bottom { get; set; }
        public double thick3Bottom { get; set; }
        public double wallHeight { get; set; }
        public double wallThick { get; set; }
        public double widthOrtPlateTopLeft { get; set; } 
        public double thickOrtPlateTopLeft { get; set; }
        public double widthOrtPlateTopRight { get; set; }
        public double thickOrtPlateTopRight { get; set; }
        public double widthOrtPlateBottomLeft { get; set; }
        public double thickOrtPlateBottomLeft { get; set; }
        public double widthOrtPlateBottomRight { get; set; }
        public double thickOrtPlateBottomRight { get; set; }
        public double hStringerTop { get; set; }
        public double thickStringerTop { get; set; }
        public int numberOfTopStringersLeft { get; set; }
        public int numberOfTopStringersRight { get; set; }
        public double hStringerBottom { get; set; }
        public double thickStringerBottom { get; set; }
        public int numberOfBottomStringersLeft { get; set; }
        public int numberOfBottomStringersRight { get; set; }
        public double yOrtPlateTop { get; set; }
        public double yOrtPlateBottom { get; set; }
        public int steelType { get; set; }
        public double Mmax { get; set; }
        public double Q1 { get; set; }
        public double Mmin { get; set; }
        public double Q2 { get; set; }
        public double m { get; set; }
        public double momentOfInertion { get; set; }
        public double TauM1 { get; set; }
        public double TauM2 { get; set; }
        public double Qcritical1 { get; set; }
        public double Qcritical2 { get; set; }
        public double kappa1 { get; set; }
        public double kappa_1 { get; set; }
        public double kappa_2 { get; set; }
        public double SigmaXMaxBottom { get; set; }
        public double SigmaXMaxTop { get; set; }
        public double SigmaXMinBottom { get; set; }
        public double SigmaXMinTop { get; set; }

        public double yMassCentre { get; set; }
        public double Rs { get; set; }
        public double Ry { get; set; }
    }
}
