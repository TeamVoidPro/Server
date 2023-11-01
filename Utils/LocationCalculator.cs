using System.Runtime.InteropServices.JavaScript;

namespace Server.Utils;

public class LocationCalculator
{
    public static double[] CalculateMinMaxCoordinates(double latitude, double longitude, double diameterInKm)
    {
        double radius = 6371; // Earth's radius in kilometers

        // Calculate the angular distance covered by the diameter
        double angularDiameter = diameterInKm / radius;

        // Calculate maximum and minimum latitude
        double maxLatitude = latitude + (angularDiameter * (180 / Math.PI));
        double minLatitude = latitude - (angularDiameter * (180 / Math.PI));

        // Calculate maximum and minimum longitude
        double maxLongitude = longitude + (angularDiameter * (180 / Math.PI) / Math.Cos(latitude * Math.PI / 180));
        double minLongitude = longitude - (angularDiameter * (180 / Math.PI) / Math.Cos(latitude * Math.PI / 180));

        double[] coordinates = { maxLatitude, minLatitude, maxLongitude, minLongitude };
        return coordinates;
    }

}

