namespace Structural_analysis.Models
{
    public interface ICrossSection
    {
        double area();

        double staticMoment();

        double momentOfInertionX();

        double momentOfInertionY();

        double yMassCentre();

        double xMassCentre();
    }
}
